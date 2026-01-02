/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using InfinityCode.UltimateEditorEnhancer.Attributes;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InfinityCode.UltimateEditorEnhancer.ComponentHeader
{
    [InitializeOnLoad]
    public class RuntimeSaveButton: ComponentHeaderItem<Component>
    {
        private const string FieldSeparator = "~«§";
        private static Dictionary<string, object> savedFields = new Dictionary<string, object>();

        protected override bool enabled => Prefs.saveComponentRuntime;
        public override float order => ComponentHeaderButtonOrder.SaveButton;

        static RuntimeSaveButton()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            EditorApplication.contextualPropertyMenu += OnAddMenuItems;
        }

        [MenuItem("CONTEXT/Component/Save Component", false, 502)]
        private static void ContextMenuAction(MenuCommand command)
        {
            SaveComponent(command.context as Component);
        }

        [MenuItem("CONTEXT/Component/Save Component", true)]
        private static bool ContextMenuValidate(MenuCommand command)
        {
            return ValidateInternal(command.context);
        }
        
        [MenuItem("CONTEXT/Component/Save Component On Exit Play Mode", false, 503)]
        private static void ContextMenuSaveOnExit(MenuCommand command)
        {
            Component component = command.context as Component;
            if (component == null) return;

            SaveOnExitPlayMode wrapper = component.gameObject.GetComponent<SaveOnExitPlayMode>();
            if (wrapper == null) wrapper = component.gameObject.AddComponent<SaveOnExitPlayMode>();
            
            if (!wrapper.saveComponents.Contains(component)) wrapper.saveComponents.Add(component);
        }
        
        [MenuItem("CONTEXT/Component/Save Component On Exit Play Mode", true)]
        private static bool ContextMenuSaveOnExitValidate(MenuCommand command)
        {
            return ValidateInternal(command.context);
        }

        protected override bool DrawButton(Rect rect, Component component)
        {
            if (GUI.Button(rect, EditorIconContents.saveActive, Styles.iconButton))
            {
                SaveComponent(component);
            }

            return true;
        }

        private static void OnAddMenuItems(GenericMenu menu, SerializedProperty property)
        {
            if (!EditorApplication.isPlaying) return;

            Component target = property.serializedObject.targetObject as Component;
            if (target == null) return;
            if (target.gameObject.scene.name == null) return;

            int instanceID = target.GetInstanceID();
            string path = property.propertyPath;

            SerializedProperty prop = property.Copy();

            menu.AddItem(TempContent.Get("Save Field Value"), false, () =>
            {
                savedFields[instanceID + FieldSeparator + path] = SerializedPropertyHelper.GetBoxedValue(prop);
            });
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                savedFields.Clear();
            }
            else if (state == PlayModeStateChange.EnteredEditMode)
            {
                RestoreSavedValues();
            }
            else if (state == PlayModeStateChange.ExitingPlayMode)
            {
                SaveOnExitPlayMode[] wrappers = ObjectHelper.FindObjectsOfType<SaveOnExitPlayMode>();
                foreach (SaveOnExitPlayMode wrapper in wrappers)
                {
                    foreach (Component component in wrapper.saveComponents)
                    {
                        if (component != null) SaveComponent(component, false);
                    }
                }
            }
        }

        private static void RestoreSavedValues()
        {
            Undo.SetCurrentGroupName("Set saved state");
            int group = Undo.GetCurrentGroup();

            foreach (KeyValuePair<string, object> pair in savedFields)
            {
                string[] parts = pair.Key.Split(new[]{FieldSeparator}, StringSplitOptions.None);
                int id;
                if (!int.TryParse(parts[0], out id)) continue;

                Object obj = Compatibility.EntityIdToObject(id);
                if (!obj) continue;

                Undo.RecordObject(obj, "Set saved state");
                SerializedObject so = new SerializedObject(obj);
                SerializedProperty prop = so.FindProperty(parts[1]);
                so.Update();
                SerializedPropertyHelper.SetBoxedValue(prop, pair.Value);
                so.ApplyModifiedProperties();
                EditorUtility.SetDirty(obj);
            }

            Undo.CollapseUndoOperations(@group);
        }

        private static void SaveComponent(Component component, bool log = true)
        {
            if (!component) return;
            SerializedObject so = new SerializedObject(component);
            SerializedProperty p = so.GetIterator();
            if (p.Next(true))
            {
                do
                {
                    savedFields[component.GetInstanceID() + FieldSeparator + p.propertyPath] = SerializedPropertyHelper.GetBoxedValue(p.Copy());
                } while (p.NextVisible(true));
            }

            if (log) Debug.Log($"{component.gameObject.name}/{ObjectNames.NicifyVariableName(component.GetType().Name)} component state saved.");
        }

        protected override bool Validate(Object target)
        {
            return ValidateInternal(target);
        }

        private static bool ValidateInternal(Object target)
        {
            if (!EditorApplication.isPlaying) return false;
            if (!Prefs.saveComponentRuntime) return false;
            Component component = target as Component;
            if (!component) return false;
            return component.gameObject.scene.name != null;
        }
    }
}
