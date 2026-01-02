/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using InfinityCode.UltimateEditorEnhancer.InspectorTools;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.ComponentHeader
{
    public class CopyPaste: ComponentHeaderItem<Component>
    {
        private static GUIContent content;

        protected override bool enabled => Prefs.headerCopyPaste;
        public override float order => ComponentHeaderButtonOrder.CopyPaste;

        protected override bool DrawButton(Rect rect, Component component)
        {
            rect.xMin += 1;
            rect.xMax -= 1;
            rect.yMin += 1;
            rect.yMax -= 1;

            ButtonEvent buttonEvent = GUILayoutUtils.Button(rect, content, GUIStyle.none);
            if (buttonEvent != ButtonEvent.click) return true;
            
            Event e = Event.current;
            if (e.button == 0)
            {
                if (!component) return false;
                UnityEditorInternal.ComponentUtility.CopyComponent(component);
            }
            else if (e.button == 1)
            {
                ShowContextMenu(component);
            }

            return true;
        }

        protected override void Initialize()
        {
            content = new GUIContent(Icons.copy, "Left Click - Copy Component.\nRight Click - Paste Component and Advanced Options.");
        }

        private void ShowContextMenu(Component component)
        {
            GenericMenuEx menu = GenericMenuEx.Start();
            
            menu.Add("Copy Component", () =>
            {
                UnityEditorInternal.ComponentUtility.CopyComponent(component);
            });

            menu.Add("Paste Component Values", () =>
            {
                UnityEditorInternal.ComponentUtility.PasteComponentValues(component);
            });
            
            menu.Add("Paste Component As New", () =>
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(component.gameObject);
            });

            menu.AddSeparator();
            
            menu.Add("Copy Component As JSON", () =>
            {
                string json = ComponentExporter.GetJsonString(component);
                EditorGUIUtility.systemCopyBuffer = json;
            });
            
            if (ComponentExporter.ValidateJson(EditorGUIUtility.systemCopyBuffer))
            {
                menu.Add("Paste Component From JSON", () =>
                {
                    string json = EditorGUIUtility.systemCopyBuffer;
                    ComponentExporter.SetComponentJson(component, json);
                });
            }
            else
            {
                menu.AddDisabled("Paste Component From JSON");
            }
            
            menu.Add("Export Component To JSON", () =>
            {
                string json = ComponentExporter.GetJsonString(component);
                if (string.IsNullOrEmpty(json)) return;
            
                string path = EditorUtility.SaveFilePanel("Export Component", "Assets", component.GetType().Name + ".json", "json");
                if (string.IsNullOrEmpty(path)) return;
            
                System.IO.File.WriteAllText(path, json);
            });
            
            menu.Add("Import Component From JSON", () =>
            {
                string path = EditorUtility.OpenFilePanel("Import Component", "Assets", "json");
                if (string.IsNullOrEmpty(path)) return;
            
                string json = System.IO.File.ReadAllText(path);
                ComponentExporter.SetComponentJson(component, json);
            });
            
            menu.ShowAsContext();
        }
    }
}