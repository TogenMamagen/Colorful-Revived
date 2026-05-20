using Il2CppTMPro;
using UnityEngine;

namespace Melon_Loader_Mod5
{
    public class AvatarSelectUI
    {
        public static void Avatar(Transform parent)
        {
            Color color = PreferencesCreator.IsEnabled ? Colors.NorthWest : Color.white;

            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);

                if (child.name == "img_outline" || child.name == "img_bg" || child.name == "image_bgFade")
                    continue;

                PaintChild(child, color);
                Avatar(child);
            }
        }

        public static void Bodymall(Transform parent)
        {
            Color color = PreferencesCreator.IsEnabled ? Colors.NorthWest : Color.white;
            Color darkText = Color.black;

            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);

                if (child.name == "img_outline" || child.name == "img_bg")
                    continue;

                if (child.name == "Chart")
                {
                    Renderer renderer = child.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        Material mat = renderer.material;
                        if (mat != null)
                            mat.color = color;
                    }
                    Bodymall(child);
                    continue;
                }

                bool isTabText = child.name == "text_avatars_val";
                PaintChild(child, isTabText ? darkText : color);
                Bodymall(child);
            }
        }

        private static void PaintChild(Transform t, Color color)
        {
            TextMeshProUGUI tmp = t.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
                tmp.color = color;
            else
            {
                TextMeshPro tmp2 = t.GetComponent<TextMeshPro>();
                if (tmp2 != null)
                    tmp2.color = color;
                else
                {
                    UnityEngine.UI.Image img = t.GetComponent<UnityEngine.UI.Image>();
                    if (img != null)
                        img.color = color;
                }
            }
        }
    }
}
