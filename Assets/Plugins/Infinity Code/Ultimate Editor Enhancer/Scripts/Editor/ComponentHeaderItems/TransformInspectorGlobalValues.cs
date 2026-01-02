/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using InfinityCode.UltimateEditorEnhancer.Interceptors;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InfinityCode.UltimateEditorEnhancer.ComponentHeader
{
    public class TransformInspectorGlobalValues: ComponentHeaderItem<Transform>
    {
        private static bool active;
        private static GUIStyle activeStyle;
        private static bool canUseSize;
        private static GUIContent content;
        private static GUIContent linkedContent;
        private static Vector3 originalScale;
        private static Vector3 originalSize;
        private static Vector3 position;
        private static bool proportional;
        private static Vector3 rotation;
        private static Vector3 scale;
        private static int scaleType;
        private static string[] sizeTypeTexts = { "World Scale", "World Size" };
        private static Units units = Units.meter; 
        private static GUIStyle style;
        private static Transform target;
        private static GUIContent unlinkedContent;

        protected override bool enabled => Prefs.transformInspectorGlobalValues;
        protected override bool isImportant => true;
        public override float order => ComponentHeaderButtonOrder.TransformGlobalValues;

        private static void Disable()
        {
            scaleType = 0;
            proportional = false;
            active = false;
            target = null;
            TransformInspectorInterceptor.OnInspector3DPrefix -= DrawInspector3D;
        }

        protected override bool DrawButton(Rect rect, Transform transform)
        {
            EditorGUI.BeginChangeCheck();
            active = GUI.Toggle(rect, active, content, active? activeStyle: style);
            if (EditorGUI.EndChangeCheck())
            {
                if (active) Enable(transform);
                else Disable();
            }

            return true;
        }

        private static bool DrawInspector3D(Editor editor)
        {
            EditorGUI.BeginChangeCheck();
            position = EditorGUILayout.Vector3Field("World Position", position);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Position");

                target.position = position;
                EditorUtility.SetDirty(target);
            }

            EditorGUI.BeginChangeCheck();
            rotation = EditorGUILayout.Vector3Field("World Rotation", rotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Rotation");

                target.rotation = Quaternion.Euler(rotation);
                EditorUtility.SetDirty(target);
            }

            DrawScale();

            return false;
        }

        private static void DrawScale()
        {
            if (scaleType == 0) DrawWorldScale();
            else DrawWorldSize();

            Rect rect = GUILayoutUtility.GetLastRect();

            const int proportionalWidth = 20;
            if (GUI.Button(new Rect(rect.x + EditorGUIUtility.labelWidth - proportionalWidth, rect.y, proportionalWidth, rect.height), proportional? linkedContent: unlinkedContent, GUIStyle.none))
            {
                proportional = !proportional;
                originalScale = target.lossyScale;
            }

            EditorGUI.BeginDisabledGroup(!canUseSize);
            scaleType = GUILayout.Toolbar(scaleType, sizeTypeTexts);
            if (scaleType == 1)
            {
                units = (Units)EditorGUILayout.EnumPopup("Units", units);
                EditorGUILayout.HelpBox("The world size is calculated based on the contained Renderers.", MessageType.None);
            }
            EditorGUI.EndDisabledGroup();

            if (!canUseSize)
            {
                EditorGUILayout.HelpBox("The world size is not available because the current and child GameObjects do not contain a Renderer.", MessageType.None);
            }
        }

        private static void DrawWorldScale()
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newScale = EditorGUILayout.Vector3Field("World Scale", scale);
            if (!EditorGUI.EndChangeCheck()) return;
            
            Undo.RecordObject(target, "Change Scale");

            if (proportional)
            {
                if (Math.Abs(scale.x - newScale.x) > float.Epsilon)
                {
                    if (Math.Abs(originalScale.x) > float.Epsilon)
                    {
                        newScale = originalScale * newScale.x / originalScale.x;
                    }
                    else newScale = new Vector3(newScale.x, newScale.x, newScale.x);
                }
                else if (Math.Abs(scale.x - newScale.x) > float.Epsilon)
                {
                    if (Math.Abs(originalScale.y) > float.Epsilon)
                    {
                        newScale = originalScale * newScale.y / originalScale.y;
                    }
                    else newScale = new Vector3(newScale.y, newScale.y, newScale.y);
                }
                else if (Math.Abs(scale.z - newScale.z) > float.Epsilon)
                {
                    if (Math.Abs(originalScale.z) > float.Epsilon)
                    {
                        newScale = originalScale * newScale.z / originalScale.z;
                    }
                    else newScale = new Vector3(newScale.z, newScale.z, newScale.z);
                }
            }

            GameObjectUtils.SetLossyScale(target, newScale);
            scale = newScale;
        }

        private static void DrawWorldSize()
        {
            EditorGUI.BeginChangeCheck();
            Vector3 size = GetSize();
            Vector3 newSize = EditorGUILayout.Vector3Field("World Size", size);
            if (!EditorGUI.EndChangeCheck()) return;
            
            SetSize(size, newSize);
        }

        private static void Enable(Transform transform)
        {
            InitTarget(transform);

            TransformInspectorInterceptor.OnInspector3DPrefix += DrawInspector3D;
            Selection.selectionChanged += Disable;
            Renderer[] renderers = target.GetComponentsInChildren<Renderer>();
            canUseSize = renderers.Length > 0;
            if (canUseSize)
            {
                originalSize = GameObjectUtils.GetOriginalBounds(target.gameObject).size;
            }
        }

        private static Vector3 GetSize()
        {
            Vector3 size = originalSize;
            size.Scale(scale);
            
            if (units == Units.inch) size *= 39.3701f;
            else if (units == Units.feet) size *= 3.28084f;
            else if (units == Units.yard) size *= 1.09361f;
            else if (units == Units.mile) size *= 0.000621371f;
            
            return size;
        }

        private static void InitTarget(Transform transform)
        {
            target = transform;
            rotation = target.rotation.eulerAngles;
            position = target.position;
            scale =  target.lossyScale;
        }

        protected override void Initialize()
        {
            content = new GUIContent(EditorIconContents.toolHandleGlobal.image, "Display transform values in world space.");
            linkedContent = new GUIContent(EditorIconContents.linked.image, "Disable constrained proportions");
            unlinkedContent = new GUIContent(EditorIconContents.unlinked.image, "Enable constrained proportions");

            style = new GUIStyle(Styles.iconButton)
            {
                name = null,
                alignment = TextAnchor.MiddleCenter,
            };

            activeStyle = new GUIStyle(style)
            {
                normal =
                {
                    background = Resources.CreateSinglePixelTexture( 110, 204, 204, 77)
                }
            };
        }

        private static void SetSize(Vector3 oldSize, Vector3 newSize)
        {
            if (units == Units.inch) newSize /= 39.3701f;
            else if (units == Units.feet) newSize /= 3.28084f;
            else if (units == Units.yard) newSize /= 1.09361f;
            else if (units == Units.mile) newSize /= 0.000621371f;

            if (proportional)
            {
                if (Math.Abs(oldSize.x - newSize.x) > float.Epsilon)
                {
                    if (Math.Abs(originalSize.x) > float.Epsilon) newSize = originalSize * newSize.x / originalSize.x;
                    else newSize.x = 1;
                }
                else if (Math.Abs(oldSize.y - newSize.y) > float.Epsilon)
                {
                    if (Math.Abs(originalSize.y) > float.Epsilon) newSize = originalSize * newSize.y / originalSize.y;
                    else newSize.y = 1;
                }
                else if (Math.Abs(oldSize.z - newSize.z) > float.Epsilon)
                {
                    if (Math.Abs(originalSize.z) > float.Epsilon) newSize = originalSize * newSize.z / originalSize.z;
                    else newSize.z = 1;
                }
            }

            if (Math.Abs(originalSize.x) > float.Epsilon) newSize.x /= originalSize.x;
            else newSize.x = 1;

            if (Math.Abs(originalSize.y) > float.Epsilon) newSize.y /= originalSize.y;
            else newSize.y = 1;

            if (Math.Abs(originalSize.z) > float.Epsilon) newSize.z /= originalSize.z;
            else newSize.z = 1;

            Undo.RecordObject(target, "Change Scale");

            target.localScale = scale = newSize;
        }

        protected override bool Validate(Transform target)
        {
            return !(target is RectTransform);
        }

        private enum Units
        {
            meter,
            inch,
            feet,
            yard,
            mile,
        }
    }
}