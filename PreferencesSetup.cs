using MelonLoader;
using UnityEngine;

// This file is part of the `PreferencesCreator` partial class.
// See `BoneMenuBuilder.cs` for the other half (BoneMenu page creation).

namespace Colorful
{
    internal partial class PreferencesCreator
    {
        public static MelonPreferences_Category MelonPrefCategory { get; private set; }
        public static MelonPreferences_Entry<bool> MelonPrefEnabled { get; private set; }
        public static bool IsEnabled { get; private set; }
        public static MelonPreferences_Entry<Color> NorthPref { get; private set; }
        public static MelonPreferences_Entry<Color> NorthEastPref { get; private set; }
        public static MelonPreferences_Entry<Color> EastPref { get; private set; }
        public static MelonPreferences_Entry<Color> SouthEastPref { get; private set; }
        public static MelonPreferences_Entry<Color> SouthPref { get; private set; }
        public static MelonPreferences_Entry<Color> SouthWestPref { get; private set; }
        public static MelonPreferences_Entry<Color> WestPref { get; private set; }
        public static MelonPreferences_Entry<Color> NorthWestPref { get; private set; }
        public static MelonPreferences_Entry<Color> MiddlePref { get; private set; }
        public static MelonPreferences_Entry<Color> MenuPref { get; private set; }

        public static void MelonPreferencesCreator()
        {
            MelonPrefCategory = MelonPreferences.CreateCategory("Colorful");
            MelonPrefEnabled = MelonPrefCategory.CreateEntry("IsEnabled", true);
            IsEnabled = MelonPrefEnabled.Value;
            NorthPref = MelonPrefCategory.CreateEntry("Eject Color", Colors.NorthDefault);
            NorthEastPref = MelonPrefCategory.CreateEntry("Level Select Color", Colors.NorthEastDefault);
            EastPref = MelonPrefCategory.CreateEntry("Preferences Color", Colors.EastDefault);
            SouthEastPref = MelonPrefCategory.CreateEntry("Quick Mute Color", Colors.SouthEastDefault);
            SouthPref = MelonPrefCategory.CreateEntry("Inventory Color", Colors.SouthDefault);
            SouthWestPref = MelonPrefCategory.CreateEntry("Spawn Devtools Color", Colors.SouthWestDefault);
            WestPref = MelonPrefCategory.CreateEntry("SpawnGun Menu Color", Colors.WestDefault);
            NorthWestPref = MelonPrefCategory.CreateEntry("Avatar Select Color", Colors.NorthWestDefault);
            MiddlePref = MelonPrefCategory.CreateEntry("Radial Cancel Color", Colors.MiddleDefault);
            MenuPref = MelonPrefCategory.CreateEntry("Menu Color", Colors.MenuDefault);

            foreach (var module in ModModuleManager.Modules)
                module.SetupPref(MelonPrefCategory);
        }

        public static void OnSetEnabled(bool value)
        {
            IsEnabled = value;
            MelonPrefEnabled.Value = value;
            MelonPrefCategory.SaveToFile();
            Main.MoggingTime();
        }
    }
}
