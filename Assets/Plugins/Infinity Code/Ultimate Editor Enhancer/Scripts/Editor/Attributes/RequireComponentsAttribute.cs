/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.Attributes
{
    public class RequireComponentsAttribute : ValidateAttribute
    {
        private Type[] types;

        public RequireComponentsAttribute(params Type[] types)
        {
            this.types = types;
        }

        public override bool Validate()
        {
            GameObject go = Selection.activeGameObject;
            return go && types.Any(type => go.GetComponent(type));
        }
    }
}