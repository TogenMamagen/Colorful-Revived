using Il2CppSLZ.Bonelab;
using UnityEngine;

namespace Melon_Loader_Mod5
{
    public class RadialMenuButtonsUI
    {
        static PageItemView N;
        static PageItemView NE;
        static PageItemView E;
        static PageItemView SE;
        static PageItemView S;
        static PageItemView SW;
        static PageItemView W;
        static PageItemView NW;
        static PageElementView M;

        public static void RadialMenuButtons()
        {
            SetupButton(ref N, "button_Region_N", Colors.North);
            SetupButton(ref NE, "button_Region_NE", Colors.NorthEast);
            SetupButton(ref E, "button_Region_E", Colors.East);
            SetupButton(ref SE, "button_Region_SE", Colors.SouthEast);
            SetupButton(ref S, "button_Region_S", Colors.South);
            SetupButton(ref SW, "button_Region_SW", Colors.SouthWest);
            SetupButton(ref W, "button_Region_W", Colors.West);
            SetupButton(ref NW, "button_Region_NW", Colors.NorthWest);
            SetupCancelButton();
        }

        private static void SetupButton(ref PageItemView field, string objectName, Color activeColor)
        {
            if (field == null)
            {
                var all = GameObject.FindObjectsOfType<GameObject>(true);
                foreach (var go in all)
                {
                    if (go.name == objectName)
                    {
                        field = go.GetComponent<PageItemView>();
                        break;
                    }
                }
            }
            if (field != null)
                field.color2 = PreferencesCreator.IsEnabled ? activeColor : Color.white;
        }

        private static void SetupCancelButton()
        {
            if (M == null)
            {
                var all = GameObject.FindObjectsOfType<GameObject>(true);
                foreach (var go in all)
                {
                    if (go.name == "button_cancel")
                    {
                        M = go.GetComponent<PageElementView>();
                        break;
                    }
                }
            }
            if (M != null)
            {
                if (PreferencesCreator.IsEnabled)
                    M.color2 = Colors.Middle;
                else
                    M.color2 = new Color(1f, 0.2667f, 0.4824f, 0.749f);
            }
        }
    }
}
