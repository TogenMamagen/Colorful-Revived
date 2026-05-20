using Il2CppSLZ.Bonelab;
using UnityEngine;

namespace Colorful
{
    public static class InventorySlotsUI
    {
        public static void Paint(Transform parent, Color color)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);

                PageElementView view = child.GetComponent<PageElementView>();
                if (view != null)
                    view.color2 = color;
                else
                {
                    MeshRenderer mesh = child.GetComponent<MeshRenderer>();
                    if (mesh != null)
                        mesh.material.color = color;
                }

                Paint(child, color);
            }
        }
    }
}
