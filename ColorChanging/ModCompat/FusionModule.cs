using HarmonyLib;
using MelonLoader;
using System.Reflection;
using UnityEngine;

namespace Colorful
{
    public class FusionModule : ModModule
    {
        public override string DisplayName => "Fusion Menu";
        public override string AssemblyName => "LabFusion";
        public override string PrefEntryName => "Fusion Menu Color";
        public override Color DefaultColor => Color.white;

        private bool _patched;
        private static FusionModule _instance;
        private UnityEngine.UI.Image _cachedNavBackline;

        public FusionModule()
        {
            _instance = this;
        }

        public override void Apply()
        {
            GameObject canvas = null;
            var all = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (var go in all)
            {
                if (go.name.StartsWith("canvas_FusionMenu"))
                {
                    canvas = go;
                    break;
                }
            }
            if (canvas != null)
                FusionUI.Paint(canvas.transform, Color);
            PaintFusionNavButton();

            ModModuleManager.SetDividerLineColor(Color);
        }

        public override void ApplyPatches()
        {
            if (_patched) return;

            foreach (var asm in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (var type in asm.GetExportedTypes())
                    {
                        if (type.Name.Contains("MenuPage"))
                            TryPatchEnable(type);
                    }
                }
                catch { }
            }

            foreach (var melon in MelonMod.RegisteredMelons)
            {
                try
                {
                    var asm = melon.MelonAssembly?.Assembly;
                    if (asm == null) continue;
                    foreach (var type in asm.GetExportedTypes())
                    {
                        if (type.Name.Contains("MenuPage"))
                            TryPatchEnable(type);
                    }
                }
                catch { }
            }
        }

        private void TryPatchEnable(System.Type type)
        {
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var onEnable = type.GetMethod("OnEnable", flags);
            if (onEnable == null) return;

            var harmony = new HarmonyLib.Harmony($"Colorful.Fusion.{type.FullName}");
            var postfix = new HarmonyMethod(typeof(FusionModule), nameof(OnMenuEnabled));
            harmony.Patch(onEnable, postfix: postfix);
            _patched = true;
        }

        private static void OnMenuEnabled(MonoBehaviour __instance)
        {
            if (!PreferencesCreator.IsEnabled || __instance == null || _instance == null)
                return;

            Transform t = __instance.transform;
            while (t != null)
            {
                if (t.name.StartsWith("canvas_FusionMenu"))
                {
                    FusionUI.Paint(t, _instance.Color);
                    _instance.PaintFusionNavButton();
                    ModModuleManager.SetDividerLineColor(_instance.Color);
                    return;
                }
                t = t.parent;
            }
        }

        public override void OnMoggingTime(GameObject obj)
        {
            if (obj.name.StartsWith("canvas_FusionMenu"))
                FusionUI.Paint(obj.transform, PreferencesCreator.IsEnabled ? Color : Color.white);

            if (obj.name.Contains("button_Fusion"))
                PaintFusionNavButton();

            if (obj.name == "image----------------")
            {
                var img = obj.GetComponent<UnityEngine.UI.Image>();
                if (img != null)
                    img.color = Color;
            }
        }

        private void PaintFusionNavButton()
        {
            if (_cachedNavBackline == null)
            {
                var btn = GameObject.Find("button_Fusion");
                if (btn == null) return;
                var bl = btn.transform.Find("image_backline");
                if (bl == null) return;
                _cachedNavBackline = bl.GetComponent<UnityEngine.UI.Image>();
            }

            if (_cachedNavBackline != null)
                _cachedNavBackline.color = Color;
        }

    }
}
