/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.ProjectTools
{
    [InitializeOnLoad]
    public static class ProjectCreateMaterial
    {
        static ProjectCreateMaterial()
        {
            ProjectItemDrawer.Listener listener = ProjectItemDrawer.Register(nameof(ProjectCreateMaterial), DrawButton, ProjectToolOrder.CreateMaterial);
            listener.folderName = "Materials";
        }

        private static void CreateMaterial(ProjectItem item)
        {
            Selection.activeObject = item.asset;
            Material material = new Material(RenderPipelineHelper.GetDefaultShader());
            ProjectWindowUtil.CreateAsset(material, "New Material.mat");
        }

        private static void DrawButton(ProjectItem item)
        {
            if (!Prefs.projectCreateMaterial) return;

            Rect r = item.rect;
            r.xMin = r.xMax - 18;
            r.height = 16;

            item.rect.xMax -= 18;

            if (GUI.Button(r, TempContent.Get(EditorIconContents.material.image, "Create Material"), GUIStyle.none))
            {
                CreateMaterial(item);
            }
        }
    }
}