using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WayContainer))]
public class SphereEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var styleAddButton = new GUIStyle(GUI.skin.button);
        styleAddButton.normal.textColor = Color.green;

        var styleRemovButton = new GUIStyle(GUI.skin.button);
        styleRemovButton.normal.textColor = Color.red;

        WayContainer routerClass = (WayContainer)target;

        if (GUILayout.Button("Add Point", styleAddButton)) 
        {
            routerClass.AddPoint();
            Selection.SetActiveObjectWithContext(routerClass.waypoints[routerClass.waypoints.Count - 1].gameObject, null);
        }
        if (GUILayout.Button("Remove Point", styleRemovButton))
        {
            routerClass.RemovePoint();
            Selection.SetActiveObjectWithContext(routerClass.waypoints[routerClass.waypoints.Count - 1].gameObject, null);
        }
    }
}