/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.References
{
    public abstract class ReferenceBase<T, U>: ScriptableObject, IReferenceBase where T : ReferenceBase<T, U>
    {
        private static T _instance;
        
        [SerializeField]
        private U[] _items;
        
        public static U[] items
        {
            get => instance._items;
            set
            {
                instance._items = value; 
                Save();
            }
        }

        public static int count => items.Length;

        public static T instance
        {
            get
            {
                if (!_instance) Load();
                return _instance;
            }
        }
        
        protected abstract string filename { get; }

        public static void Add(U item)
        {
            ArrayUtility.Add(ref instance._items, item);
            Save();
        }
        
        public static bool All(Func<U, bool> func)
        {
            return instance._items.All(func);
        }
        
        public static bool Any(Func<U, bool> func)
        {
            return instance._items.Any(func);
        }

        public static void Clear()
        {
            instance._items = Array.Empty<U>();
            Save();
        }

        public static bool Contains(U item)
        {
            return instance._items.Contains(item);
        }

        public static U FirstOrDefault(Func<U, bool> func)
        {
            return instance._items.FirstOrDefault(func);
        }

        public static int IndexOf(U item)
        {
            if (instance._items == null) return -1;
            return Array.IndexOf(instance._items, item);
        }

        public static void Insert(int index, U item)
        {
            if (index < 0 || index > instance._items.Length) return;
            ArrayUtility.Insert(ref instance._items, index, item);
            Save();
        }

        private static void Load()
        {
            Type type = typeof(T);
            PropertyInfo propertyInfo = type.GetProperty("filename", BindingFlags.Instance | BindingFlags.NonPublic);
            object obj = FormatterServices.GetUninitializedObject(type);
            string filename = propertyInfo.GetValue(obj, null) as string;

            string path = Resources.settingsFolder + filename + ".asset";
            try
            {
                if (File.Exists(path))
                {
                    try
                    {
                        _instance = AssetDatabase.LoadAssetAtPath<T>(path);
                        if (_instance && _instance._items == null)
                        {
                            _instance._items = Array.Empty<U>();
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Add(e);
                    }

                }

                if (!_instance)
                {
                    _instance = CreateInstance<T>();
                    _instance._items = Array.Empty<U>();

#if !UEE_IGNORE_SETTINGS
                    FileInfo info = new FileInfo(path);
                    if (!info.Directory.Exists) info.Directory.Create();

                    AssetDatabase.CreateAsset(_instance, path);
                    AssetDatabase.SaveAssets();
#endif
                }
            }
            catch (Exception e)
            {
                Log.Add(e);
            }
        }

        protected virtual void OnResetContent()
        {
            _items = Array.Empty<U>();
            Save();
        }

        public static void ResetContent()
        {
            instance.OnResetContent();
        }

        public static void Remove(U item)
        {
            ArrayUtility.Remove(ref instance._items, item);
            Save();
        }

        public static int RemoveAll(Predicate<U> match)
        {
            List<U> list = items.ToList();
            int removed = list.RemoveAll(match);
            items = list.ToArray();
            Save();
            return removed;
        }
        
        public static void RemoveAt(int index)
        {
            if (index < 0 || index >= instance._items.Length) return;
            ArrayUtility.RemoveAt(ref instance._items, index);
            Save();
        }
        
        public static void Save()
        {
            try
            {
                EditorUtility.SetDirty(_instance); 
            }
            catch { }
        }
        
        public static IEnumerable<V> Select<V>(Func<U, V> selector)
        {
            return instance._items.Select(selector);
        }
        
        public static void Sort(IComparer<U> comparer)
        {
            U[] array = instance._items;
            if (array.Length < 2) return;
            Array.Sort(array, comparer);
            Save();
        }
        
        public static IEnumerable<U> Where(Func<U, bool> func)
        {
            return instance._items.Where(func);
        }
    }
}