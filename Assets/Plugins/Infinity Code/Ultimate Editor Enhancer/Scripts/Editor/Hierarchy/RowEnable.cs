/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.HierarchyTools
{
    [InitializeOnLoad]
    public static class RowEnable
    {
        static RowEnable()
        {
            HierarchyItemDrawer.Register(nameof(RowEnable), OnHierarchyItem, HierarchyToolOrder.RowEnable);
            HierarchyItemDrawer.Register(nameof(RowEnable) + "Middle", OnHierarchyItemMiddle, HierarchyToolOrder.RowEnable);
        }

        private static void OnHierarchyItemMiddle(HierarchyItem item)
        {
            if (!Prefs.hierarchyEnableMiddleClick) return;
            if (!item.gameObject || !item.hovered) return;

            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 2)
            {
                Undo.RecordObject(item.gameObject, "Modified Property in " + item.gameObject.name);
                item.gameObject.SetActive(!item.gameObject.activeSelf);
                EditorUtility.SetDirty(item.gameObject);
                e.Use();
            }
        }

        private static void OnHierarchyItem(HierarchyItem item)
        {
            if (!Prefs.hierarchyEnableGameObject) return;
            if (!item.gameObject || !item.hovered) return;

            Rect rect = item.rect;
            Rect r = new Rect(32, rect.y, 16, rect.height);
            
            if (Event.current.type == EventType.Repaint)
            {
                const string tooltip = "Click to toggle this GameObject active state.\nRight click to toggle all neighbors GameObjects active state.";
                GUI.Label(r, TempContent.Get(string.Empty, tooltip), GUIStyle.none);
            }

            EditorGUI.BeginChangeCheck();
            bool v = EditorGUI.Toggle(r, GUIContent.none, item.gameObject.activeSelf);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(item.gameObject, "Modified Property in " + item.gameObject.name);
                item.gameObject.SetActive(v);
                EditorUtility.SetDirty(item.gameObject);
            }

            Event e = Event.current;
            if (e.type == EventType.MouseUp && r.Contains(e.mousePosition))
            {
                if (e.button == 1)
                {
                    e.Use();
                    ToggleOtherEnabled(item.gameObject);
                }
            }
        }

        private static void SetOtherEnabled(GameObject gameObject, bool value)
        {
            Undo.SetCurrentGroupName("Modified Other Enabled in " + gameObject.name);
            int group = Undo.GetCurrentGroup();

            foreach (GameObject sibling in GameObjectUtils.GetSiblings(gameObject))
            {
                Undo.RecordObject(sibling, "Modified Property in " + sibling.name);
                sibling.SetActive(value);
                EditorUtility.SetDirty(sibling);
            }
            
            Undo.CollapseUndoOperations(group);
        }

        private static void ToggleOtherEnabled(GameObject target)
        {
            bool isOtherOtherEnabled = GameObjectUtils.GetSiblings(target).Any(s => s.activeSelf);
            Debug.Log(isOtherOtherEnabled);
            SetOtherEnabled(target, !isOtherOtherEnabled);
        }
    }
}