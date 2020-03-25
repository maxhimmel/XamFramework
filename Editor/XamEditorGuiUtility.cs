using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Editor
{
	public static class XamEditorGuiUtility
	{
		public static Texture2D CreateTexture( int width, int height, Color color )
		{
			Color[] pixels = new Color[width * height];

			for ( int i = 0; i < pixels.Length; i++ )
			{
				pixels[i] = color;
			}

			Texture2D result = new Texture2D( width, height );
			result.SetPixels( pixels );
			result.Apply();

			return result;
		}
	}
}