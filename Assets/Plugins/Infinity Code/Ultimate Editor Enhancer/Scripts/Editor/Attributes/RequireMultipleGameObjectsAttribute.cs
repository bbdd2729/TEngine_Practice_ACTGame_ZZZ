/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using UnityEditor;

namespace InfinityCode.UltimateEditorEnhancer.Attributes
{
    public class RequireMultipleGameObjectsAttribute : ValidateAttribute
    {
        public RequireMultipleGameObjectsAttribute()
        {

        }

        public override bool Validate()
        {
            return Selection.gameObjects.Length > 1;
        }
    }
}