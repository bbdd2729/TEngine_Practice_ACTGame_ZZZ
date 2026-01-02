/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using InfinityCode.UltimateEditorEnhancer.Attributes;
using InfinityCode.UltimateEditorEnhancer.ComponentHeader;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.InspectorTools
{
    [InitializeOnLoad]
    public static class ComponentHeaderItemDrawer
    {
        static ComponentHeaderItemDrawer()
        {
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            IList editorHeaderItemsMethods = EditorGUIUtilityRef.editorHeaderItemsMethodsField.GetValue(null) as IList;
            if (editorHeaderItemsMethods == null) return;
            
            EditorApplication.update -= Update;

            var targets = TypeCache
                .GetTypesDerivedFrom<ComponentHeaderItem>()
                .Where(t => !t.IsAbstract)
                .Select(t => Activator.CreateInstance(t))
                .OrderBy(t => (t as ComponentHeaderItem).order);

            MethodInfo method = typeof(ComponentHeaderItem).GetMethod("Draw", Reflection.InstanceLookup);

            Type headerItemDelegate = Reflection.GetEditorType("EditorGUIUtility+HeaderItemDelegate");
            
            foreach (object target in targets)
            {
                Delegate del = Delegate.CreateDelegate(headerItemDelegate, target, method);
                editorHeaderItemsMethods.Add(del);
            }

            EditorGUIUtilityRef.editorHeaderItemsMethodsField.SetValue(null, editorHeaderItemsMethods);

            GUI.changed = true;
        }
    }
}