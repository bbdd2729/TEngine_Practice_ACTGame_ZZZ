/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Linq;
using System.Text.RegularExpressions;
using InfinityCode.UltimateEditorEnhancer.References;
using InfinityCode.UltimateEditorEnhancer.Tools;
using InfinityCode.UltimateEditorEnhancer.Windows;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace InfinityCode.UltimateEditorEnhancer.EditorMenus.Actions
{
    public class SceneViewActions : ActionItem
    {
        protected override bool closeOnSelect
        {
            get { return false; }
        }

        private void AlignViewToCamera(object userdata)
        {
            SceneViewHelper.AlignViewToCamera(userdata as Camera);
            EditorMenu.Close();
        }

        private void AlignViewToSelected()
        {
            SceneView.lastActiveSceneView.AlignViewToObject(targets[0].transform);
            EditorMenu.Close();
        }

        private void AlignWithView()
        {
            SceneView.lastActiveSceneView.AlignWithView();
            EditorMenu.Close();
        }

        private static Camera CreateCameraFromSceneView()
        {
            if (!EditorApplication.ExecuteMenuItem("GameObject/Camera")) return null;

            Camera sceneViewCamera = SceneView.lastActiveSceneView.camera;
            Camera camera = Selection.activeGameObject.GetComponent<Camera>();
            camera.transform.position = sceneViewCamera.transform.position;
            camera.transform.rotation = sceneViewCamera.transform.rotation;
            return camera;
        }

        [MenuItem(WindowsHelper.MenuPath + "Cameras/Create Permanent", false, MenuItemOrder.CameraCreatePermanent)]
        private static void CreatePermanentCameraFromSceneView()
        {
            CreateCameraFromSceneView();
        }

        [MenuItem(WindowsHelper.MenuPath + "Cameras/Create Temporary", false, MenuItemOrder.CameraCreateTemporary)]
        private static void CreateTemporaryCameraFromSceneView()
        {
            GameObject container = TemporaryContainer.GetContainer();
            if (container == null) return;

            string pattern = @"Camera \((\d+)\)";

            int maxIndex = 1;
            Camera[] cameras = container.GetComponentsInChildren<Camera>();
            for (int i = 0; i < cameras.Length; i++)
            {
                string name = cameras[i].gameObject.name;
                Match match = Regex.Match(name, pattern);
                if (match.Success)
                {
                    string strIndex = match.Groups[1].Value;
                    int index = Int32.Parse(strIndex);
                    if (index >= maxIndex) maxIndex = index + 1;
                }
            }

            string defaultName = "Camera (" + maxIndex + ")";
            InputDialog.Show("Enter name of Camera GameObject", defaultName, s =>
            {
                Camera camera = CreateCameraFromSceneView();
                if (camera == null) return;

                camera.gameObject.name = !string.IsNullOrEmpty(s) ? s : defaultName;
                camera.farClipPlane = Mathf.Max(camera.farClipPlane, SceneView.lastActiveSceneView.size * 2);

                camera.transform.SetParent(container.transform, true);
                camera.tag = "EditorOnly";
            });
        }

        private void DeleteAllViewStates()
        {
            ViewStateReferences.Clear();
            EditorMenu.Close();
        }

        private void DeleteViewState(object userdata)
        {
            ViewStateReferences.Remove((userdata as SceneViewState));
            EditorMenu.Close();
        }

        private void FrameSelected()
        {
            SceneView.FrameLastActiveSceneView();
            EditorMenu.Close();
        }

        protected override void Init()
        {
            guiContent = new GUIContent(Icons.focus, "Views and Cameras");
        }

        private void InitCreateCameraFromViewMenu(GenericMenuEx menu)
        {
            menu.Add("Create Camera From View/Permanent", CreatePermanentCameraFromSceneView);
            menu.Add("Create Camera From View/Temporary", CreateTemporaryCameraFromSceneView);
        }

        private void InitAlignViewToCameraMenu(GenericMenuEx menu)
        {
            Camera[] cameras = ObjectHelper.FindObjectsOfType<Camera>().OrderBy(c => c.name).ToArray();
            if (cameras.Length > 0)
            {
                for (int i = 0; i < cameras.Length; i++)
                {
                    menu.Add("Align View To Camera/" + cameras[i].gameObject.name, AlignViewToCamera, cameras[i]);
                }
            }
        }

        private void InitViewStatesMenu(GenericMenuEx menu)
        {
            menu.Add("View States/Gallery", ViewGallery.OpenWindow);
            menu.AddSeparator("View States/");
            menu.Add("View States/Create", SaveViewState);

            SceneViewState[] views = ViewStateReferences.items.OrderBy(v => v.title).ToArray();
            if (views.Length == 0) return;
            
            for (int i = 0; i < views.Length; i++)
            {
                menu.Add("View States/Restore/" + views[i].title, RestoreViewState, views[i]);

                if (i == 0)
                {
                    menu.Add("View States/Delete/All States", DeleteAllViewStates);
                    menu.AddSeparator("View States/Delete/");
                }
                menu.Add("View States/Delete/" + views[i].title, DeleteViewState, views[i]);
            }
        }

        public override void Invoke()
        {
            GenericMenuEx menu = GenericMenuEx.Start();

            InitCreateCameraFromViewMenu(menu);
            InitAlignViewToCameraMenu(menu);
            InitViewStatesMenu(menu);

            if (targets != null && targets.Length > 0 && targets[0] != null)
            {
                menu.Add("Frame Selected", FrameSelected);
                menu.Add("Move To View", MoveToView);
                menu.Add("Align With View", AlignWithView);
                menu.Add("Align View To Selected", AlignViewToSelected);
            }

            menu.Show();
        }

        private void MoveToView()
        {
            SceneView.lastActiveSceneView.MoveToView();
            EditorMenu.Close();
        }

        private void RestoreViewState(object userdata)
        {
            SceneViewState state = userdata as SceneViewState;
            SceneView view = SceneView.lastActiveSceneView;
            view.in2DMode = state.is2D;
            view.pivot = state.pivot;
            view.size = state.size;
            if (!view.in2DMode) view.rotation = state.rotation;
            EditorMenu.Close();
        }

        [MenuItem(WindowsHelper.MenuPath + "View States/Create", false, MenuItemOrder.ViewStatesCreate)]
        public static void SaveViewState()
        {
            string pattern = @"View State \((\d+)\)";

            int maxIndex = 1;
            SceneViewState[] viewStates = ViewStateReferences.items;
            for (int i = 0; i < viewStates.Length; i++)
            {
                string name = viewStates[i].title;
                Match match = Regex.Match(name, pattern);
                if (match.Success)
                {
                    string strIndex = match.Groups[1].Value;
                    int index = int.Parse(strIndex);
                    if (index >= maxIndex) maxIndex = index + 1;
                }
            }

            string viewStateName = "View State (" + maxIndex + ")";
            InputDialog.Show("Enter title of View State", viewStateName, s =>
            {
                SceneViewState viewState = new SceneViewState();
                ViewStateReferences.Add(viewState);

                SceneView view = SceneView.lastActiveSceneView;
                viewState.scenePath = SceneManager.GetActiveScene().path;
                viewState.pivot = view.pivot;
                viewState.rotation = view.rotation;
                viewState.size = view.size;
                viewState.is2D = view.in2DMode;
                viewState.title = s;
                
                EditorMenu.Close();
                ViewGallery.RepaintAll();
            });
        }
    }
}