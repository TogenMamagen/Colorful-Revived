using Il2CppTMPro;
using UnityEngine;

namespace Melon_Loader_Mod5
{
    public class LevelSelectUI
    {
        public static void LevelSelect(Transform parent)
        {
            Color color = PreferencesCreator.IsEnabled ? Colors.NorthEast : Color.white;

            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);

                if (child.name == "image_bgFade" || child.name == "Background")
                    continue;

                UnityEngine.UI.Image img = child.GetComponent<UnityEngine.UI.Image>();
                if (img != null)
                    img.color = color;
                else
                {
                    TextMeshProUGUI tmp = child.GetComponent<TextMeshProUGUI>();
                    if (tmp != null)
                        tmp.color = color;
                }

                LevelSelect(child);
            }
        }
    }
}
