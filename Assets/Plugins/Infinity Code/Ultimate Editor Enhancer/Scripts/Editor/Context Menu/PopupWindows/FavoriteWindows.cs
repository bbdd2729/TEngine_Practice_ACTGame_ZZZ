/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using System.Linq;
using InfinityCode.UltimateEditorEnhancer.References;
using InfinityCode.UltimateEditorEnhancer.Windows;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.EditorMenus.PopupWindows
{
    public class FavoriteWindows : PopupWindowItem, IValidatableLayoutItem
    {
        private GUIContent[] contents;
        private FavoriteWindowItem[] items;
        private GUIContent labelContent;
        private Vector2 labelSize;
        private GUIContent editFavoritesContent;

        public override float order => 95;

        protected override void CalcSize()
        {
            labelSize = EditorStyles.whiteLabel.CalcSize(labelContent);
            Vector2 editSize = EditorStyles.whiteLabel.CalcSize(editFavoritesContent);
            _size = new Vector2(labelSize.x + editSize.x, Mathf.Max(labelSize.y, editSize.y) + GUI.skin.label.margin.bottom);

            GUIStyle style = Styles.buttonWithToggleAlignLeft;
            int marginBottom = style.margin.bottom;

            for (int i = 0; i < items.Length; i++)
            {
                Vector2 s = style.CalcSize(contents[i]);
                _size.x = Mathf.Max(_size.x, s.x);
                _size.y += s.y + marginBottom;
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            contents = null;
            contents = null;
            labelContent = null;
        }

        public override void Draw()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(labelContent, EditorStyles.whiteLabel, GUILayout.Width(labelSize.x));
            if (GUILayout.Button(editFavoritesContent, EditorStyles.whiteLabel, GUILayout.ExpandWidth(false)))
            {
                Settings.OpenFavoriteWindowsSettings();
                EditorMenu.Close();
            }
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < items.Length; i++)
            {
                if (GUILayout.Button(contents[i], Styles.buttonWithToggleAlignLeft))
                {
                    items[i].Open();
                    EditorMenu.Close();
                }
            }
        }

        protected override void Init()
        {
            items = FavoriteWindowReferences.items;
            contents = items.Select(i => new GUIContent(i.title)).ToArray();

            labelContent = new GUIContent("Favorite Windows:");
            editFavoritesContent = EditorGUIUtility.IconContent("d_Preset.Context");
            editFavoritesContent.tooltip = "Edit Favorite Windows";
        }

        public bool Validate()
        {
            return Prefs.favoriteWindowsInContextMenu && FavoriteWindowReferences.count > 0;
        }
    }
}