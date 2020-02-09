using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;


[InitializeOnLoad]
public class CustomHierarchy
{
    static string iconPath = "Assets/Tools/CustomEditor/Icon/";

    static Texture2D lockOn, lockOff;
    static Texture2D visible, invisible;
    static Texture2D icon, iconDefault;

    static int iconWithHight = 16;
      

    // Сonstructor
    static CustomHierarchy()
    {
        lockOn = AssetDatabase.LoadAssetAtPath(iconPath + "icon_lock.png", typeof(Texture2D)) as Texture2D;
        lockOff = AssetDatabase.LoadAssetAtPath(iconPath + "icon_unlock.png", typeof(Texture2D)) as Texture2D;

        visible = AssetDatabase.LoadAssetAtPath(iconPath + "icon_view.png", typeof(Texture2D)) as Texture2D;
        invisible = AssetDatabase.LoadAssetAtPath(iconPath + "icon_invise.png", typeof(Texture2D)) as Texture2D;

        iconDefault = AssetDatabase.LoadAssetAtPath(iconPath + "icon_icon.png", typeof(Texture2D)) as Texture2D;

        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    }

    // Metod Draw
    private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        GUIStyle styleV = new GUIStyle();
        GUIStyle styleL = new GUIStyle();
        GameObject obj = (GameObject)EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        Rect rectV = new Rect(selectionRect);
        Rect rectL = new Rect(selectionRect);
        Rect rectI = new Rect(selectionRect);

        if (obj != null)
        {
            // Change interactable object
            rectL.x = rectL.xMax - 35;
            rectL.width = iconWithHight;

            if (ObjectIsLocked(obj))
            {
                styleL.normal.background = lockOff;
                if (GUI.Button(rectL, new GUIContent("", "[unlock] object"), styleL))
                    UnlockObject(obj, true);
            }
            else
            {
                styleL.normal.background = lockOn;
                if (GUI.Button(rectL, new GUIContent("", "[lock] object"), styleL))
                    LockObject(obj, true);
            }

            // Change visibility object
            rectV.x = rectV.xMax - 18;
            rectV.width = iconWithHight;

            if (ObjectIsActive(obj))
            {
                styleV.normal.background = visible;
                if (GUI.Button(rectV, new GUIContent("", "[hide] object"), styleV))
                    DeActiveObject(obj, false);
            }
            else 
            {
                styleV.normal.background = invisible;
                if (GUI.Button(rectV, new GUIContent("", "[swow] object"), styleV))
                    ActiveObject(obj, false);
            }

            // Object icon
            rectI.x = rectI.xMin - 29;
            rectI.width = iconWithHight;

            // Texture2D texture2D = EditorGUIUtility.GetIconForObject(obj);
            // Texture texture2D = EditorGUIUtility.ObjectContent(null, typeof(Transform).image);
            // Texture2D texture2D = AssetPreview.GetMiniTypeThumbnail(typeof(GameObject));

            if(AssetPreview.GetMiniThumbnail(obj) != null)
            {
                icon = AssetPreview.GetMiniThumbnail(obj);
            }
            else
            {
                icon = iconDefault;
            }
            GUI.DrawTexture(rectI, icon, ScaleMode.ScaleToFit);
       }
    }
    //
    private static void DrawIcon(string texName, Rect rect)
    {
        Texture2D text = AssetDatabase.LoadAssetAtPath(iconPath + texName, typeof(Texture2D)) as Texture2D;
        Rect r = new Rect(rect.x + rect.width - 16f, rect.y, 16f, 16f);
        GUI.DrawTexture(r, text);
    }
    static bool ObjectIsLocked(GameObject testObject)
    {
        if (testObject == null)
            return false;
        return ((int)testObject.hideFlags & (int)HideFlags.NotEditable) != 0;
    }
    private static void LockObject(GameObject targetObject, bool recursive)
    {
        targetObject.hideFlags = targetObject.hideFlags | HideFlags.NotEditable;
        if (recursive)
        {
            foreach (Transform child in targetObject.transform)
                LockObject(child.gameObject, true);
        }
    }
    private static void UnlockObject(GameObject targetObject, bool recursive)
    {
        targetObject.hideFlags = targetObject.hideFlags & ~HideFlags.NotEditable;
        if (recursive)
        {
            foreach (Transform child in targetObject.transform)
                UnlockObject(child.gameObject, true);
        }
    }
    static bool ObjectIsActive(GameObject testObject)
    {
        if (testObject.active == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private static void ActiveObject(GameObject targetObject, bool recursive)
    {
        targetObject.active = true;
        if (recursive)
        {
            foreach (Transform child in targetObject.transform)
                ActiveObject(child.gameObject, true);
        }
    }
    private static void DeActiveObject(GameObject targetObject, bool recursive)
    {
        targetObject.active = false;
        if (recursive)
        {
            foreach (Transform child in targetObject.transform)
                DeActiveObject(child.gameObject, true);
        }
    }
    //
    private static GameObject[] selectAll()
    {
        return Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
    }
    //
    private static GameObject[] selectActive()
    {
        return Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
    }
}