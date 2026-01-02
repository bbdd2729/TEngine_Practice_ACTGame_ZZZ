/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InfinityCode.UltimateEditorEnhancer
{
    [Serializable]
    public abstract class BookmarkItem : SearchableItem
    {
        public string title;
        public string type;
        public Object target;

        [NonSerialized]
        public Texture preview;

        [NonSerialized]
        public bool previewLoaded;
        
        [NonSerialized]
        private string _tooltip;
        
        [NonSerialized]
        private bool tooltipInitialized;
        
        public string tooltip
        {
            get
            {
                if (tooltipInitialized) return _tooltip;
                _tooltip = GetTooltip();
                tooltipInitialized = true;
                return _tooltip;
            }
        }

        public GameObject gameObject
        {
            get
            {
                if (!target) return null;
                if (target is Component) return (target as Component).gameObject;
                if (target is GameObject) return target as GameObject;
                return null;
            }
        }

        public abstract bool isProjectItem { get; }

        public virtual bool canBeRemoved => true;

        public BookmarkItem()
        {

        }

        public BookmarkItem(Object obj)
        {
            target = obj;

            Type t = obj.GetType();
            type = t.AssemblyQualifiedName;

            if (obj is GameObject)
            {
                GameObject go = obj as GameObject;
                title = go.name;
            }
            else if (obj is Component)
            {
                GameObject go = (obj as Component).gameObject;
                title = go.name + " (" + t.Name + ")";
            }
        }

        public void Dispose()
        {
            target = null;
            preview = null;
        }

        public static StringBuilder GetTransformPath(Transform t)
        {
            StringBuilder builder = StaticStringBuilder.Start();

            builder.Append(t.name);
            while ((t = t.parent))
            {
                builder.Insert(0, '/');
                builder.Insert(0, t.name);
            }

            return builder;
        }

        protected override int GetSearchCount()
        {
            return 1;
        }

        protected override string GetSearchString(int index)
        {
            return title;
        }

        protected virtual string GetTooltip()
        {
            if (target is GameObject)
            {
                return GetTransformPath(gameObject.transform).ToString();
            }

            if (target is Component)
            {
                return GetTransformPath(gameObject.transform).ToString() + "." + target.GetType().Name;
            }

            return null;
        }

        public abstract bool HasLabel(string label);

        public bool Update(string pattern, string valueType)
        {
            if (!string.IsNullOrEmpty(valueType) && type.IndexOf(valueType, StringComparison.OrdinalIgnoreCase) != -1) return false;
            return Match(pattern);
        }
    }
}