using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Colorful
{
    public static class InfoBoardUI
    {
        public static void Paint(Transform parent)
        {
            Color color = PreferencesCreator.IsEnabled ? Colors.Menu : Color.white;
            TransformPainter.Paint(parent, color);
        }

        public static void PaintLeaderboard(Transform parent)
        {
            Color color = PreferencesCreator.IsEnabled ? Colors.Menu : Color.white;
            Color darkBg = Color.Lerp(color, Color.black, 0.6f);

            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);

                Image img = child.GetComponent<Image>();
                if (img != null)
                    img.color = darkBg;
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
                    }
                }

                PaintLeaderboard(child);
            }
        }
    }
}
