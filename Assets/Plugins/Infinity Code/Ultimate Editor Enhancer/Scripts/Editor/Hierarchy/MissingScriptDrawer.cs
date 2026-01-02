/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.HierarchyTools
{
    [InitializeOnLoad]
    public static class MissingScriptDrawer
    {
        private static Dictionary<int, bool> missingScripts = new Dictionary<int, bool>();
        
        static MissingScriptDrawer()
        {
            HierarchyItemDrawer.Register(nameof(MissingScriptDrawer), DrawHierarchyItem, HierarchyToolOrder.MissingScript);
        }
        
        private static bool CheckMissingScripts(GameObject gameObject)
        {
            if (!gameObject) return false;

            bool hasMissingScript = false;
            Component[] components = gameObject.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component) continue;
                
                hasMissingScript = true;
                break;
            }

            missingScripts[gameObject.GetInstanceID()] = hasMissingScript;
            return hasMissingScript;
        }
        
        private static void DrawHierarchyItem(HierarchyItem row)
        {
            if (!Prefs.hierarchyMissingComponents) return;
            if (!row.gameObject) return;
            
            if (!missingScripts.TryGetValue(row.id, out bool hasMissingScript)) hasMissingScript = CheckMissingScripts(row.gameObject);
            if (!hasMissingScript) return;
            
            Rect localRect = new Rect(row.rect);
            localRect.xMin = localRect.xMax - 20;
            row.rect.width -= 22;

            GUIContent content = TempContent.Get(Icons.missingComponent, "Has Missing Scripts");
            if (GUI.Button(localRect, content, GUIStyle.none))
            {
                Selection.activeGameObject = row.gameObject;
            }
        }
    }
}