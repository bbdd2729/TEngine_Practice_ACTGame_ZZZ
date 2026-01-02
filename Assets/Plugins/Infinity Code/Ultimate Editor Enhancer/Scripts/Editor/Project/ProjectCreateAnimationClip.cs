/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.ProjectTools
{
    [InitializeOnLoad]
    public static class ProjectCreateAnimationClip
    {
        static ProjectCreateAnimationClip()
        {
            ProjectItemDrawer.Listener listener = ProjectItemDrawer.Register(nameof(ProjectCreateAnimationClip), DrawButton, ProjectToolOrder.CreateAnimationClip);
            listener.folderName = "Animations";
        }

        private static void CreateAnimationClip(ProjectItem item)
        {
            Selection.activeObject = item.asset;
            AnimationClip clip = new AnimationClip();
            ProjectWindowUtil.CreateAsset(clip, "New Animation.anim");
        }

        private static void DrawButton(ProjectItem item)
        {
            if (!Prefs.projectCreateAnimationClip) return;

            Rect r = item.rect;
            r.xMin = r.xMax - 18;
            r.height = 16;

            item.rect.xMax -= 18;

            if (GUI.Button(r, TempContent.Get(EditorIconContents.animationClip.image, "Create Animation Clip"), GUIStyle.none))
            {
                CreateAnimationClip(item);
            }
        }
    }
}