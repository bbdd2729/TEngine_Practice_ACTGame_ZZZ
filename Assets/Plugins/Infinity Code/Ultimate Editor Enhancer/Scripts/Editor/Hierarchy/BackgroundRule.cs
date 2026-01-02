/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using InfinityCode.UltimateEditorEnhancer.JSON;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.HierarchyTools
{
    [Serializable]
    public class BackgroundRule : ISerializationCallbackReceiver
    {
        public bool enabled = true;
        public BackgroundCondition condition = BackgroundCondition.nameEqual;
        public string value;
        public Color color = Color.gray;
        
        [SerializeField, HideInInspector]
        private bool _serialized;

        public JsonObject json => Json.Serialize(this) as JsonObject;

        public void OnAfterDeserialize()
        {
            if (_serialized) return;
            
            _serialized = true;
            
            enabled = true;
            condition = BackgroundCondition.nameStarts;
            value = "";
            color = Color.gray;
        }

        public void OnBeforeSerialize()
        {
            
        }

        public bool Validate(GameObject go)
        {
            if (!enabled) return false;

            string name = go.name;
            return condition switch
            {
                BackgroundCondition.nameStarts => StringHelper.StartsWith(name, value),
                BackgroundCondition.nameContains => StringHelper.Contains(name, value),
                BackgroundCondition.nameEqual => name == value,
                _ => false
            };
        }
    }
}