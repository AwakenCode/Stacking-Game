using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Drawer for the RequireInterface attribute.
/// </summary>
[CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
public class RequireInterfaceDrawer : PropertyDrawer
{
    /// <summary>
    /// Overrides GUI drawing for the attribute.
    /// </summary>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Get attribute parameters.
        RequireInterfaceAttribute requiredAttribute = (RequireInterfaceAttribute) attribute;

        // Begin drawing property field.
        EditorGUI.BeginProperty(position, label, property);

        UnityEngine.Object reference = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(UnityEngine.Object), true);

        if (reference is UnityEngine.Object obj)
            reference = obj.GetComponent(requiredAttribute.RequiredType);
        else
        {
            var previousColor = GUI.color;
            GUI.color = Color.red;
            label.tooltip = $"Required interface {requiredAttribute.RequiredType}";
            EditorGUI.LabelField(position, label);

            // Revert color change.
            GUI.color = previousColor;
        }

        property.objectReferenceValue = reference;

        // Finish drawing property field.
        EditorGUI.EndProperty();
    }
}