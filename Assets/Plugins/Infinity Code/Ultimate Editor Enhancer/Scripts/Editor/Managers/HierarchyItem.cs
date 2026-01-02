/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Linq;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer
{
    public class HierarchyItem
    {
        public int id;
        public Rect rect;
        public GameObject gameObject;
        public Object target;
        public bool hovered;
        public bool selected;

        public void Set(int id, Rect rect)
        {
            this.id = id;
            this.rect = rect;

            target = Compatibility.EntityIdToObject(id);
            gameObject = target as GameObject;

            Vector2 p = Event.current.mousePosition;
            hovered = p.x >= 0 && p.x <= rect.xMax + 16 && p.y >= rect.y && p.y < rect.yMax;

#if UNITY_6000_3_OR_NEWER
            selected = Selection.entityIds.Contains(id);
#else
            selected = Selection.instanceIDs.Contains(id);
#endif
        }
    }
}