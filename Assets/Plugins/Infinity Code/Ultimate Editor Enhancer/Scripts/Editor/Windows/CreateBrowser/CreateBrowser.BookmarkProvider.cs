/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using InfinityCode.UltimateEditorEnhancer.References;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.Windows
{
    [InitializeOnLoad]
    public partial class CreateBrowser
    {
        public class BookmarkProvider : Provider
        {
            public override float order => -1;

            public override string title => "Bookmarks";

            public override void Cache()
            {
                items = new List<Item>();

                string goType = typeof(GameObject).AssemblyQualifiedName;
                foreach (var item in BookmarkReferences.items)
                {
                    if (item.type != goType) continue;
                    
                    string path = AssetDatabase.GetAssetPath(item.target);
                    if (item.title.Length == 0) continue;

                    items.Add(new PrefabItem(item.title + ".prefab", path));
                }
            }

            public override void Filter(string pattern, List<Item> filteredItems)
            {

            }
        }
    }
}