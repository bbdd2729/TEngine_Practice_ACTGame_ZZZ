/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.UnityTypes
{
    public static class ITreeViewDataSourceRef
    {
        private static MethodInfo _isExpandedMethod;
        public static Type _type;

        public static MethodInfo isExpandedMethod
        {
            get
            {
                if (_isExpandedMethod == null)
                {
                    _isExpandedMethod = type.GetMethods().FirstOrDefault(m =>
                    {
                        if (m.Name != "IsExpanded") return false;
                        ParameterInfo[] parameters = m.GetParameters();
                        return parameters.Length == 1 && 
                               parameters[0].ParameterType == typeof(int);
                    });
                }
                return _isExpandedMethod;
            }
        }

        public static Type type
        {
            get
            {
                if (_type == null)
                {
#if UNITY_6000_2_OR_NEWER
                    _type = Reflection.GetEditorType("IMGUI.Controls.ITreeViewDataSource`1");
                    _type = _type.MakeGenericType(typeof(int));
#else
                    _type = Reflection.GetEditorType("IMGUI.Controls.ITreeViewDataSource");
#endif
                } ;
                return _type;
            }
        }

        public static bool IsExpanded(
            object instance, 
            int id)
        {
            return (bool)isExpandedMethod.Invoke(instance, new object[] {id});
        }
    }
}