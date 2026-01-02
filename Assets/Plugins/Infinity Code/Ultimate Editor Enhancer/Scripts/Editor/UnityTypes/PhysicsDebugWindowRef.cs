/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;
using UnityEditor;

namespace InfinityCode.UltimateEditorEnhancer.UnityTypes
{
    public static class PhysicsDebugWindowRef
    {
        public static Type type => typeof(PhysicsDebugWindow);

#if UNITY_2022_1_OR_NEWER
        private static MethodInfo _updateSelectionOnComponentAddMethod;
        
        private static MethodInfo updateSelectionOnComponentAddMethod
        {
            get
            {
                if (_updateSelectionOnComponentAddMethod == null)
                {
                    _updateSelectionOnComponentAddMethod = type.GetMethod("UpdateSelectionOnComponentAdd", Reflection.StaticLookup);
                }
                return _updateSelectionOnComponentAddMethod;
            }
        }
        
        public static void UpdateSelectionOnComponentAdd()
        {
            updateSelectionOnComponentAddMethod.Invoke(null, null);
        }
#endif
    }
}