using HarmonyLib;
using Il2CppSLZ.Bonelab;
using Il2CppSLZ.UI;
using MelonLoader;
using System.Reflection;
using UnityEngine;

namespace Colorful
{
    internal static class PanelPatches
    {
        public static void Apply()
        {
            TryPatch<LevelsPanelView>("OnEnable");
            TryPatch<AvatarsPanelView>("OnEnable");
        }

        private static bool _searchThingPatched;

        public static void ApplySearchThingPatches()
        {
            if (_searchThingPatched) return;
            _searchThingPatched = true;

            TryPatchByName("SearchThing.Extensions.SpawnablePanelExtension", "Show");
            TryPatchByName("SearchThing.Extensions.SpawnablePanelExtension", "RenderAll");
        }

        private static void TryPatch<T>(string methodName)
        {
            var flags = BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.Instance;
            var method = typeof(T).GetMethod(methodName, flags);

            if (method == null)
            {
                MelonLogger.Warning($"Could not find {methodName} on {typeof(T).Name}");
                return;
            }

            var harmony = new HarmonyLib.Harmony($"Colorful.{typeof(T).Name}");
            var postfix = new HarmonyMethod(typeof(PanelPatches), nameof(OnPanelEnabled));
            harmony.Patch(method, postfix: postfix);

            MelonLogger.Msg($"Patched {typeof(T).Name}.{methodName}");
        }

        private static void TryPatchByName(string typeName, string methodName)
        {
            var type = FindTypeByName(typeName);
            if (type == null)
            {
                MelonLogger.Msg($"Type {typeName} not found, skipping {methodName} patch");
                return;
            }

            var flags = BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.Instance;
            var method = type.GetMethod(methodName, flags);

            if (method == null)
            {
                MelonLogger.Warning($"Could not find {methodName} on {typeName}");
                return;
            }

            var harmony = new HarmonyLib.Harmony($"Colorful.{typeName}");
            var postfix = new HarmonyMethod(typeof(PanelPatches), nameof(OnSearchThingShow));
            harmony.Patch(method, postfix: postfix);

            MelonLogger.Msg($"Patched {typeName}.{methodName}");
        }

        private static System.Type FindTypeByName(string typeName)
        {
            foreach (var asm in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = asm.GetType(typeName);
                if (type != null)
                    return type;
            }

            foreach (var melon in MelonMod.RegisteredMelons)
            {
                if (melon.MelonAssembly?.Assembly == null)
                    continue;
                var type = melon.MelonAssembly.Assembly.GetType(typeName);
                if (type != null)
                    return type;
            }

            return null;
        }

        private static void OnSearchThingShow(object __instance)
        {
            if (!PreferencesCreator.IsEnabled || __instance == null)
                return;

            var panelViewProp = __instance.GetType().GetProperty("PanelView");
            if (panelViewProp == null)
                return;

            var panelView = panelViewProp.GetValue(__instance) as UnityEngine.Component;
            if (panelView == null)
                return;

            SpawnGunUI.SpawnGun(panelView.transform);
        }

        private static void OnPanelEnabled(UnityEngine.MonoBehaviour __instance)
        {
            if (!PreferencesCreator.IsEnabled || __instance == null)
                return;

            var t = __instance.transform;

            if (__instance is LevelsPanelView)
                LevelSelectUI.LevelSelect(t);
            else if (__instance is AvatarsPanelView)
                AvatarSelectUI.Avatar(t);
        }
    }
}
