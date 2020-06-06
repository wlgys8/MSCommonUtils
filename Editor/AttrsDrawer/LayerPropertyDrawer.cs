using UnityEngine;
using UnityEditor;

namespace MS.CommonUtils.Editor{
    [CustomPropertyDrawer(typeof(LayerFieldAttribute))]
    public class LayerPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label){
            return EditorGUI.GetPropertyHeight(SerializedPropertyType.Enum,label);
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Integer)
            {
                Debug.LogError("Layer property should be an integer ( the layer id )");
            }
            else
            {
                property.intValue = EditorGUI.LayerField(position,label,property.intValue);
            }
        }
    }
}
