/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using InfinityCode.UltimateEditorEnhancer.HierarchyTools;
using InfinityCode.UltimateEditorEnhancer.Interceptors;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InfinityCode.UltimateEditorEnhancer
{
    public static partial class Prefs
    {
        public static bool hierarchyBookmarks = true;
        public static bool hierarchyDropCopyComponent = true;
        public static bool hierarchyDropScriptToCreateGameObject = true;
        public static bool hierarchyEnableGameObject = true;
        public static bool hierarchyEnableMiddleClick = true;
        public static bool hierarchyErrorIcons = true;
        public static bool hierarchyIcons = true;
        public static bool hierarchyIconsHideDefault = false;
        public static HierarchyIconsDisplayRule hierarchyIconsDisplayRule = HierarchyIconsDisplayRule.onHoverWithModifiers;
        public static bool hierarchyMissingComponents = true;
        public static bool hierarchyNote = true;
        public static bool hierarchyOddEven = true;
        public static bool hierarchyOverrideMainIcon = true;
        public static bool hierarchySoloPickability = true;
        public static bool hierarchySoloVisibility = true;
        public static float hierarchyMarginRight = 0;

        public static bool hierarchyTree = true;

#if !UNITY_EDITOR_OSX
        public static EventModifiers hierarchyIconsModifiers = EventModifiers.Control;
#else
        public static EventModifiers hierarchyIconsModifiers = EventModifiers.Command;
#endif
        public static int hierarchyIconsMaxItems = 6;

        public class HierarchyManager : StandalonePrefManager<HierarchyManager>, IHasShortcutPref, IStateablePref
        {
            public override IEnumerable<string> keywords
            {
                get 
                { 
                    return new[] 
                    { 
                        "Drop Copy Component",
                        "Enable / Disable GameObject",
                        "Filter by Type",
                        "Hierarchy Icons",
                        "Icon Right Margin",
                        "Note",
                        "Max Items",
                        "Missing Components",
                        "Odd Even",
                        "Show Bookmark Button",
                        "Show Bookmark Button",
                        "Show Best Component Icon Before Name",
                        "Show Components In Hierarchy",
                        "Show Error Icon When GameObject Has an Error or Exception",
                        "Solo Pickability",
                        "Solo Visibility",
                        "Tree",
                    };
                }
            }

            public override void Draw()
            {
                DrawBestComponents();
                
                hierarchyBookmarks = EditorGUILayout.ToggleLeft("Bookmark Button", hierarchyBookmarks);
                
                DrawComponentButtons();
                
                hierarchyDropCopyComponent = EditorGUILayout.ToggleLeft("Drop Copy Component", hierarchyDropCopyComponent);
                hierarchyDropScriptToCreateGameObject = EditorGUILayout.ToggleLeft("Drop Script To Create GameObject", hierarchyDropScriptToCreateGameObject);
                hierarchyEnableGameObject = EditorGUILayout.ToggleLeft("Enable / Disable GameObject", hierarchyEnableGameObject);
                hierarchyEnableMiddleClick = EditorGUILayout.ToggleLeft("Enable / Disable By Middle Click", hierarchyEnableMiddleClick);
                hierarchyErrorIcons = EditorGUILayout.ToggleLeft("Error Icon When GameObject Has an Error or Exception", hierarchyErrorIcons);
                
                DrawFilterByType();

                hierarchyMarginRight = EditorGUILayout.FloatField("Icon Right Margin", hierarchyMarginRight);
                hierarchyMissingComponents = EditorGUILayout.ToggleLeft("Missing Components", hierarchyMissingComponents);
                hierarchyNote = EditorGUILayout.ToggleLeft("Note Icon", hierarchyNote);
                hierarchyOddEven = EditorGUILayout.ToggleLeft("Odd / Even Rows", hierarchyOddEven);
                
                hierarchySoloVisibility = EditorGUILayout.ToggleLeft("Solo Visibility", hierarchySoloVisibility);
                hierarchySoloPickability = EditorGUILayout.ToggleLeft("Solo Pickability", hierarchySoloPickability);
                hierarchyTree = EditorGUILayout.ToggleLeft("Tree", hierarchyTree);
            }

            private static void DrawFilterByType()
            {
                EditorGUI.BeginDisabledGroup(!unsafeFeatures);
                EditorGUI.BeginChangeCheck();
                _hierarchyTypeFilter = EditorGUILayout.ToggleLeft("Filter By Type", _hierarchyTypeFilter);
                if (EditorGUI.EndChangeCheck()) HierarchyToolbarInterceptor.Refresh();
                EditorGUI.EndDisabledGroup();
            }

            private static void DrawBestComponents()
            {
                EditorGUI.BeginChangeCheck();
                hierarchyOverrideMainIcon = EditorGUILayout.ToggleLeft("Best Component Icon Before Name", hierarchyOverrideMainIcon);
                if (!EditorGUI.EndChangeCheck()) return;

                Object[] windows = UnityEngine.Resources.FindObjectsOfTypeAll(SceneHierarchyWindowRef.type);
                foreach (Object window in windows)
                {
                    EditorWindow wnd = window as EditorWindow;
                    HierarchyHelper.SetDefaultIconsSize(wnd, hierarchyOverrideMainIcon ? 0 : 18);
                    wnd.Repaint();
                }
            }

            private static void DrawComponentButtons()
            {
                hierarchyIcons = EditorGUILayout.ToggleLeft("Component Buttons", hierarchyIcons, EditorStyles.label);

                EditorGUI.indentLevel++;
                
                EditorGUI.BeginChangeCheck();
                hierarchyIconsHideDefault = EditorGUILayout.ToggleLeft("Hide Default Icons", hierarchyIconsHideDefault);
                if (EditorGUI.EndChangeCheck())
                {
                    ComponentIconDrawer.ClearCache();
                    EditorApplication.RepaintHierarchyWindow();
                }
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Modifiers", GUILayout.Width(LabelWidth - 17));
                hierarchyIconsModifiers = DrawModifiers(hierarchyIconsModifiers);
                EditorGUILayout.EndHorizontal();

                hierarchyIconsMaxItems = EditorGUILayout.IntField("Max Items", hierarchyIconsMaxItems);
                if (hierarchyIconsMaxItems < 1) hierarchyIconsMaxItems = 1;

                hierarchyIconsDisplayRule = (HierarchyIconsDisplayRule)EditorGUILayout.EnumPopup("Display Rule", hierarchyIconsDisplayRule);

                EditorGUI.indentLevel--;
            }

            public string GetMenuName()
            {
                return "Hierarchy";
            }

            public IEnumerable<Shortcut> GetShortcuts()
            {
                List<Shortcut> shortcuts = new List<Shortcut>();

                if (hierarchyIcons)
                {
                    shortcuts.Add(new Shortcut("Show Component Icons", "Hierarchy", hierarchyIconsModifiers));
                }

                if (hierarchyEnableMiddleClick)
                {
                    shortcuts.Add(new Shortcut("Toggle GameObject Enable", "Hierarchy", "MMB"));
                }

                return shortcuts;
            }

            public override void SetState(bool state)
            {
                base.SetState(state);
                
                _hierarchyTypeFilter = state;
                hierarchyBookmarks = state;
                hierarchyEnableGameObject = state;
                hierarchyEnableMiddleClick = state;
                hierarchyErrorIcons = state;
                hierarchyIcons = state;
                hierarchyNote = state;
                hierarchyOverrideMainIcon = state;
                hierarchyOddEven = state;
                hierarchyRowBackground = state;
                hierarchySoloVisibility = state;
                hierarchyTree = state;

                GetManager<HeadersManager>().SetState(state);
            }
        }

        public enum HierarchyRowBackgroundStyle
        {
            gradient,
            solid
        }
    }
}