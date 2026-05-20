# Colorful

A BONELAB MelonLoader mod that colors UI elements — radial menu, main menu, preferences, inventory, leaderboard, spawnable UI, InfoBoard, arena score display, Hub modules, and Fusion compatibility.

## Install

1. Install [BoneLib](https://bonelab.thunderstore.io/package/BoneLib/BoneLib/)
2. Download `Colorful.dll` from [Releases](https://github.com/YOUR_USERNAME/Colorful/releases)
3. Place it in `BONELAB/Mods/`
4. Launch the game

## Build

Requires .NET 6 SDK and BONELAB game files for Il2Cpp assembly references.

```
export BONELAB_DIR="/path/to/SteamLibrary/steamapps/common/BONELAB"
dotnet build Colorful.csproj -c Release
cp bin/Release/Colorful.dll "$BONELAB_DIR/Mods/"
```

## Usage

- Open the BoneMenu (default: `Insert`) → find the **Colorful** page
- Toggle the mod on/off
- Adjust colors for each UI element individually
- Use **Color Override** to set all colors at once
- Use **Default All** to reset everything
- Radar directions match their respective UI section

## Compatibility

- **LabFusion** — automatically detected, adds Fusion menu color control
- **BrowsingPlus** — auto-repaints when new UI elements appear
- **SearchThing** — auto-repaints on search results

## License

MIT
