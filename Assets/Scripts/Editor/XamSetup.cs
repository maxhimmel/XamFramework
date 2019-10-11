using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Xam.Editor
{
	public class XamSetup : EditorWindow
	{
		[MenuItem( "Xam/Setup/Create GameManager Prefab" )]
		private static void CreateGameManagerPrefab()
		{
			string savePath = GetNewFileSavePath( "Save GameManager Prefab", "GameManager", "prefab" );
			if ( string.IsNullOrEmpty( savePath ) ) { return; }

			string prefabName = System.IO.Path.GetFileNameWithoutExtension( savePath );

			GameManager gameManager = XamFactory.CreateGameManager( prefabName );
			GameObject gameManagerObj = gameManager.gameObject;

			SaveAndCleanup( ref gameManagerObj, savePath );
		}

		private static string GetNewFileSavePath( string panelTitle, string filename, string extension )
		{
			string message = $"Create a new {filename} prefab.";
			return EditorUtility.SaveFilePanelInProject( panelTitle, filename, extension, message );
		}

		private static bool SaveAndCleanup( ref GameObject prefab, string savePath )
		{
			PrefabUtility.SaveAsPrefabAsset( prefab, savePath, out bool success );
			DestroyImmediate( prefab );

			return success;
		}
	}
}