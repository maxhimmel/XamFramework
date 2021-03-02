using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditorInternal;

using UEditor = UnityEditor.Editor;

namespace Xam.Editor.Rewired
{
	public class RewiredAssemDefSetup : UEditor
	{
		private const string k_integrationEditorDirectory = "Integration/UnityUI/Editor";
		private const string k_internalEditorDirectory = "Internal/Scripts/Editor";

		[MenuItem( "Xam/Setup/Rewired Assembly Def. Config" )]
		private static void ConfigureRewiredAssemblyDef()
		{
			if ( !TryGetRewiredDirectory( out string rootRewiredDirectory ) ) { return; }

			string rewiredEditorDirectory = $"{rootRewiredDirectory}/Editor";
			Directory.CreateDirectory( rewiredEditorDirectory );

			string integrationEditorDirectory = $"{rootRewiredDirectory}/{k_integrationEditorDirectory}";
			MoveContents( integrationEditorDirectory, rewiredEditorDirectory );

			string internalEditorDirectory = $"{rootRewiredDirectory}/{k_internalEditorDirectory}";
			MoveContents( internalEditorDirectory, rewiredEditorDirectory );

			AssetDatabase.Refresh( ImportAssetOptions.ForceUpdate );
		}

		private static bool TryGetRewiredDirectory( out string rootRewiredDirectory )
		{
			string assetsPath = Application.dataPath;

			string[] rewiredDirectories = Directory.GetDirectories( assetsPath, "Rewired", SearchOption.AllDirectories );
			Debug.Assert( rewiredDirectories != null && rewiredDirectories.Length > 0, "Cannot find 'Rewired' directory." );
			
			foreach ( string directory in rewiredDirectories )
			{
				if ( !directory.Contains( "Editor" ) )
				{
					rootRewiredDirectory = directory;
					return true;
				}
			}

			rootRewiredDirectory = string.Empty;
			return false;
		}

		private static void MoveContents( string sourceDirectory, string destDirectory )
		{
			string[] localDirectories = Directory.GetDirectories( sourceDirectory );
			if ( localDirectories != null && localDirectories.Length > 0 )
			{
				foreach ( string directory in localDirectories )
				{
					DirectoryInfo dirInfo = new DirectoryInfo( directory );
					string subDirectoryDest = $"{destDirectory}/{dirInfo.Name}";

					Directory.Move( directory, subDirectoryDest );
				}
			}

			string[] localFilePaths = Directory.GetFiles( sourceDirectory );
			if ( localFilePaths != null && localFilePaths.Length > 0 )
			{
				foreach ( string filePath in localFilePaths )
				{
					string fileName = Path.GetFileName( filePath );
					string destFilePath = $"{destDirectory}/{fileName}";

					File.Move( filePath, destFilePath );
				}
			}

			CleanupDirectory( sourceDirectory );
		}

		private static void CleanupDirectory( string directory )
		{
			if ( Directory.Exists( directory ) )
			{
				Directory.Delete( directory );
			}

			string metaFilePath = $"{directory}.meta";
			if ( File.Exists( metaFilePath ) )
			{
				File.Delete( metaFilePath );
			}
		}
	}
}