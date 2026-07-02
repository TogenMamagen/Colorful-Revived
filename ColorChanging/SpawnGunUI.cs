using Il2CppTMPro;
using UnityEngine;

namespace Colorful
{
    public class SpawnGunUI
    {
        private static bool _painted;

        public static void ResetPaint()
        {
            _painted = false;
        }

        public static void SpawnGun(Transform parent)
        {
            if (_painted)
                return;
            _painted = true;

            Color color = PreferencesCreator.IsEnabled ? Colors.West : Color.white;

            foreach (var img in parent.GetComponentsInChildren<UnityEngine.UI.Image>(true))
            {
                if (img.name == "Background" || img.name == "image_bgFade")
                    continue;
                if (img.name == "image_backline" && img.transform.parent != null && img.transform.parent.name == "group_selectedInfo")
                    continue;
                img.color = color;
            }
            foreach (var tmp in parent.GetComponentsInChildren<TextMeshProUGUI>(true))
                tmp.color = color;
            foreach (var tmp in parent.GetComponentsInChildren<TextMeshPro>(true))
                tmp.color = color;
        }
    }
}
