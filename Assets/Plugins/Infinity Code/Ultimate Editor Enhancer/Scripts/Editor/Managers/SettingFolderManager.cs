/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.IO;

namespace InfinityCode.UltimateEditorEnhancer
{
    public static class SettingFolderManager
    {
        private const string FolderName = "Ultimate Editor Enhancer";
        
        public static string GetFolder(string path)
        {
            string folder = FolderName + "/" + path;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return folder;
        }
    }
}