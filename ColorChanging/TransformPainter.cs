using Il2CppTMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Melon_Loader_Mod5
{
    public static class TransformPainter
    {
        public static void Paint(Transform parent, Color color, HashSet<string> skipList = null)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);

                if (skipList != null && skipList.Contains(child.name))
                    continue;

                TryPaint(child, color);
                Paint(child, color, skipList);
            }
        }

        private static void TryPaint(Transform t, Color color)
        {
            Image img = t.GetComponent<Image>();
            if (img != null) { img.color = color; return; }

            TextMeshProUGUI tmp = t.GetComponent<TextMeshProUGUI>();
            if (tmp != null) { tmp.color = color; return; }

            TextMeshPro tmp2 = t.GetComponent<TextMeshPro>();
            if (tmp2 != null) { tmp2.color = color; return; }

            RawImage raw = t.GetComponent<RawImage>();
            if (raw != null) { raw.color = color; return; }

            Text text = t.GetComponent<Text>();
            if (text != null) { text.color = color; return; }
        }
    }
}
