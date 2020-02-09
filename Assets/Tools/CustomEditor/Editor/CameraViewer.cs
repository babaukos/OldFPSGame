// ver. 0.1
//
// Mikle Khmelevsky

using UnityEngine;
using UnityEditor;
using System.Collections;
 
public class CameraViewer : EditorWindow
{
	bool liveUpdate = false;
	Camera camera;
	RenderTexture renderTexture, originalTarget;
 

	[ MenuItem( "Tools/CameraViewer" ) ]
	static void Launch()
	{
        EditorWindow editorWindow = EditorWindow.GetWindow(typeof(CameraViewer));
        editorWindow.title = "CameraViewer";
 
		editorWindow.Show();
	}
 
	void Update()
	{
		if( camera != null )
		{
			camera.Render();
			if( liveUpdate )
			{
				Repaint();
			}
		}
	}
 
	void OnSelectionChange()
	{
		Camera newCamera = ( Selection.activeTransform == null ) ? null : Selection.activeTransform.gameObject.GetComponent<Camera>();
 
		if( newCamera != camera )
		{
			if( originalTarget != null )
			{
				camera.targetTexture = originalTarget;
			}
 
			camera = newCamera;
			if( camera != null )
			{
				originalTarget = camera.targetTexture;
				camera.targetTexture = renderTexture;
			}
			else
			{
				originalTarget = null;
			}
		}
	}
 
	void OnGUI()
	{
		if( camera == null )
		{
			ToolbarGUI( "No camera selection" );
			return;	
		}
 
		if( renderTexture == null || renderTexture.width != position.width || renderTexture.height != position.height )
		{
			renderTexture = new RenderTexture( ( int )position.width, ( int )position.height, ( int )RenderTextureFormat.ARGB32 );
			camera.targetTexture = renderTexture;
		}
 
		GUI.DrawTexture( new Rect( 0.0f, 0.0f, position.width, position.height ), renderTexture );
 
		ToolbarGUI( camera.gameObject.name );
	}
 
	void ToolbarGUI( string title )
	{
		GUILayout.BeginHorizontal( "Toolbar" );
			GUILayout.Label( title );
			GUILayout.FlexibleSpace();
			liveUpdate = GUILayout.Toggle( liveUpdate, "Live update", "ToolbarButton" );
		GUILayout.EndHorizontal();
	}
}