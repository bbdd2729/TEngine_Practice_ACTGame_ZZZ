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
        public class CreateProvider : Provider
        {
            public string[] skipMenuItems = {
                "Center On Children",
                "Create Empty Child",
                "Create Empty Parent",
                "Group",
                "Ungroup",
                "Make Parent",
                "Clear Parent",
                "Set as first sibling",
                "Set as last sibling",
                "Move to View",
                "Align With View",
                "Align View to Selected",
                "Toggle Active State",
            };

            public override float order => 0;
            public override string title => instance.createLabel;

            public override void Cache()
            {
                items = new List<Item>();

                foreach (string submenu in Unsupported.GetSubmenus("GameObject"))
                {
                    string menu = submenu.Substring(11);
                    if (skipMenuItems.Any(i => menu.Equals(i, StringComparison.OrdinalIgnoreCase))) continue;
                    
                    string[] parts = PrepareMenuItem(menu);
                    string firstPart = parts[0];
                    if (parts.Length == 1)
                    {
                        CreateItem item = new CreateItem(firstPart, submenu);
                        if (firstPart == "Empty GameObject") item.order = -1;
                        items.Add(item);
                    }
                    else
                    {
                        CreateItemFolder root = items.FirstOrDefault(i => i.label == firstPart) as CreateItemFolder;
                        if (root != null) root.Add(parts, 0, submenu);
                        else items.Add(new CreateItemFolder(parts, 0, submenu));
                    }
                }

                items = items.OrderBy(o => o.order).ThenBy(o => o.label).ToList();
            }

            public override int IndexOf(Item item)
            {
                return items.IndexOf(item);
            }

            private static string[] PrepareMenuItem(string menu)
            {
                return menu switch
                {
                    "Create Empty" => new []{ "Empty GameObject" },
                    "Create Empty Collection" => new []{ "Collection" },
                    "Create Header" => new []{ "Header" },
                    _ => menu.Split("/", StringSplitOptions.RemoveEmptyEntries)
                };
            }
        }
    }
}