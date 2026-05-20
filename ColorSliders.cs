using BoneLib.BoneMenu;
using System;
using UnityEngine;

namespace Colorful
{
    internal static class ColorSliders
    {
        public static void Create(Page page, Color currentColor, Action<Color> applyCallback)
        {
            var colorPreview = page.CreateFunction("Preview", currentColor, null);

            page.CreateFloat("Red", Color.red, currentColor.r, 0.1f, 0f, 1f, (r) =>
            {
                currentColor.r = r;
                colorPreview.ElementColor = currentColor;
            });

            page.CreateFloat("Green", Color.green, currentColor.g, 0.1f, 0f, 1f, (g) =>
            {
                currentColor.g = g;
                colorPreview.ElementColor = currentColor;
            });

            page.CreateFloat("Blue", Color.blue, currentColor.b, 0.1f, 0f, 1f, (b) =>
            {
                currentColor.b = b;
                colorPreview.ElementColor = currentColor;
            });

            page.CreateFloat("Alpha", Color.gray, currentColor.a, 0.1f, 0f, 1f, (a) =>
            {
                currentColor.a = a;
                colorPreview.ElementColor = currentColor;
            });

            page.CreateFunction("Apply", Color.white, delegate ()
            {
                applyCallback(currentColor);
                Main.MoggingTime();
            });
        }

        public static void CreateWithConfirm(Page page, Color currentColor, Action<Color> applyCallback)
        {
            var colorPreview = page.CreateFunction("Preview", currentColor, null);

            page.CreateFloat("Red", Color.red, currentColor.r, 0.1f, 0f, 1f, (r) =>
            {
                currentColor.r = r;
                colorPreview.ElementColor = currentColor;
            });

            page.CreateFloat("Green", Color.green, currentColor.g, 0.1f, 0f, 1f, (g) =>
            {
                currentColor.g = g;
                colorPreview.ElementColor = currentColor;
            });

            page.CreateFloat("Blue", Color.blue, currentColor.b, 0.1f, 0f, 1f, (b) =>
            {
                currentColor.b = b;
                colorPreview.ElementColor = currentColor;
            });

            page.CreateFloat("Alpha", Color.gray, currentColor.a, 0.1f, 0f, 1f, (a) =>
            {
                currentColor.a = a;
                colorPreview.ElementColor = currentColor;
            });

            page.CreateFunction("Apply", Color.white, () =>
            {
                Menu.DisplayDialog("Override All Colors", "Are you sure? This will override all colors you've set",
                    confirmAction: () => applyCallback(currentColor));
            });
        }
    }
}
