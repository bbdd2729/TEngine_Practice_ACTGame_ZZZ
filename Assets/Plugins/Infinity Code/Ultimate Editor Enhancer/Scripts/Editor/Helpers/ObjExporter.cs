using System.Globalization;
using System.Text;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.Windows.ObjectExporter
{
    public static class ObjExporter
    {
        public static string MeshToString(Mesh mesh, Material[] mats = null)
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            StringBuilder sb = new StringBuilder();
            
            sb.Append("#").Append(mesh.name).Append(".obj")
                .Append("\n#").Append(System.DateTime.Now.ToLongDateString())
                .Append("\n#").Append(System.DateTime.Now.ToLongTimeString())
                .Append("\n#-------")
                .Append("\n\n")
                .Append("g ").Append(mesh.name).Append("\n");

            foreach (Vector3 vertex in mesh.vertices)
            {
                sb.AppendFormat(culture, "v {0} {1} {2}\n", 
                    -vertex.x, vertex.y, vertex.z);
            }

            foreach (Vector2 uv in mesh.uv)
            {
                sb.AppendFormat(culture, "vt {0} {1}\n", uv.x, uv.y);
            }

            foreach (Vector3 normal in mesh.normals)
            {
                sb.AppendFormat(culture, "vn {0} {1} {2}\n", 
                    -normal.x, normal.y, normal.z);
            }

            for (int subMeshIndex = 0; subMeshIndex < mesh.subMeshCount; subMeshIndex++)
            {
                if (mats != null)
                {
                    if (subMeshIndex >= 0 && subMeshIndex < mats.Length)
                    {
                        Material mat = mats[subMeshIndex];
                        sb.Append("\n");
                        sb.Append("usemtl ").Append(mat.name).Append("\n");
                        sb.Append("usemap ").Append(mat.name).Append("\n");
                    }
                }

                int[] triangles = mesh.GetTriangles(subMeshIndex);

                for (int i = 0; i < triangles.Length; i += 3)
                {
                    int idx1 = triangles[i] + 1;
                    int idx2 = triangles[i + 1] + 1;
                    int idx3 = triangles[i + 2] + 1;

                    sb.AppendFormat(culture, "f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n", idx1, idx3, idx2);
                }
            }

            return sb.ToString();
        }
    }
}