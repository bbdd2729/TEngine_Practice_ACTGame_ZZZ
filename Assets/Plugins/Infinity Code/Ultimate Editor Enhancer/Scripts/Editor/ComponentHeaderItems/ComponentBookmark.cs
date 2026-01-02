/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using InfinityCode.UltimateEditorEnhancer.Windows;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.ComponentHeader
{
    public class ComponentBookmark: ComponentHeaderItem<Component>
    {
        protected override bool enabled => Prefs.headerBookmarks;
        public override float order => ComponentHeaderButtonOrder.Bookmark;

        protected override bool DrawButton(Rect rect, Component target)
        {
            Texture emptyTexture = Styles.isProSkin? Icons.starWhite: Icons.starBlack;
            bool contain = Bookmarks.Contains(target);
            Texture texture = contain? Icons.starYellow : emptyTexture;
            GUIContent content = TempContent.Get(texture, contain? "Remove Bookmark": "Add Bookmark");

            ButtonEvent buttonEvent = GUILayoutUtils.Button(rect, content, GUIStyle.none);
            if (buttonEvent != ButtonEvent.click) return true;
            
            Event e = Event.current;
            if (e.button == 0)
            {
                if (contain) Bookmarks.Remove(target);
                else Bookmarks.Add(target);
            }
            else if (e.button == 1)
            {
                Bookmarks.ShowWindow();
            }
            e.Use();

            return true;
        }
    }
}