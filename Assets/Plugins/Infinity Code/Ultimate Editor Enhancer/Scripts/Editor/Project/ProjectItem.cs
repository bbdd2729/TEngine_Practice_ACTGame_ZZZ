/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.ProjectTools
{
    public class ProjectItem
    {
        private const double CACHE_LIFETIME = 10;
        private static Dictionary<string, CacheItem> cache = new Dictionary<string, CacheItem>();
        
        public string guid;
        public string path;
        public Rect rect;
        public bool hovered;
        public bool isFolder;

        public Object asset => ProjectAssetCache.Get<Object>(path);

        public void Set(string guid, Rect rect)
        {
            this.guid = guid;
            this.rect = rect;
            
            CacheItem item;
            Vector2 p = Event.current.mousePosition;
            hovered = rect.Contains(p);
            
            if (cache.TryGetValue(guid, out item))
            {
                path = item.path;
                isFolder = item.isFolder;
                return;
            }

            path = AssetDatabase.GUIDToAssetPath(guid);
            isFolder = !string.IsNullOrEmpty(path) && Directory.Exists(path);
            
            item.path = path;
            item.isFolder = isFolder;
            cache[guid] = item;
        }

        private struct CacheItem
        {
            public string path;
            public bool isFolder;
        }
    }
}