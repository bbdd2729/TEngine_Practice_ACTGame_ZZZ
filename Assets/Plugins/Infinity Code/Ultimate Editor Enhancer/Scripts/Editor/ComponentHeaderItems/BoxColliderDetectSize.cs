/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.ComponentHeader
{
    public class BoxColliderDetectSize: ComponentHeaderItem<BoxCollider>
    {
        private static GUIContent content;
        private static GUIStyle style;
        private static Vector3[] fourCorners;

        protected override bool enabled => Prefs.boxColliderDetect;
        protected override bool isImportant => true;
        public override float order => ComponentHeaderButtonOrder.BoxColliderDetectSize;

        protected override bool DrawButton(Rect rect, BoxCollider target)
        {
            if (GUI.Button(rect, content, style))
            {
                UpdateBounds(target);
            }

            return true;
        }

        protected override void Initialize()
        {
            content = new GUIContent(EditorIconContents.rectTransformBlueprint.image, "Detect Bounds");
            style = new GUIStyle(Styles.iconButton)
            {
                alignment = TextAnchor.MiddleCenter
            };
        }

        private void UpdateBounds(BoxCollider collider)
        {
            GameObject gameObject = collider.gameObject;
            Bounds bounds = GameObjectUtils.GetOriginalBounds(gameObject);
            if (bounds.size == Vector3.zero) return;

            Undo.RecordObject(collider, "Update Collider Bounds");

            collider.center = bounds.center - gameObject.transform.position;
            collider.size = bounds.size;
        }
    }
}