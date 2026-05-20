using BoneLib.BoneMenu;
using UnityEngine;

// This file is part of the `PreferencesCreator` partial class.
// See `PreferencesSetup.cs` for the other half (MelonPreferences entries).

namespace Colorful
{
    internal partial class PreferencesCreator
    {
        private static Page _menuPage, _northPage, _northEastPage, _eastPage,
            _southEastPage, _southPage, _southWestPage, _westPage, _northWestPage, _middlePage;

        private static Page[] _directionalPages;

        public static void BonemenuCreator()
        {
            var root = Page.Root.CreatePage(BuildRainbowName(), Color.white);
            root.CreateBool("Mod Toggle", Color.yellow, IsEnabled, OnSetEnabled);

            var uiPage = root.CreatePage("UI Colors", Colors.Menu);
            _menuPage = CreateColorPage(uiPage, "Menu", Colors.Menu, MenuPref);
            _middlePage = CreateColorPage(uiPage, "Radial Cancel", Colors.Middle, MiddlePref);

            var radialPage = root.CreatePage("Radial Menu", Color.white);
            _northPage = CreateColorPage(radialPage, "Eject", Colors.North, NorthPref);
            _northEastPage = CreateColorPage(radialPage, "Level Select", Colors.NorthEast, NorthEastPref);
            _eastPage = CreateColorPage(radialPage, "Preferences", Colors.East, EastPref);
            _southEastPage = CreateColorPage(radialPage, "Quick Mute", Colors.SouthEast, SouthEastPref);
            _southPage = CreateColorPage(radialPage, "Inventory", Colors.South, SouthPref);
            _southWestPage = CreateColorPage(radialPage, "Spawn Devtools", Colors.SouthWest, SouthWestPref);
            _westPage = CreateColorPage(radialPage, "SpawnGun Menu", Colors.West, WestPref);
            _northWestPage = CreateColorPage(radialPage, "Avatar Select", Colors.NorthWest, NorthWestPref);

            _directionalPages = new[] { _northPage, _northEastPage, _eastPage, _southEastPage,
                                        _southPage, _southWestPage, _westPage, _northWestPage };

            ModModuleManager.InitializeAll(MelonPrefCategory, root);

            CreateOverrideOnRoot(root);
            CreateDefaultAllOnRoot(root);
        }

        private static string BuildRainbowName()
        {
            return
                $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.North)}>C</color>" +
                $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.NorthEast)}>o</color>" +
                $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.East)}>l</color>" +
                $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.SouthEast)}>o</color>" +
                $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.South)}>r</color>" +
                $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.SouthWest)}>f</color>" +
                $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.West)}>u</color>" +
                $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.NorthWest)}>l</color>";
        }

        private static void RefreshRootName(Page root)
        {
            root.Name = BuildRainbowName();
        }

        private static Page CreateColorPage(Page root, string name, Color color, MelonLoader.MelonPreferences_Entry<Color> pref)
        {
            var page = root.CreatePage(name, color);
            ColorSliders.Create(page, color, updatedColor =>
            {
                SetColor(page, pref, updatedColor);
                RefreshRootName(root);
            });
            return page;
        }

        private static void SetColor(Page page, MelonLoader.MelonPreferences_Entry<Color> pref, Color color)
        {
            page.Color = color;
            pref.Value = color;
            MelonPrefCategory.SaveToFile();
            Main.MoggingTime();
        }

        private static void SetAllColors(Color color)
        {
            SetColor(_menuPage, MenuPref, color);
            SetColor(_northPage, NorthPref, color);
            SetColor(_northEastPage, NorthEastPref, color);
            SetColor(_eastPage, EastPref, color);
            SetColor(_southEastPage, SouthEastPref, color);
            SetColor(_southPage, SouthPref, color);
            SetColor(_southWestPage, SouthWestPref, color);
            SetColor(_westPage, WestPref, color);
            SetColor(_northWestPage, NorthWestPref, color);
            SetColor(_middlePage, MiddlePref, color);

            ModModuleManager.SetAllColors(color);
        }

        private static void ResetColor(Page page, MelonLoader.MelonPreferences_Entry<Color> pref, Color defaultColor)
        {
            page.Color = defaultColor;
            pref.Value = defaultColor;
            MelonPrefCategory.SaveToFile();
            Main.MoggingTime();
        }

        private static void CreateOverrideOnRoot(Page root)
        {
            ColorSliders.CreateWithConfirm(root, Color.white, updatedColor =>
            {
                SetAllColors(updatedColor);
                RefreshRootName(root);
            });
        }

        private static void CreateDefaultAllOnRoot(Page root)
        {
            root.CreateFunction("Default All", Color.black, () =>
            {
                Menu.DisplayDialog("Reset All Colors", "Are you sure? This will set all colors to default",
                    confirmAction: () =>
                    {
                        SetColor(_menuPage, MenuPref, Colors.MenuDefault);
                        ResetColor(_northPage, NorthPref, Colors.NorthDefault);
                        ResetColor(_northEastPage, NorthEastPref, Colors.NorthEastDefault);
                        ResetColor(_eastPage, EastPref, Colors.EastDefault);
                        ResetColor(_southEastPage, SouthEastPref, Colors.SouthEastDefault);
                        ResetColor(_southPage, SouthPref, Colors.SouthDefault);
                        ResetColor(_southWestPage, SouthWestPref, Colors.SouthWestDefault);
                        ResetColor(_westPage, WestPref, Colors.WestDefault);
                        ResetColor(_northWestPage, NorthWestPref, Colors.NorthWestDefault);
                        ResetColor(_middlePage, MiddlePref, Colors.MiddleDefault);
                        ModModuleManager.ResetAll();
                        RefreshRootName(root);
                    });
            });
        }

        public static void UpdateColorPageNames(string[] names)
        {
            if (_directionalPages == null || names == null || names.Length < 8)
                return;
            for (int i = 0; i < 8; i++)
            {
                if (!string.IsNullOrEmpty(names[i]))
                    _directionalPages[i].Name = names[i];
            }
        }
    }
}
