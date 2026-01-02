/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfinityCode.UltimateEditorEnhancer.PostHeader;
using InfinityCode.UltimateEditorEnhancer.References;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.SceneTools
{
    [InitializeOnLoad]
    public static class SceneNoteDrawer
    {
        private static GameObject activeGameObject;
        private static NoteItem activeNoteItem;
        private static Rect activeRect;
        private static bool initialized;
        private static Dictionary<GameObject, NoteItem> notes = new Dictionary<GameObject, NoteItem>();
        
        static SceneNoteDrawer()
        {
            SceneViewManager.AddListener(OnSceneGUI, 0, true);
        }

        private static void Disable()
        {
            initialized = false;
            notes.Clear();
        }

        private static void DrawItems(SceneView sceneView)
        {
            Event e = Event.current;
            float pixelPerPoint = EditorGUIUtility.pixelsPerPoint;

            activeGameObject = null;
            activeNoteItem = null;
            activeRect = default;

            foreach (KeyValuePair<GameObject, NoteItem> pair in notes)
            {
                Vector3 screenPoint = sceneView.camera.WorldToScreenPoint(pair.Key.transform.position) / pixelPerPoint;
                Rect rect = new Rect(screenPoint.x - 8, Screen.height / pixelPerPoint - screenPoint.y - 56, 16, 16);
                
                if (e.type == EventType.Repaint) GUI.DrawTexture(rect, Icons.note, ScaleMode.ScaleToFit);
                if (!rect.Contains(e.mousePosition)) continue;
                
                activeGameObject = pair.Key;
                activeNoteItem = pair.Value;
                activeRect = rect;
            }
        }

        private static void DrawTooltip(SceneView sceneView)
        {
            GUIStyle tooltipStyle = Styles.tooltip;
            StringBuilder builder = StaticStringBuilder.Start();
            builder.Append(activeGameObject.name).Append("\n--\n").Append(activeNoteItem.text);
            GUIContent tooltipContent = TempContent.Get(builder.ToString());
            Vector2 size = tooltipStyle.CalcSize(tooltipContent);
            Rect tooltipRect = new Rect(activeRect.center.x - size.x / 2, activeRect.center.y - size.y - 14, size.x, size.y);
            if (activeRect.center.y < sceneView.position.height / 2)
            {
                tooltipRect.y = activeRect.center.y + 14;
            }
            tooltipStyle.Draw(tooltipRect, tooltipContent, false, false, false, false);
        }

        private static void Enable()
        {
            initialized = true;
            notes.Clear();

            foreach (NoteItem item in NoteReferences.items.Where(i => !i.isEmpty))
            {
                GlobalObjectId gid;
                if (!GlobalObjectId.TryParse(item.gid, out gid)) continue;
                GameObject go = GlobalObjectId.GlobalObjectIdentifierToObjectSlow(gid) as GameObject;
                if (!go) continue;
                notes.Add(go, item);
            }
        }

        private static void OnSceneGUI(SceneView sceneView)
        {
            Event e = Event.current;
            if (e.modifiers != EventModifiers.Alt)
            {
                Disable();
                return;
            }

            if (!initialized) Enable();
            
            Handles.BeginGUI();

            DrawItems(sceneView);
            ProcessActiveItem(sceneView);
            
            Handles.EndGUI();
        }

        private static void ProcessActiveItem(SceneView sceneView)
        {
            if (!activeGameObject) return;

            Event e = Event.current;
            if (e.type == EventType.Repaint) DrawTooltip(sceneView);
            else if (e.type == EventType.MouseDown)
            {
                Selection.activeGameObject = activeGameObject;
                e.Use();
            }
        }
    }
}