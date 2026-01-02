/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using System.Linq;
using InfinityCode.UltimateEditorEnhancer.References;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.HierarchyTools
{
    [InitializeOnLoad]
    public static class BackgroundDrawer
    {
        public static Texture2D backgroundTexture;

        static BackgroundDrawer()
        {
            HierarchyItemDrawer.Register(nameof(BackgroundDrawer), OnDrawItem, HierarchyToolOrder.Background);
        }

        private static BackgroundRule GetBackgroundRule(GameObject gameObject)
        {
            if (BackgroundRuleReferences.count == 0) return null;
            return BackgroundRuleReferences.FirstOrDefault(rule => rule.Validate(gameObject));
        }

        private static void InitTexture()
        {
            if (Prefs.hierarchyRowBackgroundStyle == Prefs.HierarchyRowBackgroundStyle.gradient)
            {
                backgroundTexture = Resources.Load<Texture2D>("Textures/Other/HierarchyBackground.png");
                if (!backgroundTexture) backgroundTexture = Resources.CreateSinglePixelTexture(1, 0.3f);
            }
            else backgroundTexture = Resources.CreateSinglePixelTexture(1, 0.3f);
        }

        private static void OnDrawItem(HierarchyItem item)
        {
            if (!Prefs.hierarchyRowBackground) return;
            if (Event.current.type != EventType.Repaint) return;

            GameObject target = item.gameObject;
            if (target == null) return;

            SceneReferences r = SceneReferences.Get(item.gameObject.scene, false);
            if (r == null) return;

            Color? color = null;
            SceneReferences.HierarchyBackground background = r.GetBackground(item.gameObject);

            if (background != null)
            {
                color = background.color;
            }
            else
            {
                BackgroundRule rule = GetBackgroundRule(item.gameObject);
                if (rule != null) color = rule.color;
            }
            
            if (!color.HasValue) return;
            
            if (backgroundTexture == null) InitTexture();

            Color guiColor = GUI.color;
            GUI.color = color.Value;
            Rect rect = item.rect;
            rect.xMin += 16;
            GUI.DrawTexture(rect, backgroundTexture, ScaleMode.StretchToFill);
            GUI.color = guiColor;
        }
    }
}