using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ruler))]

public class RuleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Texture texture = (Texture2D)EditorGUIUtility.Load("Assets/Tools/Ruler/GUI/icon_rule.png");
        texture.filterMode = FilterMode.Point;

        Ruler routerClass = (Ruler)target;

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(texture);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}
