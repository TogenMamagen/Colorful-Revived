using BoneLib.BoneMenu.UI;
using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Colorful
{
    public static class BoneMenuUI
    {
        public static void Paint()
        {
            var guiMenu = FindGuiMenu();
            if (guiMenu == null)
                return;

            Color color = PreferencesCreator.IsEnabled ? Colors.Menu : Color.white;
            PaintRecursive(guiMenu.transform, color);
        }

        private static GUIMenu FindGuiMenu()
        {
            var menu = GameObject.FindObjectOfType<GUIMenu>();
            if (menu != null) return menu;

            var canvas = GameObject.Find("[BoneMenu] - Canvas(Clone)");
            if (canvas != null)
                return canvas.GetComponent<GUIMenu>();

            return null;
        }

        private static void PaintRecursive(Transform parent, Color targetColor)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);

                if (child.name == "Keyboard")
                    continue;

                TryPaint(child, targetColor);
                PaintRecursive(child, targetColor);
            }
        }

        private static void TryPaint(Transform t, Color targetColor)
        {
            TextMeshProUGUI tmp = t.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                if (IsWhite(tmp.color))
                    tmp.color = targetColor;
                return;
            }

            TextMeshPro tmp2 = t.GetComponent<TextMeshPro>();
            if (tmp2 != null)
            {
                if (IsWhite(tmp2.color))
                    tmp2.color = targetColor;
                return;
            }

            Text text = t.GetComponent<Text>();
            if (text != null)
            {
                if (IsWhite(text.color))
                    text.color = targetColor;
                return;
            }

            Image img = t.GetComponent<Image>();
            if (img != null)
            {
                img.color = targetColor;
                return;
            }

            RawImage raw = t.GetComponent<RawImage>();
            if (raw != null)
            {
                raw.color = targetColor;
                return;
            }
        }

        private static bool IsWhite(Color c)
        {
            return c.r > 0.9f && c.g > 0.9f && c.b > 0.9f;
        }
    }
}
