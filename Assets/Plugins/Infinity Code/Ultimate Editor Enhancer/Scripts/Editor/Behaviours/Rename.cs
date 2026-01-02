/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using InfinityCode.UltimateEditorEnhancer.Windows;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InfinityCode.UltimateEditorEnhancer.Behaviors
{
    [InitializeOnLoad]
    public static class Rename
    {
        private const float HeightWithoutTokens = 86;
        public const float HeightWithTokens = 186;
        
        public static Object[] objects;

        private static Type[] _allowWindowTypes;
        private static int index;
        private static InputDialog dialog;
        private static bool showTokens = false;


        public static Type[] allowWindowTypes
        {
            get
            {
                if (_allowWindowTypes != null) return _allowWindowTypes;
                
                _allowWindowTypes = new[]
                {
                    typeof(SceneView),
                    InspectorWindowRef.type,
                    typeof(ComponentWindow),
                    typeof(LayoutWindow),
                    ConsoleWindowRef.type
                };

                return _allowWindowTypes;
            }
        }

        static Rename()
        {
            KeyManager.KeyBinding binding = KeyManager.AddBinding();
            binding.OnPress = OnInvoke;
        }

        private static void DrawExtra(InputDialog dialog)
        {
            index = int.MinValue;
            EditorGUILayout.LabelField("Preview: " + ReplaceTokens(objects[0], dialog.text));

            EditorGUI.BeginChangeCheck();
            showTokens = EditorGUILayout.Foldout(showTokens, "Tokens");
            if (EditorGUI.EndChangeCheck())
            {
                dialog.minSize = dialog.maxSize = new Vector2(dialog.minSize.x, showTokens ? HeightWithTokens : HeightWithoutTokens);
            }
            if (!showTokens) return;
            
            EditorGUILayout.LabelField("{N} - original name");
            EditorGUILayout.LabelField("{C} - counter");
            EditorGUILayout.LabelField("{C:N} - counter with initial number");
            EditorGUILayout.LabelField("{S} - sibling index for GameObjects in the scene");
            EditorGUILayout.LabelField("{START:LEN} - part of the original name");
        }

        public static void OnDialogClose(InputDialog dialog)
        {
            objects = null;
        }

        private static void OnInvoke()
        {
            Event e = Event.current;
            if (e.keyCode != KeyCode.F2) return;
            if (e.modifiers != EventModifiers.FunctionKey) return;
            if (!OnValidate()) return;

            Object[] targets;

            EditorWindow wnd = EditorWindow.focusedWindow;
            if (wnd.GetType() == typeof(ComponentWindow))
            {
                targets = new[]
                {
                    (wnd as ComponentWindow).component.gameObject
                };
            }
            else if (wnd.GetType() == typeof(PinAndClose))
            {
                ComponentWindow cw = (wnd as PinAndClose).targetWindow as ComponentWindow;
                targets = new[]
                {
                    cw.component.gameObject
                };
            }
            else targets = Selection.objects;

            Show(targets);
        }

        public static void OnRename(string name)
        {
            if (objects == null || objects.Length == 0) return;

            name = name.Trim();
            index = int.MinValue;

            Undo.RecordObjects(objects, "Rename GameObjects");
            foreach (Object obj in objects) RenameObject(obj, name);

            objects = null;
        }

        private static bool OnValidate()
        {
            if (!Prefs.renameByShortcut) return false;

            EditorWindow wnd = EditorWindow.focusedWindow;
            if (!wnd) return false;
            Type type = wnd.GetType();
            if (allowWindowTypes.Contains(type)) return true;
            
            if (Selection.objects.Length > 1)
            {
                if (type == SceneHierarchyWindowRef.type || type == ProjectBrowserRef.type) return true;
            }
            
            return (type == typeof(PinAndClose) && (wnd as PinAndClose).targetWindow is ComponentWindow);
        }

        private static void RenameObject(Object obj, string name)
        {
            name = ReplaceTokens(obj, name);
            GameObject go = obj as GameObject;
            if (go)
            {
                if (go.scene.name != null)
                {
                    go.name = name;
                    return;
                }
            }

            string path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path)) AssetDatabase.RenameAsset(path, name);
            else obj.name = name;
        }

        private static string ReplaceTokens(Object obj, string name)
        {
            return Regex.Replace(name, @"{[\w\d:-]+}", delegate (Match match)
            {
                string v = match.Value.Trim('{', '}');
                char firstChar = char.ToUpperInvariant(v[0]);
                if (firstChar == 'N') return obj.name;
                if (firstChar == 'C')
                {
                    if (index == int.MinValue)
                    {
                        if (v.Length > 2 && v[1] == ':')
                        {
                            int n;
                            if (int.TryParse(v.Substring(2), out n)) index = n;
                        }

                        if (index == int.MinValue) index = 1;
                    }
                    int i = index++;
                    return i.ToString();
                }

                if (obj is GameObject && firstChar == 'S') return (obj as GameObject).transform.GetSiblingIndex().ToString();

                string[] ss = v.Split(':');
                if (ss.Length >= 2)
                {
                    string original = obj.name;
                    int start = 0, len = 0;

                    if (!string.IsNullOrEmpty(ss[0]) && !int.TryParse(ss[0], out start)) return "";

                    if (start < 0) start = original.Length + start;

                    if (!string.IsNullOrEmpty(ss[1]))
                    {
                        if (!int.TryParse(ss[1], out len)) return "";
                        if (len < 0) len = original.Length - start + len;
                    }
                    else len = original.Length - start;

                    if (original.Length <= start) return "";
                    if (original.Length < start + len) len = original.Length - start;
                    return original.Substring(start, len);
                }

                return "";
            });
        }

        public static void Show(params Object[] targets)
        {
            if (targets == null || targets.Length == 0) return;

            objects = targets;
            
            dialog = InputDialog.Show("Enter a new GameObjects name", objects[0].name, OnRename);
            dialog.OnClose += OnDialogClose;
            dialog.OnDrawExtra += DrawExtra;
            dialog.maxSize = dialog.minSize = new Vector2(dialog.minSize.x, showTokens? HeightWithTokens : HeightWithoutTokens);
            index = int.MinValue;
        }
    }
}