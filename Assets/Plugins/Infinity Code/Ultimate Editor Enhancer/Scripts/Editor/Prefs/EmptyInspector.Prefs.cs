/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using InfinityCode.UltimateEditorEnhancer.InspectorTools;
using InfinityCode.UltimateEditorEnhancer.JSON;
using InfinityCode.UltimateEditorEnhancer.References;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer
{
    public static partial class Prefs
    {
        public static bool emptyInspector = true;
        public static bool emptyInspectorShowUpdates = true;

        public class EmptyInspectorManager : StandalonePrefManager<EmptyInspectorManager>
        {
            private SerializedObject serializedObject;
            private SerializedProperty elementsProperty;

            public override IEnumerable<string> keywords
            {
                get
                {
                    return new[]
                    {
                        "Empty Inspector",
                    };
                }
            }

            public static JsonArray json
            {
                get
                {
                    JsonArray jArr = new JsonArray();

                    foreach (EmptyInspector.Group item in EmptyInspectorReferences.items)
                    {
                        jArr.Add(item.json);
                    }
                    
                    return jArr;
                }
                set
                {
                    if (EmptyInspectorReferences.count > 0)
                    {
                        if (!EditorUtility.DisplayDialog("Import Empty Inspector", "Empty Inspector already contain items", "Replace", "Ignore"))
                        {
                            return;
                        }
                    }

                    EmptyInspectorReferences.items = value.Deserialize<EmptyInspector.Group[]>();
                }
            }

            public override void Draw()
            {
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                if (serializedObject == null)
                {
                    serializedObject = new SerializedObject(EmptyInspectorReferences.instance);
                    if (serializedObject != null)
                    {
                        elementsProperty = serializedObject.FindProperty("_items");
                        if (elementsProperty != null) elementsProperty.isExpanded = true; 
                    }
                }

                emptyInspector = EditorGUILayout.ToggleLeft("Empty Inspector", emptyInspector);
                emptyInspectorShowUpdates = EditorGUILayout.ToggleLeft("Show Updates", emptyInspectorShowUpdates);
                
                if (GUILayout.Button("Reimport Items From Window Menu", GUILayout.ExpandWidth(false)))
                {
                    RecordUpgrader.InitDefaultEmptyInspectorItems();
                    EmptyInspector.ResetCachedItems();
                    EmptyInspectorReferences.Save();
                }

                if (elementsProperty != null)
                {
                    serializedObject.Update();

                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(elementsProperty);
                    bool isDirty = EditorGUI.EndChangeCheck();
                    
                    serializedObject.ApplyModifiedProperties();

                    if (isDirty) EmptyInspector.ResetCachedItems();
                }


                EditorGUILayout.EndScrollView();
            }

            public override void SetState(bool state)
            {
                base.SetState(state);
                
                emptyInspector = state;
            }
        }
    }
}