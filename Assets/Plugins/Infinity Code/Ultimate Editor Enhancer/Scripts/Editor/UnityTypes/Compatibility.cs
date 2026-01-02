/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections;
using InfinityCode.UltimateEditorEnhancer.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace InfinityCode.UltimateEditorEnhancer.UnityTypes
{
    [HideInIntegrity]
    public static class Compatibility
    {
        public static Object EntityIdToObject(int id)
        {
#if UNITY_6000_3_OR_NEWER
            return EditorUtility.EntityIdToObject(id);
#else
            return EditorUtility.InstanceIDToObject(id);
#endif
        }

        public static string GetAssetPath(int instanceId)
        {
#if UNITY_6000_3_OR_NEWER
            return AssetDatabase.GetAssetPath((EntityId)instanceId);
#else
            return AssetDatabase.GetAssetPath(instanceId);
#endif
        }
    }
}