/*           INFINITY CODE          */
/*     https://infinity-code.com    */

#if UNITY_6000_3_OR_NEWER
using UnityEditor.Toolbars;
#endif

using System;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.Tools
{
    [InitializeOnLoad]
    public static class Timer
    {
        public static Action OnLeftClick;
        private static GUIContent content;

        static Timer()
        {
#if !UNITY_6000_3_OR_NEWER
            ToolbarManager.AddLeftToolbar("Timer", OnGUI);
#else
            EditorApplication.playModeStateChanged += OnPlaymodeStateChanged;
#endif
        }

        private static void DrawIcon()
        {
            GUIContent iconContent = TempContent.Get(Icons.timer, "Timescale");
            if (Math.Abs(Time.timeScale - 1) > float.Epsilon) iconContent.text = "x" + Time.timeScale.ToString("F2");

            Vector2 size = Styles.appToolbarButtonLeft.CalcSize(iconContent);
            ButtonEvent buttonEvent = GUILayoutUtils.Button(iconContent, Styles.appToolbarButtonLeft, GUILayout.Width(size.x), GUILayout.Height(18));

            if (buttonEvent != ButtonEvent.click) return;

            Event e = Event.current;

            if (e.button == 0)
            {
                if (OnLeftClick != null) OnLeftClick();
            }
            else if (e.button == 1) ShowContextMenu();
        }

        private static void DrawTimer()
        {
            float time = Time.time;
            int totalSec = Mathf.FloorToInt(time);
            int hour = totalSec / 3600;
            int min = totalSec / 60 % 60;
            int sec = totalSec % 60;
            int ms = Mathf.RoundToInt((time - (int)time) * 1000);

            float width = 68;

            StringBuilder builder = StaticStringBuilder.Start();
            if (hour > 0)
            {
                builder.Append(hour).Append(":");
                width += EditorStyles.textField.CalcSize(TempContent.Get(hour.ToString())).x;
            }

            if (min < 10) builder.Append("0");
            builder.Append(min).Append(":");
            if (sec < 10) builder.Append("0");
            builder.Append(sec).Append(".");
            if (ms < 100) builder.Append("0");
            if (ms < 10) builder.Append("0");
            builder.Append(ms);

            if (content == null) content = new GUIContent(builder.ToString(), "Time since the start of the game.");
            else content.text = builder.ToString();

            Rect rect = GUILayoutUtility.GetRect(content, EditorStyles.textField, GUILayout.Width(width));
            GUILayoutUtils.lastRect = rect;

            Event e = Event.current;
            if (e.type == EventType.Repaint)
            {
                EditorStyles.textField.Draw(rect, content, false, false, false, false);
                GUI.changed = true;
            }
            else if (e.type == EventType.MouseDown)
            {
                if (rect.Contains(e.mousePosition))
                {
                    if (e.button == 0)
                    {
                        if (OnLeftClick != null) OnLeftClick();
                    }
                    else if (e.button == 1) ShowContextMenu();
                }
            }
        }

        private static void OnGUI()
        {
            if (Prefs.timerMode == TimerMode.icon) DrawIcon();
            else if (Prefs.timerMode == TimerMode.timer)
            {
                if (EditorApplication.isPlaying) DrawTimer();
                else DrawIcon();
            }
        }

        private static void SetMode(TimerMode mode)
        {
            Prefs.timerMode = mode;
            Prefs.Save();

#if UNITY_6000_3_OR_NEWER
            Refresh();
#endif
        }

        private static void ShowContextMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Timer"), Prefs.timerMode == TimerMode.timer, () => SetMode(TimerMode.timer));
            menu.AddItem(new GUIContent("Icon"), Prefs.timerMode == TimerMode.icon, () => SetMode(TimerMode.icon));
            menu.AddItem(new GUIContent("Hide"), Prefs.timerMode == TimerMode.hide, () => SetMode(TimerMode.hide));
            menu.AddSeparator(string.Empty);
            menu.AddItem(new GUIContent("Config"), false, Settings.OpenToolbarSettings);
            menu.ShowAsContext();
        }

#if UNITY_6000_3_OR_NEWER
        private const string ToolbarPath = "Ultimate Editor Enhancer/Timer";
        private static int lastRefreshTime = 0;

        public static void Refresh() => MainToolbar.Refresh(ToolbarPath);

        [MainToolbarElement(ToolbarPath, defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement CreateElement()
        {
            if (Prefs.timerMode == TimerMode.timer && EditorApplication.isPlaying)
            {
                return CreateTimerElement();
            }

            return CreateIconElement();
        }

        private static MainToolbarElement CreateTimerElement()
        {
            float time = Time.time;
            int totalSec = Mathf.FloorToInt(time);
            int hour = totalSec / 3600;
            int min = totalSec / 60 % 60;
            int sec = totalSec % 60;
            int ms = Mathf.RoundToInt((time - (int)time) * 1000);

            StringBuilder builder = StaticStringBuilder.Start();
            if (hour > 0) builder.Append(hour).Append(":");

            if (min < 10) builder.Append("0");
            builder.Append(min).Append(":");
            if (sec < 10) builder.Append("0");
            builder.Append(sec).Append(".");
            if (ms < 100) builder.Append("0");
            if (ms < 10) builder.Append("0");
            builder.Append(ms);

            content = TempContent.Get(builder.ToString(), "Time since the start of the game.");
            return new MainToolbarButton(content, () =>
            {
                OnLeftClick();
                Refresh();
            });
        }

        private static MainToolbarElement CreateIconElement()
        {
            GUIContent iconContent = TempContent.Get(Icons.timer, "Timescale");
            if (Math.Abs(Time.timeScale - 1) > float.Epsilon) iconContent.text = "x" + Time.timeScale.ToString("F2");
            return new MainToolbarButton(iconContent, () =>
            {
                OnLeftClick();
                Refresh();
            });
        }
        
        private static void OnPlaymodeStateChanged(PlayModeStateChange state)
        {
            EditorApplication.update -= Update;
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                lastRefreshTime = 0;
                EditorApplication.update += Update;
            }

            Refresh();
        }
        
        private static void Update()
        {
            int t = (int)(Time.realtimeSinceStartupAsDouble * 10);
            if (t > lastRefreshTime)
            {
                Refresh();
                lastRefreshTime = t;
            }
        }

#else
        public static void Refresh() {}
#endif
    }
}