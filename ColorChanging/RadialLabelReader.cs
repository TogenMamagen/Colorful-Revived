using Il2CppTMPro;
using MelonLoader;
using UnityEngine;
using System.Collections;

namespace Colorful
{
    internal static class RadialLabelReader
    {
        private static readonly int[] TextButtonIndices = { 0, 3, 6, 9, 12, 15, 18, 21 };
        private static readonly string[] DefaultNames =
            { "Eject", "Level Select", "Preferences", "Quick Mute",
              "Inventory", "Spawn Devtools", "SpawnGun Menu", "Avatar Select" };

        public static string[] GetButtonNames()
        {
            string[] names = new string[8];
            GameObject canvas = GameObject.Find("CANVAS_RADIALUI");
            if (canvas != null)
            {
                Transform t = canvas.transform;
                for (int i = 0; i < 8; i++)
                {
                    int idx = TextButtonIndices[i];
                    if (idx < t.childCount)
                    {
                        TextMeshProUGUI tmp = t.GetChild(idx).GetComponent<TextMeshProUGUI>();
                        if (tmp != null && !string.IsNullOrEmpty(tmp.text))
                        {
                            names[i] = tmp.text;
                            continue;
                        }
                    }
                    names[i] = DefaultNames[i];
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                    names[i] = DefaultNames[i];
            }
            return names;
        }

        public static void StartPolling()
        {
            MelonCoroutines.Start(PollRadialText());
        }

        private static IEnumerator PollRadialText()
        {
            yield return new WaitForSeconds(1f);

            for (int attempt = 0; attempt < 30; attempt++)
            {
                GameObject canvas = GameObject.Find("CANVAS_RADIALUI");
                if (canvas != null)
                {
                    Transform t = canvas.transform;
                    bool anyText = false;
                    string[] names = new string[8];

                    for (int i = 0; i < 8; i++)
                    {
                        int idx = TextButtonIndices[i];
                        if (idx < t.childCount)
                        {
                            TextMeshProUGUI tmp = t.GetChild(idx).GetComponent<TextMeshProUGUI>();
                            if (tmp != null && !string.IsNullOrEmpty(tmp.text))
                            {
                                names[i] = tmp.text;
                                anyText = true;
                            }
                            else
                            {
                                names[i] = DefaultNames[i];
                            }
                        }
                        else
                        {
                            names[i] = DefaultNames[i];
                        }
                    }

                    if (anyText)
                    {
                        // Validate names before applying — reject known junk
                        for (int i = 0; i < 8; i++)
                        {
                            if (string.IsNullOrEmpty(names[i]) ||
                                names[i].IndexOf("COOL", System.StringComparison.OrdinalIgnoreCase) >= 0 ||
                                names[i].IndexOf("cool", System.StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                names[i] = DefaultNames[i];
                            }
                        }
                        PreferencesCreator.UpdateColorPageNames(names);
                        yield break;
                    }
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
