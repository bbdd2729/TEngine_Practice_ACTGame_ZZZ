/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfinityCode.UltimateEditorEnhancer.Windows
{
    public partial class ViewGallery
    {
        public abstract class ViewItem: SearchableItem
        {
            public virtual Texture2D texture { get;  set; }
            public abstract bool useInPreview { get; set; }
            public abstract bool allowPreview { get; }
            public abstract string name { get; }
            public virtual bool inCurrentScene => false;
            public virtual string sceneName => SceneManager.GetActiveScene().name;
            public virtual bool isTemp { get; set; } = false;

            public bool Draw(Rect rect, float maxLabelWidth)
            {
                bool status = false;
                Event e = Event.current;
                Rect toggleRect = new Rect(rect.xMax - 20, rect.yMin + 4, 16, 16);
                Rect currentRect = new RectOffset(2, 2, 2, 2).Add(rect);
                GUIContent content;
                bool isCurrentScene = inCurrentScene;
                
                if (rect.Contains(e.mousePosition))
                {
                    string extraAction = !isCurrentScene && !isTemp ? $"open \"{sceneName}\" scene and " : "";
                    string tooltip = $"{name} from \"{sceneName}\"\nClick to {extraAction}set this view";
                    content = TempContent.Get(string.Empty, tooltip);
                    GUI.Box(currentRect, content, selectedStyle);
                    if (!toggleRect.Contains(e.mousePosition)) ProcessEvents(e, ref status);
                }
                else if (isTemp)
                {
                    GUI.Box(currentRect, string.Empty, tempStyle);
                }
                else if (isCurrentScene)
                {
                    GUI.Box(currentRect, string.Empty, currentSceneStyle);
                }
                GUI.Box(rect, TempContent.Get(texture), GUIStyle.none);
                GUI.Label(new Rect(rect.center.x - maxLabelWidth / 2, rect.yMax + 5, maxLabelWidth, 15), name, Styles.centeredLabel);
                if (!allowPreview) return status;
                
                EditorGUI.BeginChangeCheck();
                content = TempContent.Get(string.Empty, "Use In Preview Tool");
                bool v = GUI.Toggle(toggleRect, useInPreview, content);
                if (EditorGUI.EndChangeCheck()) useInPreview = v;

                return status;
            }

            protected override int GetSearchCount()
            {
                return 1;
            }

            protected override string GetSearchString(int index)
            {
                return name;
            }

            public abstract void PrepareMenu(GenericMenuEx menu);

            public void ProcessEvents(Event e, ref bool status)
            {
                if (e.type == EventType.MouseDown)
                {
                    if (e.button == 0) status = true;
                    else if (e.button == 1) ShowContextMenu();
                }
            }

            public void Set()
            {
                Set(SceneView.lastActiveSceneView);
                GetWindow<SceneView>();
                GUI.changed = true;
            }

            public abstract void Set(SceneView view);

            private void ShowContextMenu()
            {
                GenericMenuEx menu = GenericMenuEx.Start();
                PrepareMenu(menu);
                menu.Show();
            }
        }
    }
}