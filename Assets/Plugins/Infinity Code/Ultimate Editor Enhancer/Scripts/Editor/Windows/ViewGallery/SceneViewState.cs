/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfinityCode.UltimateEditorEnhancer
{
    [Serializable]
    public class SceneViewState
    {
        public string scenePath;
        public Vector3 pivot;
        public float size;
        public Quaternion rotation;
        public string title;
        public bool is2D;
        public bool useInPreview = true;

        [SerializeField]
        private string _id;
        
        [NonSerialized]
        private Texture2D _texture;
        
        [NonSerialized]
        private string _texturePath;

        public string id
        {
            get
            {
                if (!string.IsNullOrEmpty(_id)) return _id;
                _id = Guid.NewGuid().ToString();
                return _id;
            }
        }
        
        public string texturePath
        {
            get
            {
                if (!string.IsNullOrEmpty(_texturePath)) return _texturePath;
                string folder = SettingFolderManager.GetFolder("ViewGallery/Textures");
                _texturePath = $"{folder}/{id}.png"; 
                return _texturePath;
            }
        }

        public Texture2D texture
        {
            get
            {
                if (_texture) return _texture;
                
                string path = texturePath;
                if (!File.Exists(path)) return null;
                byte[] bytes = File.ReadAllBytes(path);
                Texture2D t = new Texture2D(1, 1);
                t.LoadImage(bytes);
                _texture = t;
                return _texture; 
            }
            set
            {
                if (_texture == value) return;
                _texture = value;

                string path = texturePath;

                if (!_texture)
                {
                    if (File.Exists(path)) File.Delete(path);
                }
                else
                {
                    byte[] bytes = _texture.EncodeToPNG();
                    File.WriteAllBytes(path, bytes);
                }
            }
        }

        public Vector3 position
        {
            get
            {
                if (is2D) return pivot - Vector3.forward * size;
                return pivot - rotation * Vector3.forward * GetPerspectiveCameraDistance(size, 60);
            }
        }

        public static float GetPerspectiveCameraDistance(float size, float fov)
        {
            return size / Mathf.Sin((float)(fov * 0.5 * (Math.PI / 180.0)));
        }

        public void SetView(Camera camera)
        {
            Transform t = camera.transform;

            if (!is2D)
            {
                camera.orthographic = false;
                camera.fieldOfView = 60;
                t.position = pivot - rotation * Vector3.forward * GetPerspectiveCameraDistance(size, 60);
                t.rotation = rotation;
            }
            else
            {
                camera.orthographic = true;
                camera.orthographicSize = size;
                t.position = pivot - Vector3.forward * size;
                t.rotation = Quaternion.identity;
            }
        }

        public bool TryLoad()
        {
            int i;
            for (i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene s = SceneManager.GetSceneAt(i);
                if (s.isLoaded && s.path == scenePath) break;
            }
            if (i < SceneManager.sceneCount) return true;
            
            bool success = EditorUtility.DisplayDialog("View Gallery",
                $"Do you want to load scene \"{Path.GetFileNameWithoutExtension(scenePath)}\" and set view?",
                "OK", "Cancel");
            
            if (!success) return false;
            if (!SceneManagerHelper.AskForSave()) return false;
            
            Scene scene = SceneManager.GetSceneByPath(scenePath);
            if (scene.IsValid())
            {
                if (!scene.isLoaded)
                {
                    scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
                }
                SceneManager.SetActiveScene(scene);
                return true;
            }

            scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            if (scene.IsValid())
            {
                SceneManager.SetActiveScene(scene);
                return true;
            }

            Debug.LogError($"Failed to load scene: {scenePath}");
            return false;
        }
    }
}