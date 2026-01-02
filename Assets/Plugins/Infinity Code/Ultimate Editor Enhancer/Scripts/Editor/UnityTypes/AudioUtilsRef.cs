/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.UnityTypes
{
    public static class AudioUtilsRef
    {
        private static MethodInfo _isClipPlayingMethod;
        private static MethodInfo _playClipMethod;
        private static MethodInfo _stopAllClipsMethod;
        private static MethodInfo _stopClipMethod;
        private static Type _type;

        private static MethodInfo isClipPlayingMethod
        {
            get
            {
                if (_isClipPlayingMethod == null)
                {
                    _isClipPlayingMethod = type.GetMethod("IsPreviewClipPlaying", Reflection.StaticLookup);
                }
                return _isClipPlayingMethod;
            }
        }

        private static MethodInfo playClipMethod
        {
            get
            {
                if (_playClipMethod == null)
                {
                    _playClipMethod = type.GetMethod("PlayPreviewClip", Reflection.StaticLookup, null, new[] { typeof(AudioClip), typeof(int), typeof(bool) }, null);
                }
                return _playClipMethod;
            }
        }

        private static MethodInfo stopAllClipsMethod
        {
            get
            {
                if (_stopAllClipsMethod == null)
                {
                    _stopAllClipsMethod = type.GetMethod("StopAllPreviewClips", Reflection.StaticLookup);
                }
                return _stopAllClipsMethod;
            }
        }

        private static MethodInfo stopClipMethod
        {
            get
            {
                if (_stopClipMethod == null)
                {
                    _stopClipMethod = type.GetMethod("PausePreviewClip", Reflection.StaticLookup);
                }
                return _stopClipMethod;
            }
        }

        public static Type type
        {
            get
            {
                if (_type == null) _type = Reflection.GetEditorType("AudioUtil");
                return _type;
            }
        }

        public static bool IsClipPlaying(AudioClip clip)
        {
            return (bool)isClipPlayingMethod.Invoke(null, Array.Empty<object>());
        }

        public static void PlayClip(AudioClip clip, int startSample = 0, bool loop = false)
        {
            playClipMethod.Invoke(null, new object[] { clip, startSample, loop});
        }

        public static void StopAllClips()
        {
            stopAllClipsMethod.Invoke(null, null);
        }

        public static void StopClip(AudioClip clip)
        {
            stopClipMethod.Invoke(null, Array.Empty<object>());
        }
    }
}