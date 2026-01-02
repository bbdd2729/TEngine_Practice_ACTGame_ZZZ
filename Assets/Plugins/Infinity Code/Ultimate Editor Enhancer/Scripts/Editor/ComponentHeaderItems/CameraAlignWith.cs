/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using InfinityCode.UltimateEditorEnhancer.Attributes;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.ComponentHeader
{
    public class CameraAlignWith: ComponentHeaderItem<Camera>
    {
        private static GUIContent content;

        protected override bool enabled => Prefs.cameraAlignWith;
        public override float order => ComponentHeaderButtonOrder.CameraAlignWith;
        protected override bool isImportant => true;

        protected override bool DrawButton(Rect rectangle, Camera target)
        {
            if (GUI.Button(rectangle, content, Styles.iconButton))
            {
                ShowMenu(target);
            }

            return true;
        }

        protected override void Initialize()
        {
            content = new GUIContent(EditorGUIUtility.isProSkin? Icons.align : Icons.alignDark, "Align With");
        }

        private static void ShowMenu(Component target)
        {
            GenericMenuEx menu = GenericMenuEx.Start();
            menu.Add("Frame Selected", () => SceneView.FrameLastActiveSceneView());
            menu.Add("Move To View", SceneView.lastActiveSceneView.MoveToView);
            menu.Add("Align With View", SceneView.lastActiveSceneView.AlignWithView);
            menu.Add("Align View To Selected", () => SceneView.lastActiveSceneView.AlignViewToObject(target.transform));
            menu.Show();
        }
    }
}