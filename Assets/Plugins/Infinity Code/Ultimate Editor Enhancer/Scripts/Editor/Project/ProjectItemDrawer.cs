/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.ProjectTools
{
    [InitializeOnLoad]
    public class ProjectItemDrawer
    {
        private static char[] assets = {'A', 's', 's', 'e', 't', 's'};
        
        public static Action<ProjectItem> OnStopped;
        private static List<Listener> listeners;
        private static bool isDirty;
        private static bool isStopped;
        private static ProjectItem item;
        private static string lastHoveredGUID;

        static ProjectItemDrawer()
        {
            if (Prefs.projectTools) Enable();
            item = new ProjectItem();
        }

        private static int CompareListeners(Listener i1, Listener i2)
        {
            if (i1.order == i2.order) return 0;
            if (i1.order > i2.order) return 1;
            return -1;
        }

        public static void Disable()
        {
            EditorApplication.projectWindowItemOnGUI -= OnProjectItemGUI;
        }

        public static void Enable()
        {
            EditorApplication.projectWindowItemOnGUI -= OnProjectItemGUI;
            EditorApplication.projectWindowItemOnGUI += OnProjectItemGUI;
        }

        private static void OnProjectItemGUI(string guid, Rect rect)
        {
            if (listeners == null) return;

            if (isDirty)
            {
                listeners.Sort(CompareListeners);
                isDirty = false;
            }

            EditorWindow mouseOverWindow = WindowsHelper.mouseOverWindow;
            if (mouseOverWindow && mouseOverWindow.GetType() == ProjectBrowserRef.type && mouseOverWindow.wantsMouseMove == false)
            {
                mouseOverWindow.wantsMouseMove = true;
            }

            item.Set(guid, rect);
            bool needRepaint = false;

            if (item.hovered && lastHoveredGUID != item.guid)
            {
                lastHoveredGUID = item.guid;
                needRepaint = true;
            }

            foreach (Listener listener in listeners)
            {
                if (listener.action != null)
                {
                    try
                    {
                        if (listener.onHoverOnly && !item.hovered) continue;
                        
                        string path = item.path;
                        if (path.Length < assets.Length) continue;

                        int i;
                        for (i = 0; i < assets.Length; i++)
                        {
                            if (path[i] != assets[i])
                            {
                                break;
                            }
                        }
                        if (i != assets.Length) continue;
                        
                        if (!string.IsNullOrEmpty(listener.folderName))
                        {
                            if (!item.isFolder) continue;
                            if (!path.Contains(listener.folderName)) continue;
                        }
                        listener.action(item);
                    }
                    catch (Exception e)
                    {
                        Log.Add(e);
                    }
                }
                if (isStopped) break;
            }

            if (needRepaint && mouseOverWindow) mouseOverWindow.Repaint();

            isStopped = false;
        }

        public static Listener Register(string id, Action<ProjectItem> action, float order = 0)
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("ID cannot be empty");
            if (listeners == null) listeners = new List<Listener>();

            int hash = id.GetHashCode();
            foreach (Listener listener in listeners)
            {
                if (listener.hash == hash && listener.id == id)
                {
                    listener.action = action;
                    listener.order = order;
                    return listener;
                }
            }

            Listener l = new Listener
            {
                id = id,
                hash = hash,
                action = action,
                order = order
            };
            listeners.Add(l);

            isDirty = true;
            return l;
        }

        public static void StopCurrentRowGUI()
        {
            isStopped = true;
            if (OnStopped != null) OnStopped(item);
        }

        public class Listener
        {
            public int hash;
            public string id;
            public Action<ProjectItem> action;
            public float order;

            private string _folderName;

            public bool onHoverOnly;

            public string folderName
            {
                get => _folderName;
                set
                {
                    onHoverOnly = true;
                    _folderName = value;
                }
            }
        }
    }
}