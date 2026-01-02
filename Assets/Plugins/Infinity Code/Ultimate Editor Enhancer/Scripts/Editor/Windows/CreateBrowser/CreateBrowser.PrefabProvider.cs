/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace InfinityCode.UltimateEditorEnhancer.Windows
{
    public partial class CreateBrowser
    {
        public class PrefabProvider : Provider
        {
            public override float order => 1;
            public override string title => instance.prefabsLabel;

            private void AddItemToCache(string assetPath)
            {
                int i;
                string shortPath = assetPath.Substring(7);
                if (shortPath.Length < 8) return;
                
                string[] parts = shortPath.Split('/');
                if (parts.Length == 1)
                {
                    items.Add(new PrefabItem(shortPath, assetPath));
                }
                else
                {
                    string label = parts[0];
                    for (i = 0; i < items.Count; i++)
                    {
                        Item item = items[i];
                        if (!item.label.Equals(label, StringComparison.Ordinal)) continue;

                        PrefabItemFolder f = item as PrefabItemFolder;
                        f?.Add(parts, 0, assetPath);
                        return;
                    }
                    
                    items.Add(new PrefabItemFolder(parts, 0, assetPath));
                }
            }

            public override void Cache()
            {
                items = new List<Item>();

                string[] blacklist = Prefs.createBrowserBlacklist.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                CacheItems(blacklist, "t:prefab");
                CacheItems(blacklist, "t:model");

                foreach (Item item in items)
                {
                    PrefabItemFolder fi = item as PrefabItemFolder;
                    if (fi == null) continue;
                    fi.Simplify();
                }

                items = items.OrderBy(o =>
                {
                    if (o is FolderItem) return 0;
                    return -1;
                }).ThenBy(o => o.label).ToList();
            }

            private void CacheItems(string[] blacklist, string filter)
            {
                string[] assets = AssetDatabase.FindAssets(filter, new[] { "Assets" });
                if (blacklist.Length > 0)
                {
                    foreach (string guid in assets)
                    {
                        string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                        if (blacklist.Any(t => assetPath.StartsWith(t, StringComparison.Ordinal))) continue;
                        AddItemToCache(assetPath);
                    }
                }
                else
                {
                    foreach (string guid in assets)
                    {
                        string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                        AddItemToCache(assetPath);
                    }
                }
            }
        }
    }
}