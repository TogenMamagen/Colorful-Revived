using BoneLib.BoneMenu;
using MelonLoader;
using System.Collections.Generic;
using UnityEngine;

namespace Melon_Loader_Mod5
{
    public static class ModModuleManager
    {
        public static List<ModModule> Modules { get; } = new List<ModModule>();

        public static void Register(ModModule module)
        {
            Modules.Add(module);
        }

        public static void InitializeAll(MelonPreferences_Category category, Page root)
        {
            var modsPage = root.CreatePage("Mods", Color.white);

            foreach (var module in Modules)
            {
                if (!module.Detect())
                    continue;

                module.SetupPref(category);
                module.LoadFromPrefs();
                module.CreateBoneMenuPage(modsPage);
                module.ApplyPatches();
            }
        }

        public static void OnSceneAwakeAll()
        {
            foreach (var module in Modules)
            {
                if (!module.IsLoaded) continue;
                module.OnSceneAwake();
            }
        }

        public static void OnMoggingTimeAll(GameObject obj)
        {
            foreach (var module in Modules)
            {
                if (!module.IsLoaded) continue;
                module.OnMoggingTime(obj);
            }
        }

        public static void SetAllColors(Color color)
        {
            foreach (var module in Modules)
            {
                if (!module.IsLoaded) continue;
                module.SetColor(color);
            }
        }

        public static void ResetAll()
        {
            foreach (var module in Modules)
            {
                if (!module.IsLoaded) continue;
                module.ResetToDefault();
            }
        }

        public static T Get<T>() where T : ModModule
        {
            foreach (var module in Modules)
            {
                if (module is T t)
                    return t;
            }
            return null;
        }

        public static void SetDividerLineColor(Color color)
        {
            var objs = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (var obj in objs)
            {
                if (obj.name != "image----------------") continue;
                var p = obj.transform.parent;
                if (p != null && p.name == "panel_Preferences")
                {
                    var img = obj.GetComponent<UnityEngine.UI.Image>();
                    if (img != null)
                        img.color = color;
                    return;
                }
            }
        }
    }
}
