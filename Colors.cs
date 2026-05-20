using UnityEngine;

namespace Colorful
{
    public static class Colors
    {
        public static Color North = new Color(0f, 1f, 1f, 1f);
        public static Color NorthEast = new Color(1f, 0.5f, 0f, 1f);
        public static Color East = new Color(1f, 1f, 0f, 1f);
        public static Color SouthEast = new Color(0f, 0f, 1f, 1f);
        public static Color South = new Color(1f, 0f, 1f, 1f);
        public static Color SouthWest = new Color(0.5f, 0f, 1f, 1f);
        public static Color West = new Color(1f, 0f, 0f, 1f);
        public static Color NorthWest = new Color(0f, 1f, 0f, 1f);
        public static Color Middle = new Color(1f, 0.2f, 0.2f, 0.7f);
        public static Color Menu = new Color(1f, 1f, 1f, 1f);

        public static readonly Color NorthDefault = new Color(0f, 1f, 1f, 1f);
        public static readonly Color NorthEastDefault = new Color(1f, 0.5f, 0f, 1f);
        public static readonly Color EastDefault = new Color(1f, 1f, 0f, 1f);
        public static readonly Color SouthEastDefault = new Color(0f, 0f, 1f, 1f);
        public static readonly Color SouthDefault = new Color(1f, 0f, 1f, 1f);
        public static readonly Color SouthWestDefault = new Color(0.5f, 0f, 1f, 1f);
        public static readonly Color WestDefault = new Color(1f, 0f, 0f, 1f);
        public static readonly Color NorthWestDefault = new Color(0f, 1f, 0f, 1f);
        public static readonly Color MiddleDefault = new Color(1f, 0.2f, 0.2f, 1f);
        public static readonly Color MenuDefault = new Color(1f, 1f, 1f, 1f);

        public static void LoadFromPrefs()
        {
            North = FromPref(PreferencesCreator.NorthPref, NorthDefault);
            NorthEast = FromPref(PreferencesCreator.NorthEastPref, NorthEastDefault);
            East = FromPref(PreferencesCreator.EastPref, EastDefault);
            SouthEast = FromPref(PreferencesCreator.SouthEastPref, SouthEastDefault);
            South = FromPref(PreferencesCreator.SouthPref, SouthDefault);
            SouthWest = FromPref(PreferencesCreator.SouthWestPref, SouthWestDefault);
            West = FromPref(PreferencesCreator.WestPref, WestDefault);
            NorthWest = FromPref(PreferencesCreator.NorthWestPref, NorthWestDefault);
            Middle = FromPref(PreferencesCreator.MiddlePref, MiddleDefault);
            Menu = FromPref(PreferencesCreator.MenuPref, MenuDefault);
        }

        private static Color FromPref(MelonLoader.MelonPreferences_Entry<Color> pref, Color fallback)
        {
            return pref != null ? pref.Value : fallback;
        }
    }
}
