/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Linq;

namespace InfinityCode.UltimateEditorEnhancer
{
    [Serializable]
    public class ProjectBookmark : BookmarkItem
    {
        [NonSerialized]
        private string _path;

        public override bool isProjectItem => true;

        public string path
        {
            get
            {
#if UNITY_EDITOR
                if (_path == null) _path = UnityEditor.AssetDatabase.GetAssetPath(target);
#else
                _path = null;
#endif

                return _path;
            }
        }

        public ProjectBookmark()
        {

        }

        public ProjectBookmark(UnityEngine.Object obj):base(obj)
        {
            title = obj.name;
        }

        protected override string GetTooltip()
        {
            return path;
        }

        public override bool HasLabel(string label)
        {
#if UNITY_EDITOR
            return UnityEditor.AssetDatabase.GetLabels(target).Contains(label);
#else
            return false;
#endif
        }
    }
}