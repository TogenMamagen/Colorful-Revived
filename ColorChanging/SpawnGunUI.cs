using Il2CppTMPro;
using UnityEngine;

namespace Colorful
{
    public class SpawnGunUI
    {
        public static void SpawnGun(Transform parent)
        {
            Color color = PreferencesCreator.IsEnabled ? Colors.West : Color.white;

            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);

                if (child.name == "Background")
                    continue;

                if (child.name == "image_backline" && child.parent.gameObject.name == "group_selectedInfo")
                    continue;

                if (child.name == "image_bgFade")
                    continue;

                UnityEngine.UI.Image img = child.GetComponent<UnityEngine.UI.Image>();
                if (img != null)
                    img.color = color;
                else
                {
                    TextMeshProUGUI tmp = child.GetComponent<TextMeshProUGUI>();
                    if (tmp != null)
                        tmp.color = color;
                    else
                    {
                        TextMeshPro tmp2 = child.GetComponent<TextMeshPro>();
                        if (tmp2 != null)
                            tmp2.color = color;
                    }
                }

                SpawnGun(child);
            }
        }
    }
}
