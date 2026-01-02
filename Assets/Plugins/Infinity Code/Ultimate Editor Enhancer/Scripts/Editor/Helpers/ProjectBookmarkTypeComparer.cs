/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using UnityEditor;

namespace InfinityCode.UltimateEditorEnhancer
{
    public class ProjectBookmarkTypeComparer : IComparer<ProjectBookmark>
    {
        public int Compare(ProjectBookmark x, ProjectBookmark y)
        {
            bool f1 = x.target is DefaultAsset;
            bool f2 = y.target is DefaultAsset;
            if (f1 != f2) return f1 ? -1 : 1;

            Type t1 = x.target.GetType();
            Type t2 = y.target.GetType();
            if (t1 == t2) return string.Compare(x.title, y.title, StringComparison.OrdinalIgnoreCase);
            return string.Compare(t1.Name, t2.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}