/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.UnityTypes
{
    public static class TreeViewGUIRef
    {
        private static FieldInfo _iconWidthField;
        private static FieldInfo _spaceBetweenIconAndTextField;
        private static Type _type;

        private static FieldInfo iconWidthField
        {
            get
            {
                if (_iconWidthField == null) _iconWidthField = type.GetField("k_IconWidth", Reflection.InstanceLookup | BindingFlags.FlattenHierarchy);
                return _iconWidthField;
            }
        }

        private static FieldInfo spaceBetweenIconAndTextField
        {
            get
            {
                if (_spaceBetweenIconAndTextField == null) _spaceBetweenIconAndTextField = type.GetField("k_SpaceBetweenIconAndText", Reflection.InstanceLookup | BindingFlags.FlattenHierarchy);
                return _spaceBetweenIconAndTextField;
            }
        }

        public static Type type
        {
            get
            {
                if (_type == null)
                {
#if UNITY_6000_3_OR_NEWER
                    _type = Reflection.GetEditorType("IMGUI.Controls.TreeViewGUI`1");
                    _type = _type.MakeGenericType(typeof(EntityId));
#else
                    _type = Reflection.GetEditorType("IMGUI.Controls.TreeViewGUI");
#endif
                }
                return _type;
            }
        }

        public static void SetIconWidth(object instance, float size)
        {
            iconWidthField.SetValue(instance, size);
        }

        public static void SetSpaceBetweenIconAndText(object instance, float space)
        {
            spaceBetweenIconAndTextField.SetValue(instance, space);
        }
    }
}