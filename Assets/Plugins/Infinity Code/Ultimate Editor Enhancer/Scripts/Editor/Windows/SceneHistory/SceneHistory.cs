/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using System.Linq;
using InfinityCode.UltimateEditorEnhancer.Behaviors;
using InfinityCode.UltimateEditorEnhancer.References;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfinityCode.UltimateEditorEnhancer.Windows
{
    [InitializeOnLoad]
    public class SceneHistory : EditorWindow
    {
        private static Texture2D _noBookmarkTexture;
        private static GUIStyle _showContentStyle;
        private static GUIContent closeContent;
        private static string filter = "";
        private static SceneHistoryItem[] filteredRecords;
        private static bool focusOnTextField = false;
        private static double lastClickTime;
        private static Texture2D noBookmarkTexture;
        private static Vector2 scrollPosition;
        private static SceneHistoryItem selectedItem;
        private static int selectedIndex = 0;
        private static GUIContent showContent;

        private static GUIStyle showContentStyle
        {
            get
            {
                if (_showContentStyle == null || !_showContentStyle.normal.background)
                {
                    _showContentStyle = new GUIStyle(Styles.transparentButton)
                    {
                        margin =
                        {
                            top = 6,
                            left = 6
                        }
                    };
                }
                
                return _showContentStyle;
            }
        }

        static SceneHistory()
        {
            EditorSceneManager.sceneClosed += OnSceneClosed;
        }

        private static bool CheckPlaymode()
        {
            if (!EditorApplication.isPlaying) return true;
            if (!EditorUtility.DisplayDialog(
                    "Opening the scene during play mode", 
                    "Opening the scene cannot be used during play mode.", 
                    "Stop play mode", 
                    "Cancel"))
            {
                return false;
            }
            
            EditorApplication.isPlaying = false;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            return false;

        }

        private void DrawItems()
        {
            int removeIndex = -1;
            if (filteredRecords == null)
            {
                for (int i = 0; i < SceneHistoryReferences.count; i++)
                {
                    SceneHistoryItem item = SceneHistoryReferences.items[i];
                    if (DrawRow(item)) removeIndex = i; 
                }
            }
            else
            {
                foreach (SceneHistoryItem item in filteredRecords)
                {
                    if (DrawRow(item)) removeIndex = SceneHistoryReferences.IndexOf(item);
                }
            }

            if (removeIndex == -1) return;
            SceneHistoryReferences.RemoveAt(removeIndex);
            if (filteredRecords != null) UpdateFilteredRecords();
        }

        private bool DrawRow(SceneHistoryItem item)
        {
            bool ret = false;

            EditorGUILayout.BeginHorizontal(selectedItem == item ? Styles.selectedRow : Styles.transparentRow);

            EditorGUI.BeginDisabledGroup(!item.exists);

            if (GUILayout.Button(showContent, showContentStyle, GUILayout.ExpandWidth(false), GUILayout.Height(12)))
            {
                if (SceneManagerHelper.AskForSave(SceneManager.GetActiveScene()))
                {
                    EditorSceneManager.OpenScene(item.path);
                    Close();
                }
            }

            GUIContent content = TempContent.Get(item.name, item.path);
            ButtonEvent ev = GUILayoutUtils.Button(content, EditorStyles.label, GUILayout.Height(20), GUILayout.MaxWidth(position.width - 30));
            if (ProcessRowEvents(item, ev))
            {
                Close();
            }

            bool hasBookmark = HasBookmark(item);

            Texture2D texture = noBookmarkTexture;
            string tooltip = "Add Bookmark";

            if (hasBookmark)
            {
                texture = (Texture2D)Icons.starYellow;
                tooltip = "Remove Bookmark";
            }

            if (GUILayout.Button(TempContent.Get(texture, tooltip), GUIStyle.none, GUILayout.Width(20)))
            {
                SceneAsset asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(item.path);
                if (hasBookmark) Bookmarks.Remove(asset);
                else Bookmarks.Add(asset);
            }

            EditorGUI.EndDisabledGroup();

            if (GUILayout.Button(closeContent, Styles.transparentButton, GUILayout.ExpandWidth(false), GUILayout.Height(12)))
            {
                ret = true;
            }

            EditorGUILayout.EndHorizontal();

            return ret;
        }

        private static bool HasBookmark(SceneHistoryItem item)
        {
            return BookmarkReferences.Any(b => b.target && b.path == item.path);
        }

        private void OnDestroy()
        {
            filteredRecords = null;
            filter = "";
        }

        private void OnEnable()
        {
            filteredRecords = null;
            SceneHistoryItem[] items = SceneHistoryReferences.items;
            if (items.Length > 0) selectedItem = items[selectedIndex];

            showContent = new GUIContent(Styles.isProSkin ? Icons.openNewWhite : Icons.openNewBlack, "Open Scene");
            closeContent = new GUIContent(Styles.isProSkin ? Icons.closeWhite : Icons.closeBlack, "Remove");
            noBookmarkTexture = Styles.isProSkin ? (Texture2D)Icons.starWhite : (Texture2D)Icons.starBlack;

            foreach (SceneHistoryItem item in items)
            {
                item.CheckExists();
            }

            focusOnTextField = true;
        }


        private void OnGUI()
        {
            if (ProcessEvents())
            {
                Close();
                return;
            }
            Toolbar();

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            DrawItems();
            EditorGUILayout.EndScrollView();
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.EnteredEditMode) return;

            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            if (!SceneManagerHelper.AskForSave(SceneManager.GetActiveScene())) return;
            
            SelectionHistory.Clear();
            EditorSceneManager.OpenScene(selectedItem.path);
        }

        private static void OnSceneClosed(Scene scene)
        {
            if (EditorApplication.isPlaying) return;

            SelectionHistory.Clear();
            string path = scene.path;
            if (string.IsNullOrEmpty(path)) return;

            SceneHistoryItem item = SceneHistoryReferences.FirstOrDefault(i => i.path == path);
            if (item != null)
            {
                SceneHistoryReferences.Remove(item);
                SceneHistoryReferences.Insert(0, item);
                return;
            }

            item = new SceneHistoryItem
            {
                path = path,
                name = scene.name
            };
            item.CheckExists();
            SceneHistoryReferences.Insert(0, item);

            while (SceneHistoryReferences.count > 19) SceneHistoryReferences.RemoveAt(SceneHistoryReferences.count - 1);
        }

        [MenuItem("File/Recent Scenes %#o", false, 155)]
        public static void OpenWindow()
        {
            GetWindow<SceneHistory>(true, "Recent Scenes");
        }

        private void ProcessContextMenu(SceneHistoryItem item)
        {
            selectedItem = item;
            GenericMenuEx menu = GenericMenuEx.Start();
            menu.Add("Open", () => SelectCurrent());
            menu.Add("Open Additive", () => { EditorSceneManager.OpenScene(item.path, OpenSceneMode.Additive); });
            menu.AddSeparator();
            menu.Add("Remove", () =>
            {
                SceneHistoryReferences.Remove(item);
                if (filteredRecords != null) UpdateFilteredRecords();
            });
            menu.Show();
        }

        private bool ProcessEvents()
        {
            Event e = Event.current;
            if (e.type != EventType.KeyDown) return false;
            
            if (e.keyCode == KeyCode.DownArrow) SelectNext();
            else if (e.keyCode == KeyCode.UpArrow) SelectPrev();
            else if (e.keyCode == KeyCode.KeypadEnter || e.keyCode == KeyCode.Return) return SelectCurrent();
            else if (e.keyCode == KeyCode.Escape) return true;

            return false;
        }

        private bool ProcessRowClick(SceneHistoryItem item)
        {
            if (selectedItem == item)
            {
                if (EditorApplication.timeSinceStartup - lastClickTime < 0.3)
                {
                    if (SelectCurrent())
                    {
                        Close();
                        return true;
                    }
                }

                lastClickTime = EditorApplication.timeSinceStartup;
                Selection.activeObject = AssetDatabase.LoadAssetAtPath<SceneAsset>(item.path);
            }
            else
            {
                lastClickTime = EditorApplication.timeSinceStartup;
                selectedItem = item;
                Selection.activeObject = AssetDatabase.LoadAssetAtPath<SceneAsset>(item.path);
                selectedIndex = SceneHistoryReferences.IndexOf(item);
            }

            Event.current.Use();
            return false;
        }

        private bool ProcessRowEvents(SceneHistoryItem item, ButtonEvent ev)
        {
            Event e = Event.current;
            if (ev == ButtonEvent.press)
            {
                if (e.button == 0) return ProcessRowClick(item);
                selectedItem = item;
            }
            else if (ev == ButtonEvent.click)
            {
                if (e.button == 1) ProcessContextMenu(item);
            }
            else if (ev == ButtonEvent.drag)
            {
                DragAndDrop.PrepareStartDrag();
                DragAndDrop.paths = new[] { item.path };
                DragAndDrop.StartDrag("Drag " + item.name);
                e.Use();
            }

            return false;
        }

        private bool SelectCurrent()
        {
            if (selectedItem == null) return false;
            if (!selectedItem.exists) return false;
            if (!CheckPlaymode()) return false;

            if (!SceneManagerHelper.AskForSave(SceneManager.GetActiveScene())) return false;
            
            SelectionHistory.Clear();
            EditorSceneManager.OpenScene(selectedItem.path);
            return true;

        }

        private void SelectPrev()
        {
            selectedIndex--;
            if (filteredRecords != null)
            {
                if (filteredRecords.Length == 0)
                {
                    selectedIndex = 0;
                    selectedItem = null;
                }
                else
                {
                    if (selectedIndex < 0) selectedIndex = filteredRecords.Length - 1;
                    selectedItem = filteredRecords[selectedIndex];
                }
            }
            else
            {
                if (selectedIndex < 0) selectedIndex = SceneHistoryReferences.count - 1;
                selectedItem = SceneHistoryReferences.items[selectedIndex];
            }
            Event.current.Use();
            Repaint();
        }

        private void SelectNext()
        {
            selectedIndex++;
            if (filteredRecords != null)
            {
                if (filteredRecords.Length == 0)
                {
                    selectedIndex = 0;
                    selectedItem = null;
                }
                else
                {
                    if (selectedIndex >= filteredRecords.Length) selectedIndex = 0;
                    selectedItem = filteredRecords[selectedIndex];
                }
            }
            else
            {
                if (selectedIndex >= SceneHistoryReferences.count) selectedIndex = 0;
                selectedItem = SceneHistoryReferences.items[selectedIndex];
            }

            Event.current.Use();
            Repaint();
        }

        private void Toolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            EditorGUI.BeginChangeCheck();
            GUI.SetNextControlName("UEESceneHistoryTextField");
            filter = GUILayoutUtils.ToolbarSearchField(filter);
            if (EditorGUI.EndChangeCheck()) UpdateFilteredRecords();

            if (focusOnTextField && Event.current.type == EventType.Repaint)
            {
                GUI.FocusControl("UEESceneHistoryTextField");
                focusOnTextField = false;
            }

            if (GUILayout.Button("?", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) Links.OpenDocumentation("recent-scenes");

            EditorGUILayout.EndHorizontal();
        }

        private void UpdateFilteredRecords()
        {
            if (string.IsNullOrEmpty(filter))
            {
                filteredRecords = null;
                return;
            }

            string pattern = SearchableItem.GetPattern(filter);
            filteredRecords = SceneHistoryReferences.Where(i => i.Match(pattern)).ToArray();
            
            if (!filteredRecords.Contains(selectedItem))
            {
                selectedItem = filteredRecords.Length > 0 ? filteredRecords[0] : null;
                selectedIndex = 0;
            }
            else
            {
                selectedIndex = ArrayUtility.IndexOf(filteredRecords, selectedItem);
            }
        }

        [MenuItem("File/Recent Scenes %#o", true, 155)]
        public static bool ValidateOpenWindow()
        {
            return SceneHistoryReferences.count > 0;
        }
    }
}