using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Extensions
{
	public static class ColorExtensions
	{
		public static Color NewAlpha( this Color color, float a )
		{
			Color result = color;
			result.a = a;

			return result;
		}
	}
}