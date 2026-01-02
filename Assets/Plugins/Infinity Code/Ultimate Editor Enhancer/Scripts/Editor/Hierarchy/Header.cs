/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using System.Linq;
using InfinityCode.UltimateEditorEnhancer.JSON;
using InfinityCode.UltimateEditorEnhancer.References;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.HierarchyTools
{
    [InitializeOnLoad]
    public static class Header
    {
        static Header()
        {
            HierarchyItemDrawer.Register(nameof(Header), OnHierarchyItem, HierarchyToolOrder.Header);
        }

        public static JsonArray json
        {
            get
            {
                JsonArray jArr = new JsonArray();

                foreach (HeaderRule item in HeaderRuleReferences.items)
                {
                    jArr.Add(item.json);
                }

                return jArr;
            }
            set
            {
                if (HeaderRuleReferences.count > 0)
                {
                    if (!EditorUtility.DisplayDialog("Import Hierarchy Headers", "Hierarchy Headers already contain items", "Replace", "Ignore"))
                    {
                        return;
                    }
                }

                HeaderRuleReferences.items = value.Deserialize<HeaderRule[]>();
            }
        }

        [MenuItem("GameObject/Create Header", priority = 1)]
        public static GameObject Create()
        {
            GameObject go = new GameObject(Prefs.hierarchyHeaderPrefix + "Header");
            go.tag = "EditorOnly";
            GameObject active = Selection.activeGameObject;
            if (active)
            {
                go.transform.SetParent(active.transform.parent);
                go.transform.SetSiblingIndex(active.transform.GetSiblingIndex());
            }
            Undo.RegisterCreatedObjectUndo(go, go.name);
            Selection.activeGameObject = go;
            return go;
        }

        private static void OnHierarchyItem(HierarchyItem item)
        {
            if (!Prefs.hierarchyHeaders) return;

            GameObject go = item.gameObject;
            if (!go) return;

            HeaderRule rule = HeaderRuleReferences.FirstOrDefault(r => r.Validate(go));
            if (rule == null) return;

            DrawRule(item, rule);
            HierarchyItemDrawer.StopCurrentRowGUI();
        }

        private static void DrawRule(HierarchyItem item, HeaderRule rule)
        {
            if (Event.current.type != EventType.Repaint) return;

            bool hasChildren = item.gameObject.transform.childCount > 0;
            
            int textPadding = hasChildren? (int)item.rect.x - 30 : 8;
            rule.Draw(item, textPadding);
            
            if (!hasChildren) return;
            
            bool isExpanded = HierarchyHelper.IsExpanded(item.id);
            Rect r = new Rect(item.rect);
            r.width = 16;
            r.x -= 14;
            EditorStyles.foldout.Draw(r, GUIContent.none, -1, isExpanded);
        }
    }
}