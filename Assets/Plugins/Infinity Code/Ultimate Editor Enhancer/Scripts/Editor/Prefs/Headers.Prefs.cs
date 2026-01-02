/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using InfinityCode.UltimateEditorEnhancer.References;
using UnityEditor;

namespace InfinityCode.UltimateEditorEnhancer
{
    public static partial class Prefs
    {
        public static bool hierarchyHeaders = true;
        public static string hierarchyHeaderPrefix = "--";

        public class HeadersManager : StandalonePrefManager<HeadersManager>
        {
            private static SerializedObject so;
            private SerializedProperty prop;

            public override void Draw()
            {
                hierarchyHeaders = EditorGUILayout.ToggleLeft("Headers", hierarchyHeaders);
                EditorGUI.BeginDisabledGroup(!hierarchyHeaders);

                if (so == null) so = new SerializedObject(HeaderRuleReferences.instance);
                so.Update();

                if (prop == null) prop = so.FindProperty("_items");
                EditorGUILayout.PropertyField(prop, true);
                so.ApplyModifiedProperties();

                EditorGUI.EndDisabledGroup();
            }
            
            public override void SetState(bool state)
            {
                base.SetState(state);
                
                hierarchyHeaders = state;
            }
        }
    }
}