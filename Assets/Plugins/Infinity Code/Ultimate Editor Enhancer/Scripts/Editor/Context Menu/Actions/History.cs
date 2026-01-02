/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using InfinityCode.UltimateEditorEnhancer.Behaviors;
using InfinityCode.UltimateEditorEnhancer.References;
using InfinityCode.UltimateEditorEnhancer.Tools;
using InfinityCode.UltimateEditorEnhancer.Windows;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfinityCode.UltimateEditorEnhancer.EditorMenus.Actions
{
    public class History : ActionItem
    {
        protected override bool closeOnSelect => false;

        public override float order => 800;

        protected override void Init()
        {
            guiContent = new GUIContent(Icons.history, "History");
        }

        public override void Invoke()
        {
            GenericMenuEx menu = GenericMenuEx.Start();
            Scene activeScene = SceneManager.GetActiveScene();

            foreach (SceneHistoryItem r in SceneHistoryReferences.items)
            {
                if (r.path == activeScene.path) continue;

                menu.Add("Scenes/" + r.name, () =>
                {
                    EditorSceneManager.OpenScene(r.path);
                    EditorMenu.Close();
                });
            }

            List<SelectionHistory.SelectionRecord> selectionItems = SelectionHistory.records;
            for (int i = 0; i < selectionItems.Count; i++)
            {
                int ci = i;
                string names = selectionItems[i].GetShortNames();
                if (string.IsNullOrEmpty(names)) continue;
                menu.Add("Selection/" + names, SelectionHistory.activeIndex == i, () =>
                {
                    SelectionHistory.SetIndex(ci);
                    EditorMenu.Close();
                });
            }
            
            foreach (ToolbarWindows.WindowRecord r in ToolbarWindows.recent)
            {
                menu.Add("Windows/" + r.title, () =>
                {
                    ToolbarWindows.RestoreRecentWindow(r);
                    EditorMenu.Close();
                });
            }

            menu.Show();
        }
    }
}