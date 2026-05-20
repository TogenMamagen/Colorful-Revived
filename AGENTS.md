# Colorful — BONELAB MelonLoader 0.6.x mod

C#, .NET 6, LangVersion 7.3. Colors BONELAB UI elements (radial menu, main menu, preferences, inventory, leaderboard, spawnable UI, InfoBoard, arena score display, Hub mode modules).

## Build

```sh
export BONELAB_DIR="/var/mnt/wwn-.../SteamLibrary/steamapps/common/BONELAB"
podman exec mono-builder dotnet build Colorful.csproj -c Release
cp bin/Release/Colorful.dll "$BONELAB_DIR/Mods/"
```

- Host has no `dotnet` — build inside `mono-builder` container. Host `/run/media/system/...` is at same path inside container.
- `$(BONELAB_DIR)` required at build time for Il2Cpp assembly references.
- `build.sh` and `Melon Loader Mod5.csproj` are dead; only `Colorful.csproj` is active.

## MelonLoader 0.6.x quirks

- **Mod class must be `public`** — `internal` is silently ignored at runtime.
- **Requires assembly-level attributes** in `Main.cs`:
  `[assembly: MelonInfo(typeof(Melon_Loader_Mod5.Main), "Colorful", "1.2.0", "Bread Soup")]`
  `[assembly: MelonGame("Stress Level Zero", "BONELAB")]`
  `[assembly: MelonPriority(1)]`
- **Do NOT create `Properties/AssemblyInfo.cs`** — SDK-style project auto-generates it.
- Il2CppInterop prepends `Il2Cpp` to namespaces: `TMPro.TextMeshProUGUI` → `Il2CppTMPro.TextMeshProUGUI`.

## BoneLib 3.x API

- `Hooking.OnLevelLoaded` (not `OnLevelInitialized`)
- `element.ElementColor = value` (not `.Color`)
- `FunctionElement`, `FloatElement` etc. in `BoneLib.BoneMenu` (no `.Elements` sub-namespace)

## Entrypoint

- `OnInitializeMelon()` → register `FusionModule`, bind `Hooking.OnLevelLoaded`, create prefs + colors + BoneMenu, apply patches.
- `OnSceneAwake()` (from `Hooking.OnLevelLoaded`) → if enabled runs `MoggingTime()`, always runs `RadialMenuButtonsUI.RadialMenuButtons()` + `RadialLabelReader.StartPolling()` + `PanelPatches.ApplySearchThingPatches()` + `ModModuleManager.OnSceneAwakeAll()`.
- `MoggingTime()` does `GameObject.FindObjectsOfType<GameObject>(true)` → dispatches by name.

### MoggingTime dispatch

| GameObject name condition | Handler | Color used |
|---|---|---|
| `group_levelSelect` | `LevelSelectUI.LevelSelect` | East |
| `panel_Preferences` | `PreferencesUI.Preferences` | East |
| `grid_Graphics` | `PreferencesUI.Extra` | East |
| `group_toolMenu` | `SpawnGunUI.SpawnGun` | East |
| `group_AvatarSelect` | `AvatarSelectUI.Avatar` | NorthWest |
| `BodyMallController` | `AvatarSelectUI.Bodymall` | NorthWest (tab text=`Color.black`) |
| `CANVAS_RADIALUI` | `RadialMenuTextAndImageUI` | Per-index color mapping |
| `canvasMainMenu` / `CANVAS_UX` | `MenuUI.MainMenu` (on `MENU` child) | Menu |
| `GashaponMachine` | `TransformPainter.Paint` on `UI` child | Menu |
| `slot_target` | `InventorySlotsUI.Paint` (paints `PageElementView.color2`) | South |
| `CANVAS_INFOBOARD` | `InfoBoardUI.Paint` | Menu |
| `HighscoreLeaderboard_UI_Group` | `InfoBoardUI.PaintLeaderboard` (images=get darker) | Menu |
| `SpawnableCanvas` | `TransformPainter.Paint` | West |
| `GameControl_Display*` | `TransformPainter.Paint` | Menu |
| `ui_Module*` | `TransformPainter.Paint` (skips popup warnings) | Menu |
| `canvas_FusionMenu` (via FusionModule) | `FusionUI.Paint` | Fusion (module color) |

After the if-else chain: `ModModuleManager.OnMoggingTimeAll(obj)`, `BoneMenuUI.Paint()`, `RadialMenuButtonsUI.RadialMenuButtons()`.

## Painting conventions

