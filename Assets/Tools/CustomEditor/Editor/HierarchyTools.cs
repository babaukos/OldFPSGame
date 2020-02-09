using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public static class HierarchyTools 
{
    [MenuItem("Tools/Hierarchy/Collapse Hierarchy %c")] // % (ctrl on Windows, cmd on macOS), # (shift), & (alt).
    public static void CollapseHierarchy() 
    {
        EditorApplication.ExecuteMenuItem("Window/Hierarchy");
        var hierarchyWindow = EditorWindow.focusedWindow;
        var expandMethodInfo = hierarchyWindow.GetType().GetMethod("SetExpandedRecursive");
        foreach (GameObject root in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            expandMethodInfo.Invoke(hierarchyWindow, new object[] { root.GetInstanceID(), false });
        }
    }
    [MenuItem("Tools/Hierarchy/Expand Hierarchy %e")]   // % (ctrl on Windows, cmd on macOS), # (shift), & (alt).
    public static void ExpandHierarchy()
    {
        EditorApplication.ExecuteMenuItem("Window/Hierarchy");
        var hierarchyWindow = EditorWindow.focusedWindow;
        var expandMethodInfo = hierarchyWindow.GetType().GetMethod("SetExpandedRecursive");
        foreach (GameObject root in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            expandMethodInfo.Invoke(hierarchyWindow, new object[] { root.GetInstanceID(), true });
        }
    }
}
     
