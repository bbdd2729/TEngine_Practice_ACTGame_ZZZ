/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.HierarchyTools
{
    [InitializeOnLoad]
    public static class DropCopyComponent
    {
        static DropCopyComponent()
        {
            HierarchyItemDrawer.Register(nameof(DropCopyComponent), OnHierarchyItem, HierarchyToolOrder.DropCopyComponent);
        }

        private static void OnHierarchyItem(HierarchyItem row)
        {
            if (!Prefs.hierarchyDropCopyComponent) return;
            if (!row.hovered) return;

            Event e = Event.current;
            if (e.type != EventType.DragPerform && e.type != EventType.DragUpdated) return;
            if (e.modifiers != EventModifiers.Control && e.modifiers != EventModifiers.Command) return;
            if (DragAndDrop.objectReferences.Length != 1) return;
            
            Component c = DragAndDrop.objectReferences[0] as Component;
            if (!c) return;
            
            if (e.type == EventType.DragUpdated)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                e.Use();
            }
            else
            {
                DragAndDrop.AcceptDrag();
                e.Use();

                GameObject go = row.gameObject;

                Undo.AddComponent(go, c.GetType());
                Component newComponent = go.GetComponent(c.GetType());
                EditorUtility.CopySerialized(c, newComponent);
            }
        }
    }
}