/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer
{
    [InitializeOnLoad]
    public static class InputManager
    {
        public static Vector2 mousePosition { get; private set; }
        private static bool[] mouseStates = new bool[3];

        static InputManager()
        {
            SceneViewManager.AddListener(OnSceneView, SceneViewOrder.InputManager);
            GlobalEventManager.AddListener(OnGlobalEvent);
        }

        private static void OnGlobalEvent()
        {
            Event e = Event.current;
            mousePosition = e.mousePosition;
            EditorWindow wnd = EditorWindow.focusedWindow;
            if (wnd) mousePosition += wnd.position.position;
            
            if (e.type == EventType.MouseDown)
            {
                if (e.button < 3) mouseStates[e.button] = true;
            }
            else if (e.type == EventType.MouseUp)
            {
                if (e.button < 3) mouseStates[e.button] = false;
            }
        }

        private static void OnSceneView(SceneView view)
        {
            Event e = Event.current;
            mousePosition = e.mousePosition + view.position.position;
            
            if (e.type == EventType.MouseDown)
            {
                if (e.button < 3) mouseStates[e.button] = true;
            }
            else if (e.type == EventType.MouseUp)
            {
                if (e.button < 3) mouseStates[e.button] = false;
            }
        }

        public static bool GetAnyMouseButton()
        {
            return mouseStates[0] || mouseStates[1] || mouseStates[2];
        }

        public static bool GetMouseButton(int button)
        {
            if (button < 0 || button > 2) return false;
            return mouseStates[button];
        }
    }
}