- **`TransformPainter.Paint()`** — generic recursive painter: Image → TextMeshProUGUI → TextMeshPro → RawImage → Text. Used by LevelSelectUI, MenuUI, PreferencesUI, SpawnGunUI, InfoBoardUI, and inline handlers. Accepts optional `HashSet<string> skipList` checked against each child name at every recursion depth.
- **Skip lists** vary per handler — common names: `img_bg`, `img_outline`, `image_backline`, `image_bgFade`, `Background`, `popup_MOD WARNING`, `popup_AVATARWARNING`.
- **`BoneMenuUI.TryPaint`** — paints TextMeshProUGUI/TextMeshPro/Text only if `IsWhite(c)` (`r,g,b > 0.9f`). Images painted unconditionally.
- **`FusionUI.Paint`** — paints text unconditionally (`TextMeshProUGUI`), paints Image/RawImage only if `IsWhite`. Profile picture protection via parent-Mask check. NameTag Color preview preserved (skips non-white text inside "NameTag Color").
- **`InventorySlotsUI.Paint`** — paints `PageElementView.color2` on children (falls back to `MeshRenderer.material.color`). Patches `PageElementView.set_color2` to intercept hover highlight overrides.
- **`InfoBoardUI.PaintLeaderboard`** — paints Images with `Color.Lerp(color, Color.black, 0.6f)`, paints TextMeshProUGUI with full color.
- **Radial menu** uses `ColorForIndex(i)` mapping (i=0/1→North, 3/4→NE, 6/7→East, 9/10→SE, 12/13→South, 15/16→SW, 18/19→West, 21/22→NW).
- **Radial buttons** use `PageElementView.color2`, not `.color`.

## BoneMenu layout

```
Colorful (rainbow root)
├── Mod Toggle
├── UI Colors/ → Menu, Radial Cancel
├── Radial Menu/ → Eject, Level Select, Preferences, Quick Mute, Inventory, Spawn Devtools, SpawnGun Menu, Avatar Select
├── Mods/ → Fusion Menu (auto-populated by ModModuleManager)
├── Color Override (sliders with confirm, on root)
└── Default All (button with confirm dialog, on root)
```

Root name built via `BuildRainbowName()` — each letter colored from directional colors. `RefreshRootName(root)` called after color changes.

BoneMenu root name re-labeled from radial labels via `UpdateColorPageNames(string[])`.

## Harmony patches

- **`PanelPatches`** — postfixes on `LevelsPanelView.OnEnable` + `AvatarsPanelView.OnEnable` (repaints when BrowsingPlus adds UI). Also patches `SearchThing.Extensions.SpawnablePanelExtension`. Fusion patches migrated to FusionModule.
- **`FusionModule`** — scans ALL loaded assemblies for types with "MenuPage" in the name, patches both `OnEnable` (paint with Fusion color) and `OnDisable` (reset divider to `Colors.East`). Handler verifies instance is inside `canvas_FusionMenu` before painting.
- **`InventorySlotsUI`** — patches `PageElementView.set_color2` to intercept hover highlight color overrides on inventory slot circles.

## ModCompat module system (`ColorChanging/ModCompat/`)

### `ModModule.cs` — Abstract base
- `DisplayName`, `AssemblyName` (DLL name to detect), `PrefEntryName` (config key), `DefaultColor`
- `Color` / `ColorPref` — runtime color state
- `Detect()` — checks if assembly is loaded in `AppDomain`
- `SetupPref(category)`, `LoadFromPrefs()`, `CreateBoneMenuPage(parent)` — lifecycle
- `SetColor(color)` / `ResetToDefault()` — apply + save
- `ApplyPatches()`, `OnSceneAwake()`, `OnMoggingTime(GameObject)` — overridable hooks

### `ModModuleManager` — Static registry
- `Register(module)`, `InitializeAll()`, `OnSceneAwakeAll()`, `OnMoggingTimeAll()`
- `SetAllColors(color)`, `ResetAll()`, `Get<T>()`
- `SetDividerLineColor(color)` — paints `image----------------` under `panel_Preferences`

### `FusionModule` — LabFusion compatibility
- Detects `"LabFusion"` assembly. Pref key: `"Fusion Menu Color"` (preserves existing config).
- Paints `canvas_FusionMenu`, `button_Fusion/image_backline` (nav underline), and `image----------------` (Preferences divider line).
- Harmony patches: all `*MenuPage.OnEnable` (repaint Fusion pages), `*MenuPage.OnDisable` (reset divider to East).
- Uses `FusionUI.Paint()` for element color logic.

## Adding a new mod module
1. New class inheriting `ModModule` in `ColorChanging/ModCompat/`
2. Override `DisplayName`, `AssemblyName`, `PrefEntryName`, `DefaultColor`
3. Override `OnMoggingTime(GameObject)` to catch your mod's GameObjects
4. Override `ApplyPatches()` for any Harmony patches needed
5. Register in `Main.OnInitializeMelon()` via `ModModuleManager.Register(new YourModule())`
6. Module auto-skips if the target assembly isn't detected

## Divider line management

The `image----------------` element (visual separator in Preferences panel) dynamically switches color:
- Fusion menu open → Fusion module color (via OnEnable patch)
- Fusion menu closed → `Colors.East` (via OnDisable patch)
- Managed via `ModModuleManager.SetDividerLineColor(color)`
