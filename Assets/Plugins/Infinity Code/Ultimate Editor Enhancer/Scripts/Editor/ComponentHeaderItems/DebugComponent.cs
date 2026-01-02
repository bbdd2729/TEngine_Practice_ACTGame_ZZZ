/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using InfinityCode.UltimateEditorEnhancer.Windows;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.ComponentHeader
{
    public class DebugComponent : ComponentHeaderItem<Component>
    {
        private static GUIContent content;

        protected override bool enabled => Prefs.debugComponentHeaderButton;
        public override float order => ComponentHeaderButtonOrder.Debug;

        protected override void Initialize()
        {
            content = new GUIContent(Icons.debug, "Debug Component");
        }

        protected override bool DrawButton(Rect rect, Component component)
        {
            ButtonEvent buttonEvent = GUILayoutUtils.Button(rect, content, GUIStyle.none);
            if (buttonEvent == ButtonEvent.click && Event.current.button == 0)
            {
                ComponentWindow.Show(component).SetDebugMode(true);
            }

            return true;
        }
    }
}