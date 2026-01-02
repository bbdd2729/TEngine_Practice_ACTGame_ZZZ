/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InfinityCode.UltimateEditorEnhancer.UnityTypes
{
    public static class ContainerWindowRef
    {
        private static MethodInfo _setMinMaxSizesMethod;
        private static MethodInfo _showMethod;
        private static FieldInfo _showModeField;
        private static FieldInfo _rootViewField;
        private static Type _type;

        private static FieldInfo rootViewField
        {
            get
            {
                if (_rootViewField == null)
                {
                    _rootViewField = type.GetField("m_RootView", Reflection.InstanceLookup);
                }

                return _rootViewField;
            }
        }
        
        private static MethodInfo setMinMaxSizesMethod
        {
            get
            {
                if (_setMinMaxSizesMethod == null)
                {
                    _setMinMaxSizesMethod = Reflection.GetMethod(type, "SetMinMaxSizes", new[] { typeof(Vector2), typeof(Vector2) }, Reflection.InstanceLookup);
                }

                return _setMinMaxSizesMethod;
            }
        }
        
        private static MethodInfo showMethod
        {
            get
            {
                if (_showMethod == null)
                {
                    _showMethod = type.GetMethod("Show", Reflection.InstanceLookup, null, new[] {ShowModeRef.type, typeof(bool), typeof(bool), typeof(bool)}, null);
                }

                return _showMethod;
            }
        }

        private static FieldInfo showModeField
        {
            get
            {
                if (_showModeField == null)
                {
                    _showModeField = type.GetField("m_ShowMode", Reflection.InstanceLookup);
                }

                return _showModeField;
            }
        }

        private static Type type
        {
            get
            {
                if (_type == null)
                {
                    _type = Reflection.GetEditorType("ContainerWindow");
                }

                return _type;
            }
        }

        public static Object GetRootView(Object containerWindow)
        {
            return rootViewField.GetValue(containerWindow) as Object;
        }
        
        public static int GetShowMode(Object containerWindow)
        {
            return (int) showModeField.GetValue(containerWindow);
        }
        
        public static void Show(Object containerWindow, int showMode, bool loadPosition, bool displayImmediately, bool setFocus)
        {
            showMethod.Invoke(containerWindow, new object[] {showMode, loadPosition, displayImmediately, setFocus});
        }

        public static void SetMinMaxSizes(Object containerWindow, Vector2 minSize, Vector2 maxSize)
        {
            setMinMaxSizesMethod.Invoke(containerWindow, new object[] {minSize, maxSize});
        }
    }
}