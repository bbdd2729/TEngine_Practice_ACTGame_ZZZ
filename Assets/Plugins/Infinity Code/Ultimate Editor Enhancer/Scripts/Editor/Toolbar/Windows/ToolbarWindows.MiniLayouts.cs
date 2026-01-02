/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using System.Text;
using InfinityCode.UltimateEditorEnhancer.References;

namespace InfinityCode.UltimateEditorEnhancer.Tools
{
    public static partial class ToolbarWindows
    {
        public class MiniLayouts : Provider
        {
            public override float order => 1;

            public override void GenerateMenu(GenericMenuEx menu, ref bool hasItems)
            {
                MiniLayout[] miniLayouts = MiniLayoutReferences.items;
                if (miniLayouts.Length > 0)
                {
                    StringBuilder sb = StaticStringBuilder.Start();

                    foreach (MiniLayout miniLayout in miniLayouts)
                    {
                        sb.Append("Mini Layouts/");
                        sb.Append(miniLayout.name);
                        menu.Add(sb.ToString(), Restore, miniLayout);
                        sb.Clear();
                    }
                    
                    menu.AddSeparator("Mini Layouts/");
                }
                
                menu.Add("Mini Layouts/Edit", Settings.OpenMiniLayoutsSettings);
                
                hasItems = true;
            }

            private void Restore(object userdata)
            {
                MiniLayout miniLayout = (MiniLayout) userdata;
                if (miniLayout == null) return;
                LayoutHelper.LoadLayoutFromText(miniLayout.data);
            }
        }
    }
}