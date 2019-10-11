using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Xam.Editor
{
	public static class XamEditorUtilities
	{
		/// <summary>
		/// Loads first asset found with search parameter: "t: [typeof(T).name]"
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="path"></param>
		/// <returns></returns>
		public static T LoadAsset<T>() where T : Object
		{
			string[] assetGuids = AssetDatabase.FindAssets( $@"t:{typeof( T ).Name}" );
			if ( assetGuids == null || assetGuids.Length <= 0 ) { return null; }

			return LoadFirstAsset<T>( assetGuids );
		}

		public static T LoadAssetReferencing<T>() where T : Object
		{
			string[] assetGuids = AssetDatabase.FindAssets( $@"ref:Scripts/{typeof( T ).Name}.cs" );
			if ( assetGuids == null || assetGuids.Length <= 0 ) { return null; }

			return LoadFirstAsset<T>( assetGuids );
		}

		private static T LoadFirstAsset<T>( params string[] assetGuids ) where T : Object
		{
			foreach ( string guid in assetGuids )
			{
				string assetPath = AssetDatabase.GUIDToAssetPath( guid );
				T asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );

				if ( asset != null ) { return asset; }
			}

			return null;
		}
	}
}