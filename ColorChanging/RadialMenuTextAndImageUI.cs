using Il2CppTMPro;
using UnityEngine;

namespace Colorful
{
    public class RadialMenuTextAndImageUI
    {
        public static void RadialMenuTextAndImage(Transform parent)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);

                if (child.name == "text_Information")
                    continue;

                if (PreferencesCreator.IsEnabled)
                {
                    Color? color = ColorForIndex(i);
                    if (color.HasValue)
                        Paint(child, color.Value);
                }
                else
                {
                    Paint(child, Color.white);
                }

                RadialMenuTextAndImage(child);
            }
        }

        private static Color? ColorForIndex(int i)
        {
            if (i == 0 || i == 1)      return Colors.North;
            if (i == 3 || i == 4)      return Colors.NorthEast;
            if (i == 6 || i == 7)      return Colors.East;
            if (i == 9 || i == 10)     return Colors.SouthEast;
            if (i == 12 || i == 13)    return Colors.South;
            if (i == 15 || i == 16)    return Colors.SouthWest;
            if (i == 18 || i == 19)    return Colors.West;
            if (i == 21 || i == 22)    return Colors.NorthWest;
            return null;
        }

        private static void Paint(Transform t, Color color)
        {
            UnityEngine.UI.Image img = t.GetComponent<UnityEngine.UI.Image>();
            if (img != null)
                img.color = color;
            else
            {
                TextMeshProUGUI tmp = t.GetComponent<TextMeshProUGUI>();
                if (tmp != null)
                    tmp.color = color;
            }
        }
    }
}
