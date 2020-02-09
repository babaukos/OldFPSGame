// ver. 0.1
// Скрипт класса отрисовщика PropertyDrawer 
// Mikle Khmelevsky

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DrawIfAttribute))]
public class DrawIfPropertyDrawer : PropertyDrawer
{
    #region Fields

    // Reference to the attribute on the property.
    DrawIfAttribute drawIf;

    // Field that is being compared.
    SerializedProperty comparedField;

    #endregion

    /// <summary>
    /// Errors default to showing the property.
    /// </summary>
    private bool ShowMe(SerializedProperty property)
    {
        drawIf = attribute as DrawIfAttribute;
        // заменим имя свойства на значение из параметра
        string path = property.propertyPath.Contains(".") ? System.IO.Path.ChangeExtension(property.propertyPath, drawIf.comparedPropertyName) : drawIf.comparedPropertyName;

        comparedField = property.serializedObject.FindProperty(path);

        if (comparedField == null)
        {
            Debug.LogError("Cannot find property with name: " + path);
            return true;
        }
        
        // получить значение и сравнить его по типу
        switch (comparedField.type)
        {
            // Возможные варианты расширения для поддержки собственного типа
            case "bool":
                return comparedField.boolValue.Equals(drawIf.comparedValue);
            case "Enum":
                return comparedField.enumValueIndex.Equals((int)drawIf.comparedValue);
            default:
                Debug.LogError("Error: " + comparedField.type + " is not supported of " + path);
                return true;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // тип отключения, которое должно произойти, если условие НЕ выполняется
        if (!ShowMe(property) && drawIf.disablingType == DisablingType.DontDraw)
        {
            return 0f;
        }
        // тип отключения, которое должно произойти, если условие выполняется
        else if (ShowMe(property) && (drawIf.disablingType == DisablingType.Draw))
        {
            return 0f;
        }
        else
        {
            // высота свойства должна быть по умолчанию равна высоте по умолчанию.
            return base.GetPropertyHeight(property, label);
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Если условие выполнено, просто нарисуйте поле.
        if (ShowMe(property))
        {
            EditorGUI.PropertyField(position, property);
        }
        else if (!ShowMe(property) && drawIf.disablingType == DisablingType.Draw) 
        {
            EditorGUI.PropertyField(position, property);
        }
        //... проверьте, доступен ли тип отключения только для чтения. Если это так, нарисуйте его отключенным
        else if (ShowMe(property) && drawIf.disablingType == DisablingType.ReadOnly)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property);
            GUI.enabled = true;
        }
    }
}