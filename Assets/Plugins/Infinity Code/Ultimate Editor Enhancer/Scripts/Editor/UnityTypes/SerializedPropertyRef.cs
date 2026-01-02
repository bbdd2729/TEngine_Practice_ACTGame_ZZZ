/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;
using UnityEditor;

namespace InfinityCode.UltimateEditorEnhancer.UnityTypes
{
    public static class SerializedPropertyRef
    {
        public static Type type => typeof(SerializedProperty);

        private static PropertyInfo _hasMultipleDifferentValuesBitwiseProp;
        private static MethodInfo _setBitAtIndexForAllTargetsImmediateMethod;
        
        private static PropertyInfo hasMultipleDifferentValuesBitwiseProp
        {
            get
            {
                if (_hasMultipleDifferentValuesBitwiseProp == null)
                {
                    _hasMultipleDifferentValuesBitwiseProp = type.GetProperty("hasMultipleDifferentValuesBitwise", Reflection.InstanceLookup);
                }
                return _hasMultipleDifferentValuesBitwiseProp;
            }
        }
        
        private static MethodInfo setBitAtIndexForAllTargetsImmediateMethod
        {
            get
            {
                if (_setBitAtIndexForAllTargetsImmediateMethod == null)
                {
                    _setBitAtIndexForAllTargetsImmediateMethod = type.GetMethod("SetBitAtIndexForAllTargetsImmediate", Reflection.InstanceLookup);
                }
                return _setBitAtIndexForAllTargetsImmediateMethod;
            }
        }
        
        public static int HasMultipleDifferentValuesBitwise(object instance)
        {
            return (int)hasMultipleDifferentValuesBitwiseProp.GetValue(instance);
        }
        
        public static void SetBitAtIndexForAllTargetsImmediate(object instance, int index, bool value)
        {
            setBitAtIndexForAllTargetsImmediateMethod.Invoke(instance, new object[] { index, value });
        }
    }
}