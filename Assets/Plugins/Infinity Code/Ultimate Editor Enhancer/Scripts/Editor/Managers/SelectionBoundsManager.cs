/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer
{
    [InitializeOnLoad]
    public static class SelectionBoundsManager
    {
        public static Action OnChanged;
        private static Bounds _bounds;
        private static Vector3[] fourCorners = new Vector3[4];

        public static Bounds bounds => _bounds;

        public static GameObject firstGameObject
        {
            get
            {
                if (renderers.Count > 0)
                {
                    Renderer first = renderers.FirstOrDefault(r => r);
                    if (first) return first.gameObject;
                }

                if (rectTransforms.Count > 0)
                {
                    RectTransform first = rectTransforms.FirstOrDefault(r => r);
                    if (first) return first.gameObject;
                }

                return null;
            }
        }

        public static bool hasBounds { get; private set; }

        public static bool isRectTransform => renderers.Count == 0 && rectTransforms.Count > 0;

        private static List<RectTransform> rectTransforms { get; }
        private static List<Renderer> renderers { get; }

        static SelectionBoundsManager()
        {
            renderers = new List<Renderer>();
            rectTransforms = new List<RectTransform>();
            Selection.selectionChanged += OnSelectionChanged;

            SceneViewManager.AddListener(OnSceneView);

            OnSelectionChanged();
        }

        private static void OnSceneView(SceneView scene)
        {
            if (!hasBounds) return;
            if (Event.current.type != EventType.Layout) return;

            Bounds b = new Bounds();

            bool isFirst = true;

            if (renderers.Count > 0)
            {
                for (int i = 0; i < renderers.Count; i++)
                {
                    Renderer r = renderers[i];
                    if (!r) continue;

                    if (isFirst)
                    {
                        b = r.bounds;
                        isFirst = false;
                    }
                    else
                    {
                        b.Encapsulate(r.bounds);
                    }
                }
            }

            if (rectTransforms.Count > 0)
            {
                for (int i = 0; i < rectTransforms.Count; i++)
                {
                    RectTransform rt = rectTransforms[i];
                    if (!rt) continue;

                    rt.GetWorldCorners(fourCorners);

                    if (isFirst)
                    {
                        b = new Bounds(fourCorners[0], Vector3.zero);
                        isFirst = false;
                    }

                    for (int j = 0; j < 4; j++)
                    {
                        b.Encapsulate(fourCorners[j]);
                    }
                }
            }

            if (b != _bounds)
            {
                _bounds = b;
                if (OnChanged != null) OnChanged();
            }
        }

        private static void OnSelectionChanged()
        {
            renderers.Clear();
            rectTransforms.Clear();
            hasBounds = false;

#if UNITY_6000_3_OR_NEWER
            int[] instanceIDs = Selection.entityIds.Select(i => (int)i).ToArray();
#else
            int[] instanceIDs = Selection.instanceIDs;
#endif

            bool isFirst = true;

            foreach (int instanceID in instanceIDs)
            {
                GameObject gameObject = Compatibility.EntityIdToObject(instanceID) as GameObject;

                if (!gameObject || gameObject.scene.name == null) continue;

                Renderer[] rs = gameObject.GetComponentsInChildren<Renderer>();
                if (rs != null && rs.Length != 0)
                {
                    renderers.AddRange(rs);

                    foreach (Renderer renderer in rs)
                    {
                        if (isFirst)
                        {
                            _bounds = renderer.bounds;
                            isFirst = false;
                        }
                        else
                        {
                            _bounds.Encapsulate(renderer.bounds);
                        }
                    }
                }

                RectTransform[] rts = gameObject.GetComponentsInChildren<RectTransform>();
                if (rts != null && rts.Length > 0)
                {
                    rectTransforms.AddRange(rts);

                    foreach (RectTransform rt in rts)
                    {
                        rt.GetWorldCorners(fourCorners);

                        if (isFirst)
                        {
                            _bounds = new Bounds(fourCorners[0], Vector3.zero);
                            isFirst = false;
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            _bounds.Encapsulate(fourCorners[i]);
                        }
                    }
                }
            }

            if (!isFirst) hasBounds = true;
            if (OnChanged != null) OnChanged();
        }
    }
}