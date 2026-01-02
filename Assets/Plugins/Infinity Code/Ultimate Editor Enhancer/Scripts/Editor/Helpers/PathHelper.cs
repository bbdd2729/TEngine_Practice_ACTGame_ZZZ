/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.IO;

namespace InfinityCode.UltimateEditorEnhancer
{
    public static class PathHelper
    {
        public static string GetUniquePath(string path)
        {
            if (!File.Exists(path)) return path;

            int i = 1;
            string newPath;
            string dir = Path.GetDirectoryName(path);
            string fn = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);
            
            do
            {
                newPath = Path.Combine(dir, $"{fn} ({i++}){ext}");
            } while (File.Exists(newPath));

            return newPath;
        }
    }
}