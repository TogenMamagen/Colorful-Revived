using BoneLib.BoneMenu;
using MelonLoader;
using UnityEngine;

namespace Melon_Loader_Mod5
{
    public abstract class ModModule
    {
        public abstract string DisplayName { get; }
        public abstract string AssemblyName { get; }
        public abstract string PrefEntryName { get; }
        public abstract Color DefaultColor { get; }

        public bool IsLoaded { get; private set; }
        public Color Color { get; set; }
        public MelonPreferences_Entry<Color> ColorPref { get; private set; }
        public Page BoneMenuPage { get; set; }

        public bool Detect()
        {
            foreach (var asm in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.GetName().Name == AssemblyName)
                {
                    IsLoaded = true;
                    return true;
                }
            }
            return false;
        }

        public void SetupPref(MelonPreferences_Category category)
        {
            if (!IsLoaded) return;
            ColorPref = category.CreateEntry(PrefEntryName, DefaultColor);
            Color = ColorPref.Value;
        }

        public void LoadFromPrefs()
        {
            if (!IsLoaded) return;
            Color = ColorPref != null ? ColorPref.Value : DefaultColor;
        }

        public Page CreateBoneMenuPage(Page parent)
        {
            if (!IsLoaded) return null;
            BoneMenuPage = parent.CreatePage(DisplayName, Color);
            ColorSliders.Create(BoneMenuPage, Color, updatedColor =>
            {
                SetColor(updatedColor);
            });
            return BoneMenuPage;
        }

        public virtual void SetColor(Color color)
        {
            Color = color;
            if (BoneMenuPage != null)
                BoneMenuPage.Color = color;
            if (ColorPref != null)
            {
                ColorPref.Value = color;
                PreferencesCreator.MelonPrefCategory.SaveToFile();
            }
            Apply();
        }

        public virtual void ResetToDefault()
        {
            SetColor(DefaultColor);
        }

        public virtual void Apply() { }

        public virtual void ApplyPatches() { }

        public virtual void OnSceneAwake() { }

        public virtual void OnMoggingTime(GameObject obj) { }
    }
}
