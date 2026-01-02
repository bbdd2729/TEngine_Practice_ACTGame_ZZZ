/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace InfinityCode.UltimateEditorEnhancer.UnityTypes
{
    public static class UnityEventDrawerRef
    {
        private static MethodInfo _buildPopupListMethod;
        private static MethodInfo _generatePopUpForTypeMethod;
        private static MethodInfo _getDummyEventMethod;

        private static Type _popupListType;

        private static MethodInfo buildPopupListMethod
        {
            get
            {
                if (_buildPopupListMethod == null)
                {
                    _buildPopupListMethod = Reflection.GetMethod(
                        type, 
                        "BuildPopupList", 
                        new[] { typeof(Object), typeof(UnityEventBase), typeof(SerializedProperty) }, 
                        Reflection.StaticLookup);
                }

                return _buildPopupListMethod;
            }
        }

        private static MethodInfo generatePopUpForTypeMethod
        {
            get
            {
                if (_generatePopUpForTypeMethod == null)
                {
                    Type[] types = new[]
                    {
#if UNITY_2022_1_OR_NEWER
                        typeof(GenericMenu), 
#else 
                        popupListType,
#endif
                        typeof(Object), 
                        typeof(string), 
                        typeof(SerializedProperty), 
                        typeof(Type[])
                    };

                    _generatePopUpForTypeMethod = Reflection.GetMethod(
                        type, 
                        "GeneratePopUpForType", 
                        types, 
                        Reflection.StaticLookup);
                }

                return _generatePopUpForTypeMethod;
            }
        }

        private static MethodInfo getDummyEventMethod
        {
            get
            {
                if (_getDummyEventMethod == null)
                {
                    _getDummyEventMethod = Reflection.GetMethod(
                        type, 
                        "GetDummyEvent", 
                        new[] { typeof(SerializedProperty) }, 
                        Reflection.StaticLookup);
                }
                
                return _getDummyEventMethod;
            }
        }

        public static Type popupListType
        {
            get
            {
                if (_popupListType == null)
                {
                    _popupListType = Reflection.GetEditorType("UnityEventDrawer+PopupList", "UnityEditorInternal");
                }

                return _popupListType;
            }
        }

        public static Type type => typeof(UnityEventDrawer);

        public static GenericMenu BuildPopupList(Object target, UnityEventBase dummyEvent, SerializedProperty listener)
        {
            return (GenericMenu) buildPopupListMethod.Invoke(
                null, 
                new object[]
                {
                    target, 
                    dummyEvent, 
                    listener
                }); 
        }

        public static void GeneratePopUpForType(
            object menu,
            Object target,
            string targetName,
            SerializedProperty listener,
            Type[] delegateArgumentsTypes)
        {
            #if !UNITY_2022_1_OR_NEWER
            menu = Activator.CreateInstance(popupListType, new object[] { menu });
            #endif
            
            object[] parameters = new object[]
            {
                menu, 
                target, 
                targetName,
                listener, 
                delegateArgumentsTypes
            };
            generatePopUpForTypeMethod.Invoke(
                null, 
                parameters);
        }

        public static UnityEventBase GetDummyEvent(SerializedProperty prop)
        {
            return getDummyEventMethod.Invoke(null, new object[] { prop }) as UnityEventBase;
        }
    }
}