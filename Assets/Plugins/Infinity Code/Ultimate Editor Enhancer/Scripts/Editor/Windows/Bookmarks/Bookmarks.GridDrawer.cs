/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.Windows
{
    public partial class Bookmarks
    {
        private const string GridSizePref = Prefs.Prefix + "Bookmarks.GridSize";
        private const int GridMargin = 10;
        private const int MinGridSize = 47;
        private const  int MaxGridSize = 128;

        private static int gridSize = 47;
        private static GUIStyle _labelStyle;
        private static GUIStyle selectedStyle;

        private static GUIStyle labelStyle
        {
            get
            {
                if (_labelStyle == null)
                {
                    _labelStyle = new GUIStyle(EditorStyles.miniLabel);
                    _labelStyle.alignment = TextAnchor.MiddleCenter;
                    _labelStyle.wordWrap = true;
                }
                return _labelStyle;
            }
        }

        private bool DrawCell(BookmarkItem item, Rect rect)
        {
            bool selected = item.target && Selection.activeObject == item.target;

            if (!item.preview || !item.previewLoaded) InitPreview(item);

            ProcessCellEvents(item, rect);

            if (Event.current.type != EventType.Repaint) return true;

            if (selected)
            {
                selectedStyle.Draw(new RectOffset(2, 2, 2, 2).Add(rect), GUIContent.none, false, false, false, false);
            }

            GUIStyle style = labelStyle;
            
            style.Draw(rect, TempContent.Get("", GetItemTooltip(item)), false, false, false, false);
            GUI.DrawTexture(new Rect(rect.x + GridMargin, rect.y, gridSize, gridSize), item.preview);
            
            GUIContent content = TempContent.Get(item.title);
            style.Draw(new Rect(rect.x, rect.y + gridSize, rect.width, 20), content, false, false, false, false);

            return true;
        }

        private void DrawGridItems(IEnumerable<BookmarkItem> gridItems, string label = null)
        {
            if (!string.IsNullOrEmpty(label))
            {
                GUILayout.Label(label);
            }

            int countItems = gridItems.Count();

            int countCols = Mathf.FloorToInt((position.width - 30) / (gridSize + GridMargin * 2));
            int countRows = Mathf.CeilToInt(countItems / (float)countCols);
            int rowHeight = gridSize + 20;
            int width = Mathf.Min(countCols, countItems) * (gridSize + GridMargin * 2);
            int height = countRows * (rowHeight);

            float marginLeft = (position.width - width) / 2;

            GUILayout.Box(GUIContent.none, GUIStyle.none, GUILayout.Width(width), GUILayout.Height(height));
            Rect rect = GUILayoutUtility.GetLastRect();

            int i = 0;
            foreach (BookmarkItem item in gridItems)
            {
                int row = i / countCols;
                int col = i % countCols;
                Rect r = new Rect(col * (gridSize + GridMargin * 2) + marginLeft, row * rowHeight + rect.y, gridSize + GridMargin * 2, rowHeight);
                if (!DrawCell(item, r)) removeItem = item;
                i++;
            }
        }

        private void ProcessCellEvents(BookmarkItem item, Rect rect)
        {
            Event e = Event.current;
            if (!rect.Contains(e.mousePosition)) return;
            if (e.type == EventType.MouseUp)
            {
                if (e.button == 0)
                {
                    if (Selection.activeObject == item.target)
                    {
                        ProcessDoubleClick(item);
                    }
                    else
                    {
                        lastClickTime = EditorApplication.timeSinceStartup;
                        Selection.activeObject = item.target;
                        EditorGUIUtility.PingObject(item.target);
                    }
                    e.Use();
                }
                else if (e.button == 1)
                {
                    ShowContextMenu(item);
                    e.Use();
                }
            }
            else if (e.type == EventType.MouseDrag)
            {
                if (GUIUtility.hotControl == 0)
                {
                    DragAndDrop.PrepareStartDrag();
                    DragAndDrop.objectReferences = new[] { item.target };

                    DragAndDrop.StartDrag("Drag " + item.target);
                    e.Use();
                }
            }
        }
    }
}