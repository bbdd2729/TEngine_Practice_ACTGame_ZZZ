/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.Editors
{
    [CustomEditor(typeof(Rigidbody))]
    public class RigidbodyEditor : Editor
    {
        private bool visible = false;
        private Editor editor;

        public void DrawSpeed()
        {
            if (!EditorApplication.isPlaying) return;

            visible = EditorGUILayout.Foldout(visible, "Speed");
            if (!visible) return;

            Rigidbody rb = (Rigidbody)target;
#if UNITY_6000_0_OR_NEWER
            Vector3 velocity = rb.linearVelocity;
#else
            Vector3 velocity = rb.velocity;
#endif

            float speedMs = velocity.magnitude;
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("M/s", speedMs.ToString("F3"));
            EditorGUILayout.TextField("Km/h", (speedMs * 3.6f).ToString("F3"));
            EditorGUILayout.TextField("Mph", (speedMs * 2.23694f).ToString("F3"));
            EditorGUI.EndDisabledGroup();
        }

        private void OnDisable()
        {
            if (!editor) return;
            
            DestroyImmediate(editor);
            editor = null;
        }

        public void OnEnable()
        {
            Type type = Reflection.GetEditorType("RigidbodyEditor");
            if (type == null) return;

            editor = CreateEditor(targets, type);
        }

        public override void OnInspectorGUI()
        {
            if (editor) editor.OnInspectorGUI();
            else base.OnInspectorGUI();

            DrawSpeed();
        }
    }
}