/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;
using UnityEditor;

namespace InfinityCode.UltimateEditorEnhancer.UnityTypes
{
    public static class TerrainInspectorRef
    {
        private static Type _type;
        private static FieldInfo _activeTerrainInspectorField;
        private static FieldInfo _activeTerrainInspectorInstanceField;

        public static Type type
        {
            get
            {
                if (_type == null) _type = Reflection.GetEditorType("TerrainInspector");
                return _type;
            }
        }

        private static FieldInfo activeTerrainInspectorInstanceField
        {
            get
            {
                if (_activeTerrainInspectorInstanceField == null) _activeTerrainInspectorInstanceField = type.GetField("s_activeTerrainInspectorInstance", Reflection.StaticLookup);
                return _activeTerrainInspectorInstanceField;
            }
        }

        private static FieldInfo activeTerrainInspectorField
        {
            get
            {
                if (_activeTerrainInspectorField == null) _activeTerrainInspectorField = type.GetField("s_activeTerrainInspector", Reflection.StaticLookup);
                return _activeTerrainInspectorField;
            }
        }

        private static PropertyInfo _brushSizeProp;

        private static PropertyInfo brushSizeProp
        {
            get
            {
                if (_brushSizeProp == null) _brushSizeProp = type.GetProperty("brushSize", Reflection.InstanceLookup);
                return _brushSizeProp;
            }
        }

#if UNITY_2023_1_OR_NEWER
        private static PropertyInfo _selectedCategoryProp;
        
        private static PropertyInfo selectedCategoryProp
        {
            get
            {
                if (_selectedCategoryProp == null) _selectedCategoryProp = type.GetProperty("selectedCategory", Reflection.InstanceLookup);
                return _selectedCategoryProp;
            }
        }
#else
        private static PropertyInfo _selectedToolProp;

        private static PropertyInfo selectedToolProp
        {
            get
            {
                if (_selectedToolProp == null) _selectedToolProp = type.GetProperty("selectedTool", Reflection.InstanceLookup);
                return _selectedToolProp;
            }
        }
#endif

        public static Editor GetActiveTerrainInspectorInstance()
        {
            return activeTerrainInspectorInstanceField.GetValue(null) as Editor;
        }


        public static float GetBrushSize(Editor editor)
        {
            return (float)brushSizeProp.GetValue(editor);
        }

        public static int GetSelectedTool(Editor editor)
        {
#if UNITY_2023_1_OR_NEWER
            return (int)selectedCategoryProp.GetValue(editor);
#else
            return (int)selectedToolProp.GetValue(editor);
#endif
        }

        public static void SetBrushSize(Editor editor, float size)
        {
            brushSizeProp.SetValue(editor, size);
        }

        public static void SetActiveTerrainInspectorInstance(Editor editor)
        {
            activeTerrainInspectorInstanceField.SetValue(null, editor);
        }

        public static void SetActiveTerrainInspector(int id)
        {
            activeTerrainInspectorField.SetValue(null, id);
        }
    }
}