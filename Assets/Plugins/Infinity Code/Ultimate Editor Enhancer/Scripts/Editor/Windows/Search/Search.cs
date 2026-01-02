/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.Windows
{
    [InitializeOnLoad]
    public partial class Search : PopupWindow
    {
        private const int Width = 500;
        private const int MaxRecords = 50;
        private const int SearchByFolderPriority = 25;
        
        public static int searchMode = 0;

        private static Dictionary<string, Record> projectRecords;
        private static Dictionary<int, Record> sceneRecords;
        private static Dictionary<int, Record> windowRecords;
        
        private static Record[] bestRecords;
        private static int countBestRecords = 0;
        private static bool needUpdateBestRecords;
        
        private static string pathStartsWith;

        public static Search instance { get; private set; }

        static Search()
        {
            KeyManager.KeyBinding binding = KeyManager.AddBinding();
            binding.OnValidate += OnValidate;
            binding.OnPress += OnInvoke;

            binding = KeyManager.AddBinding();
            binding.OnValidate += OnValidateScript;
            binding.OnPress += OnInvokeScript;
        }

        public static void OnInvoke()
        {
            Event e = Event.current;
            Vector2 position = e.mousePosition;

            if (focusedWindow != null) position += focusedWindow.position.position;

            Rect rect = new Rect(position + new Vector2(Width / -2, -30), new Vector2(Width, 140));

#if !UNITY_EDITOR_OSX
            if (rect.y < 5) rect.y = 5;
            else if (rect.yMax > Screen.currentResolution.height - 40) rect.y = Screen.currentResolution.height - 40 - rect.height;
#endif

            Show(rect);
            e.Use();
        }

        private static void OnInvokeScript()
        {
            OnInvoke();
            searchText = ":script";
            setSelectionIndex = 0;
            resetSelection = true;
            searchMode = 2;
        }

        private static bool OnValidate()
        {
            if (!Prefs.search) return false;
            Event e = Event.current;

            if (e.keyCode != Prefs.searchKeyCode) return false;
            if (e.modifiers != Prefs.searchModifiers) return false;
            if (EditorApplication.isPlaying && ShortcutIntegrationRef.GetIgnoreWhenPlayModeFocused()) return false;
            return !Prefs.SearchDoNotShowOnWindows();
        }

        private static bool OnValidateScript()
        {
            if (!Prefs.searchScript) return false;

            Event e = Event.current;
            return e.modifiers == Prefs.searchScriptModifiers && e.keyCode == Prefs.searchScriptKeyCode;
        }

        private static void SelectRecord(int index, int state)
        {
            bestRecords[index].Select(state);
            EventManager.BroadcastClosePopup();
        }

        [MenuItem("Assets/Search By Folder", false, SearchByFolderPriority)]
        private static void SearchByFolder()
        {
            OnInvoke();
            Rect rect = instance.position;
            rect.height += 20;
            instance.position = rect;
            searchMode = 2;
            pathStartsWith = AssetDatabase.GetAssetPath(Selection.activeObject);
            setSelectionIndex = 0;
            resetSelection = true;
        }

        public static void Show(Rect rect)
        {
            EventManager.BroadcastClosePopup();

            SceneView.RepaintAll();

            if (Prefs.searchPauseInPlayMode && EditorApplication.isPlaying) EditorApplication.isPaused = true;

            instance = CreateInstance<Search>();
            instance.position = rect;
            instance.wantsMouseMove = true;
            setSelectionIndex = -1;
            instance.ShowPopup();
            instance.Focus();
            focusOnTextField = true;
            searchMode = 0;
            searchText = "";
            pathStartsWith = null;

            EventManager.AddBinding(EventManager.ClosePopupEvent).OnInvoke += b =>
            {
                instance.Close();
                b.Remove();
            };
        }

        private int TakeBestRecords(IEnumerable<Record> tempBestRecords, string pattern, string assetType)
        {
            if (pattern.Length == 1) FilterSingle(tempBestRecords, pattern, assetType);
            else if (pattern.Length == 2) FilterTwo(tempBestRecords, pattern, assetType);
            else FilterThreePlus(tempBestRecords, pattern, assetType);
            return bestRecords.Length;
        }

        private static void FilterSingle(IEnumerable<Record> tempBestRecords, string pattern, string assetType)
        {
            List<Record> results = new List<Record>(MaxRecords);
            char c = char.ToUpperInvariant(pattern[0]);
            foreach (Record record in tempBestRecords)
            {
                if (!record.ValidateValueType(assetType)) continue;
                string[] searches = record.GetSearches();

                if (searches[0].IndexOf(c) == -1) continue;
                results.Add(record);
                if (results.Count >= MaxRecords) break;
            }
                
            bestRecords = results.ToArray();
        }
        
        private static void FilterTwo(IEnumerable<Record> tempBestRecords, string pattern, string assetType)
        {
            SortedLinkedList<Record> results = new SortedLinkedList<Record>(MaxRecords);

            foreach (Record record in tempBestRecords)
            {
                if (!record.ValidateValueType(assetType)) continue;
                string[] searches = record.GetSearches();

                if (results.count < MaxRecords)
                {
                    int i = SearchableItem.GetMatchIndex(pattern, searches);
                    if (i != -1)
                    {
                        results.Add(record, searches[i].Length);
                    }
                }
                else
                {
                    string s = searches[0];
                    if (s.Length >= results.maxValue) continue;
                    if (!SearchableItem.Match(pattern, s)) continue;
                    if (results.maxValue < 10) break;
                }
            }

            bestRecords = results.ToArray();
        }

        private static void FilterThreePlus(IEnumerable<Record> tempBestRecords, string pattern, string assetType)
        {
            SortedLinkedList<Record> results = new SortedLinkedList<Record>(MaxRecords);

            foreach (Record record in tempBestRecords)
            {
                if (!record.ValidateValueType(assetType)) continue;
                string[] searches = record.GetSearches();

                if (results.count < MaxRecords)
                {
                    int i = SearchableItem.GetMatchIndex(pattern, searches);
                    if (i != -1)
                    {
                        results.Add(record, searches[i].Length);
                    }
                }
                else
                {
                    foreach (string s in searches)
                    {
                        if (s.Length >= results.maxValue) continue;
                        if (!SearchableItem.Match(pattern, s)) continue;
                        
                        results.Add(record, s.Length);
                        break;
                    }
                }
            }

            bestRecords = results.ToArray();
        }

        private void UpdateBestRecords()
        {
            needUpdateBestRecords = false;
            bestRecordIndex = 0;
            countBestRecords = 0;
            scrollPosition = Vector2.zero;

            int minStrLen = 1;
            if (searchText == null || searchText.Length < minStrLen) return;

            string assetType;
            string pattern = SearchableItem.GetPattern(searchText, out assetType);

            IEnumerable <Record> tempBestRecords;

            if (searchMode == 0)
            {
                int currentMode = 0;
                tempBestRecords = new List<Record>();
                if (pattern.Length > 0)
                {
                    if (pattern[0] == '@') currentMode = 1;
                    else if (pattern[0] == '#') currentMode = 2;
                }

                if (currentMode != 0) pattern = pattern.Substring(1);

                if (Prefs.searchByWindow && currentMode == 0)
                {
                    var windows = windowRecords.Select(r => r.Value);
                    tempBestRecords = tempBestRecords.Concat(windows);
                }

                if (currentMode == 0 || currentMode == 1)
                {
                    var scenes = sceneRecords.Select(r => r.Value);
                    tempBestRecords = tempBestRecords.Concat(scenes);
                }
                if (Prefs.searchByProject && (currentMode == 0 || currentMode == 2))
                {
                    var tempProjectRecords = projectRecords.Select(r => r.Value);
                    tempBestRecords = tempBestRecords.Concat(tempProjectRecords);
                }
            }
            else if (searchMode == 1)
            {
                tempBestRecords = sceneRecords.Select(r => r.Value);
            }
            else
            {
                if (string.IsNullOrEmpty(pathStartsWith))
                {
                    tempBestRecords = projectRecords.Select(r => r.Value);
                }
                else
                {
                    tempBestRecords = projectRecords.Where(r => (r.Value as ProjectRecord).path.StartsWith(pathStartsWith)).Select(r => r.Value);
                }
            }

            countBestRecords = TakeBestRecords(tempBestRecords, pattern, assetType);
            updateScroll = true;
        }

        [MenuItem("Assets/Search By Folder", true, SearchByFolderPriority)]
        private static bool ValidateSearchByFolder()
        {
            return Selection.activeObject is DefaultAsset;
        }
    }
}