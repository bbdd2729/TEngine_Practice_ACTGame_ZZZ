/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Linq;
using InfinityCode.UltimateEditorEnhancer.References;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfinityCode.UltimateEditorEnhancer.SceneTools
{
    [InitializeOnLoad]
    public static class ViewStateDrawer
    {
        private static Camera[] cameras;
        private static Texture2D emptyTexture;
        private static Texture2D eyeTexture;
        private static double lastUpdateTime = double.MinValue;
        private static ViewStateWrapper[] states;
        private static GUIContent viewContent;
        private static SceneViewState[] viewStates;
        private static GUIStyle viewStyle;

        static ViewStateDrawer()
        {
            SceneViewManager.AddListener(DrawViewStates, SceneViewOrder.Normal, true);
        }

        private static void CacheItems()
        {
            viewStates = ViewStateReferences.items;
            int sceneCount = EditorSceneManager.loadedRootSceneCount;
                
            if (sceneCount > 0)
            {
                viewStates = viewStates.Where(s =>
                {
                    for (int i = 0; i < sceneCount; i++)
                    {
                        Scene scene = EditorSceneManager.GetSceneAt(i);
                        if (scene.path == s.scenePath) return true;
                    }
                    return false;
                }).ToArray();
            }
            else
            {
                viewStates = Array.Empty<SceneViewState>();
            }

            cameras = ObjectHelper.FindObjectsOfType<Camera>();
            lastUpdateTime = EditorApplication.timeSinceStartup;

            int countStates = viewStates.Length + cameras.Length;

            states = new ViewStateWrapper[countStates];
        }

        private static void DrawItems(SceneView sceneView)
        {
            Handles.BeginGUI();

            Camera camera = sceneView.camera;
            Vector2 mousePosition = Event.current.mousePosition;
            bool used = false;

            foreach (ViewStateWrapper w in states.OrderByDescending(s => s.distance))
            {
                Vector3 vp = camera.WorldToViewportPoint(w.position);
                if (vp.x < 0 || vp.y < 0 || vp.x > 1 || vp.y > 1 || vp.z < 0) continue;

                viewContent.text = w.title + "\nDistance: " + w.distance.ToString("F0") + " meters";
                Rect rect = new Rect(w.screenPoint.x - 24, w.screenPoint.y - 12, 48, 48);

                if (rect.Contains(mousePosition)) GUI.color = new Color32(211, 211, 211, 255);

                if (w.state != null)
                {
                    viewContent.image = eyeTexture;
                }
                else
                {
                    viewContent.image = emptyTexture;
                }

                if (GUI.Button(rect, viewContent, viewStyle) && !used)
                {
                    w.SetTo(sceneView);
                    used = true;
                }

                rect.position += new Vector2(0, 48);
                EditorGUIUtility.AddCursorRect(rect, MouseCursor.Zoom);

                GUI.color = Color.white;

                w.Dispose();
            }

            Handles.EndGUI();
        }

        private static void DrawViewStates(SceneView sceneView)
        {
            if (!Prefs.showViewStateInScene) return;

            Event e = Event.current;
            if (!e.alt)
            {
                cameras = null;
                viewStates = null;
                return;
            }

            if (e.type == EventType.Layout && 
                (viewStates == null || 
                 EditorApplication.timeSinceStartup - lastUpdateTime > 10))
            {
                CacheItems();
            }

            if (viewStates == null || cameras == null || viewStates.Length + cameras.Length == 0) return;
            
            Initialize();
            InitializeItems(sceneView);
            DrawItems(sceneView);
        }

        private static void Initialize()
        {
            if (viewContent == null)
            {
                eyeTexture = Resources.LoadIcon("Eye");
                emptyTexture = Resources.CreateTexture(64, 64, new Color(0, 0, 0, 0));
                viewContent = new GUIContent();
            }

            if (viewStyle == null)
            {
                viewStyle = new GUIStyle
                {
                    imagePosition = ImagePosition.ImageAbove, 
                    alignment = TextAnchor.MiddleCenter, 
                    normal =
                    {
                        textColor = Color.white
                    }
                };
            }
        }

        private static void InitializeItems(SceneView sceneView)
        {
            Camera camera = sceneView.camera;
            Vector3 cameraPosition = camera.transform.position;
            
            for (int i = 0; i < viewStates.Length; i++)
            {
                SceneViewState state = viewStates[i];
                Vector3 position = state.position;
                float magnitude = (position - cameraPosition).magnitude;

                Vector2 point = HandleUtility.WorldToGUIPoint(position);
                states[i] = new ViewStateWrapper
                {
                    state = state,
                    screenPoint = point,
                    position = position,
                    distance = magnitude
                };
            }

            for (int i = 0, j = viewStates.Length; i < cameras.Length; i++, j++)
            {
                Camera cam = cameras[i];
                if (!cam) continue;
                
                Vector3 position = cam.transform.position;
                float magnitude = (position - cameraPosition).magnitude;

                Vector2 point = HandleUtility.WorldToGUIPoint(position);
                states[j] = new ViewStateWrapper
                {
                    camera = cam,
                    screenPoint = point,
                    position = position,
                    distance = magnitude
                };
            }
        }

        internal class ViewStateWrapper
        {
            public SceneViewState state;
            public Camera camera;
            public Vector2 screenPoint;
            public float distance;
            public Vector3 position;

            public string title
            {
                get
                {
                    if (state != null) return state.title;
                    if (camera != null) return camera.gameObject.name;
                    return "";
                }
            }

            public void Dispose()
            {
                camera = null;
                state = null;
            }

            public void SetTo(SceneView view)
            {
                if (state != null)
                {
                    view.orthographic = state.is2D;
                    view.pivot = state.pivot;
                    view.size = state.size;
                    view.rotation = state.rotation;
                }
                else if (camera != null)
                {
                    SceneViewHelper.AlignViewToCamera(camera, view);
                }
            }
        }
    }
}