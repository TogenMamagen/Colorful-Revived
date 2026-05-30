using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Colorful
{
    public class PreferencesUI
    {
        public static void Preferences(Transform parent)
        {
            Color color = PreferencesCreator.IsEnabled ? Colors.East : Color.white;

            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);

                var skipNames = new[] { "Viewport_Spectator", "Viewport_Graphics", "Viewport", "Name", "image_bgFade" };
                if (ContainsName(skipNames, child.name))
                    continue;

                if (child.name.Contains("canvas_FusionMenu") || child.name.Contains("[BoneMenu]"))
                    continue;

                Image img = child.GetComponent<Image>();
                if (img != null)
                    img.color = color;
                else
                {
                    TextMeshProUGUI tmp = child.GetComponent<TextMeshProUGUI>();
                    if (tmp != null)
                        tmp.color = color;
                    else
                    {
                        RawImage raw = child.GetComponent<RawImage>();
                        if (raw != null)
                            raw.color = color;
                        else
                        {
                            TextMeshPro tmp2 = child.GetComponent<TextMeshPro>();
                            if (tmp2 != null)
                                tmp2.color = color;
                            else
                            {
                                Text legacy = child.GetComponent<Text>();
                                if (legacy != null)
                                    legacy.color = color;
                            }
                        }
                    }
                }

                Preferences(child);
            }
        }

        public static void Extra(Transform parent)
        {
            Color color = PreferencesCreator.IsEnabled ? Colors.East : Color.white;

            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);

                Image img = child.GetComponent<Image>();
                if (img != null)
                    img.color = color;
                else
                {
                    TextMeshProUGUI tmp = child.GetComponent<TextMeshProUGUI>();
                    if (tmp != null)
                        tmp.color = color;
                }

                Extra(child);
            }
        }

        private static bool ContainsName(string[] names, string name)
        {
            foreach (string n in names)
            {
                if (n == name)
                    return true;
            }
            return false;
        }
    }
}
