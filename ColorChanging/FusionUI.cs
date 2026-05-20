using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Melon_Loader_Mod5
{
    public static class FusionUI
    {
        public static void Paint(Transform parent, Color color)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                TryPaint(child, color);
                Paint(child, color);
            }
        }

        private static void TryPaint(Transform t, Color targetColor)
        {
            TextMeshProUGUI tmp = t.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                if (!IsNameTagPreview(t) || IsWhite(tmp.color))
                    tmp.color = targetColor;
                return;
            }

            TextMeshPro tmp2 = t.GetComponent<TextMeshPro>();
            if (tmp2 != null)
            {
                tmp2.color = targetColor;
                return;
            }

            Text text = t.GetComponent<Text>();
            if (text != null)
            {
                text.color = targetColor;
                return;
            }

            Image img = t.GetComponent<Image>();
            if (img != null && IsWhite(img.color))
            {
                img.color = targetColor;
                return;
            }

            RawImage raw = t.GetComponent<RawImage>();
            if (raw != null && IsWhite(raw.color) && !IsProfilePicture(raw))
            {
                raw.color = targetColor;
                return;
            }
        }

        private static bool IsWhite(Color c)
        {
            return c.r > 0.9f && c.g > 0.9f && c.b > 0.9f;
        }

        private static bool IsNameTagPreview(Transform t)
        {
            Transform p = t;
            while (p != null)
            {
                if (p.name == "NameTag Color")
                    return true;
                p = p.parent;
            }
            return false;
        }

        private static bool IsProfilePicture(RawImage raw)
        {
            Transform p = raw.transform.parent;
            while (p != null)
            {
                if (p.GetComponent<Mask>() != null)
                    return raw.texture != null;
                p = p.parent;
            }
            return false;
        }
    }
}
