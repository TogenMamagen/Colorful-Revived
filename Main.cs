using MelonLoader;
using UnityEngine;
using BoneLib;
using BoneLib.BoneMenu;

[assembly: MelonInfo(typeof(Colorful.Main), "Colorful-Revived", "1.0.1", "TogenMerfagen")]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonPriority(1)]

namespace Colorful
{
    public partial class Main : MelonMod
    {
        public void OnSceneAwake()
        {
            if (PreferencesCreator.IsEnabled)
                MoggingTime();
            RadialMenuButtonsUI.RadialMenuButtons();
            RadialLabelReader.StartPolling();
            PanelPatches.ApplySearchThingPatches();
            ModModuleManager.OnSceneAwakeAll();
        }

        public override void OnInitializeMelon()
        {
            ModModuleManager.Register(new FusionModule());

            Hooking.OnLevelLoaded += _ => OnSceneAwake();

            PreferencesCreator.MelonPreferencesCreator();
            Colors.LoadFromPrefs();
            PreferencesCreator.BonemenuCreator();

            PanelPatches.Apply();
        }

        public static void MoggingTime()
        {
            Colors.LoadFromPrefs();

            var objectsWithKeyword = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in objectsWithKeyword)
            {
                if (obj.name.Contains("group_levelSelect"))
                {
                    LevelSelectUI.LevelSelect(obj.transform);
                }
                else if (obj.name.Contains("panel_Preferences"))
                {
                    PreferencesUI.Preferences(obj.transform);
                }
                else if (obj.name.Contains("grid_Graphics"))
                {
                    PreferencesUI.Extra(obj.transform);
                }
                else if (obj.name.Contains("group_toolMenu"))
                {
                    SpawnGunUI.SpawnGun(obj.transform);
                }
                else if (obj.name.Contains("group_AvatarSelect"))
                {
                    AvatarSelectUI.Avatar(obj.transform);
                }
                else if (obj.name.Contains("BodyMallController"))
                {
                    AvatarSelectUI.Bodymall(obj.transform);
                }
                else if (obj.name.Contains("CANVAS_RADIALUI"))
                {
                    RadialMenuTextAndImageUI.RadialMenuTextAndImage(obj.transform);
                }
                else if (obj.name.Contains("canvasMainMenu"))
                {
                    MenuUI.MainMenu(obj.transform);
                }
                else if (obj.name.Contains("CANVAS_UX"))
                {
                    var menu = obj.transform.Find("MENU");
                    if (menu != null)
                        MenuUI.MainMenu(menu);
                }
                else if (obj.name.Contains("GashaponMachine"))
                {
                    var ui = obj.transform.Find("UI");
                    if (ui != null)
                        TransformPainter.Paint(ui, PreferencesCreator.IsEnabled ? Colors.Menu : Color.white);
                }
                else if (obj.name.Contains("slot_target"))
                {
                    InventorySlotsUI.Paint(obj.transform, PreferencesCreator.IsEnabled ? Colors.South : Color.white);
                }
                else if (obj.name == "CANVAS_INFOBOARD")
                {
                    InfoBoardUI.Paint(obj.transform);
                }
                else if (obj.name == "HighscoreLeaderboard_UI_Group")
                {
                    InfoBoardUI.PaintLeaderboard(obj.transform);
                }
                else if (obj.name == "SpawnableCanvas")
                {
                    TransformPainter.Paint(obj.transform, PreferencesCreator.IsEnabled ? Colors.West : Color.white);
                }
                else if (obj.name.Contains("GameControl_Display"))
                {
                    TransformPainter.Paint(obj.transform, PreferencesCreator.IsEnabled ? Colors.Menu : Color.white);
                }
                else if (obj.name.Contains("ui_Module"))
                {
                    var skip = new System.Collections.Generic.HashSet<string> { "popup_MOD WARNING", "popup_AVATARWARNING" };
                    TransformPainter.Paint(obj.transform, PreferencesCreator.IsEnabled ? Colors.Menu : Color.white, skip);
                }

                ModModuleManager.OnMoggingTimeAll(obj);
            }
            BoneMenuUI.Paint();
            RadialMenuButtonsUI.RadialMenuButtons();
        }
    }
}
