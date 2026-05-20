using Il2CppTMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Melon_Loader_Mod5
{
    public class MenuUI
    {
        private static readonly HashSet<string> NoRecurse = new HashSet<string>
        {
            "group_Options",
            "group_CAMPAIGNS",
            "group_BETA",
        };

        public static void MainMenu(Transform parent)
        {
            Color color = PreferencesCreator.IsEnabled ? Colors.Menu : Color.white;

            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);

                if (child.name == "Background")
                    continue;

                if (child.name.Contains("img_outline") || child.name.Contains("img_bg"))
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
                        TextMeshPro tmp2 = child.GetComponent<TextMeshPro>();
                        if (tmp2 != null)
                            tmp2.color = color;
                        else
                        {
                            RawImage raw = child.GetComponent<RawImage>();
                            if (raw != null)
                                raw.color = color;
                        }
                    }
                }

                if (!NoRecurse.Contains(child.name))
                    MainMenu(child);
            }
        }
    }
}
