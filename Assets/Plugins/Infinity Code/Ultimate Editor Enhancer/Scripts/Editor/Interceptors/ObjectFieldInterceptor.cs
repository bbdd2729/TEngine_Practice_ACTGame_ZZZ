/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InfinityCode.UltimateEditorEnhancer.Interceptors
{
    public class ObjectFieldInterceptor: StatedInterceptor<ObjectFieldInterceptor>
    {
        public delegate void GUIDelegate(Rect position,
            Rect dropRect,
            int id,
            Object obj,
            Object objBeingEdited,
            Type objType,
            Type additionalType,
            SerializedProperty property,
            object validator,
            bool allowSceneObjects,
            GUIStyle style);

        public static GUIDelegate OnGUIBefore;

        private MethodInfo _originalMethod;

        protected override MethodInfo originalMethod
        {
            get
            {
                if (_originalMethod == null)
                {
                    Type validatorType = typeof(EditorGUI).GetNestedType(
                        "ObjectFieldValidatorOptions"
                        , BindingFlags.Public | BindingFlags.NonPublic);

                    Type[] parameters = {
                        typeof(Rect),
                        typeof(Rect),
                        typeof(int),
                        typeof(Object),
                        typeof(Object),
                        typeof(Type),
                        typeof(Type),
                        typeof(SerializedProperty),
                        validatorType,
                        typeof(bool),
                        typeof(GUIStyle)
#if UNITY_2022_1_OR_NEWER
                        , typeof(GUIStyle)
#endif
#if UNITY_2022_2_OR_NEWER
                        , typeof(Action<Object>)
                        , typeof(Action<Object>)
#endif
                    };

                    MethodInfo[] methods = typeof(EditorGUI).GetMethods(Reflection.StaticLookup);
                    foreach (MethodInfo info in methods)
                    {
                        if (info.Name != "DoObjectField") continue;
                        ParameterInfo[] ps = info.GetParameters();
                        if (ps.Length != parameters.Length) continue;

                        _originalMethod = info;
                        break;
                    }
                }

                return _originalMethod;
            }
        }

        protected override string prefixMethodName
        {
            get => nameof(DoObjectFieldPrefix);
        }

        public override bool state
        {
            get => Prefs.objectFieldSelector;
        }

        private static void DoObjectFieldPrefix(
            Rect position,
            Rect dropRect,
            int id,
            Object obj,
            Object objBeingEdited,
            Type objType,
            Type additionalType,
            SerializedProperty property,
            object validator,
            bool allowSceneObjects,
            GUIStyle style
#if UNITY_2022_1_OR_NEWER
            ,GUIStyle buttonStyle
#endif
            )
        {
            if (OnGUIBefore != null)
            {
                OnGUIBefore(position, dropRect, id, obj, objBeingEdited, objType, additionalType, property, validator, allowSceneObjects, style);
            }
        }
    }
}