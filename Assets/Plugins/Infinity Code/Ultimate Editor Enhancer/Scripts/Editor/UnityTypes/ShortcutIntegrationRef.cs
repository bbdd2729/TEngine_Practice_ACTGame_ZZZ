/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;

namespace InfinityCode.UltimateEditorEnhancer.UnityTypes
{
    public static class ShortcutIntegrationRef
    {
        private static Type _type;

        private static Type type
        {
            get
            {
                if (_type == null) _type = Reflection.GetEditorType("ShortcutIntegration", "UnityEditor.ShortcutManagement");
                return _type;
            }
        }

        
        private static PropertyInfo _ignoreWhenPlayModeFocusedProp;

        private static PropertyInfo ignoreWhenPlayModeFocusedProp
        {
            get
            {
                if (_ignoreWhenPlayModeFocusedProp == null) _ignoreWhenPlayModeFocusedProp = type.GetProperty("ignoreWhenPlayModeFocused", Reflection.StaticLookup);
                return _ignoreWhenPlayModeFocusedProp;
            }
        }
        
        public static bool GetIgnoreWhenPlayModeFocused()
        {
            return (bool)ignoreWhenPlayModeFocusedProp.GetValue(null, null);
        }
    }
}