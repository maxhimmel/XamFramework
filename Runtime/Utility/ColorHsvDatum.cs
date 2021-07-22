using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility
{
	[System.Serializable]
	public class ColorHsvDatum
	{
		[Range( 0, 1 )] public float HueMin = 0;
		[Range( 0, 1 )] public float HueMax = 1;

		[Space]
		[Range( 0, 1 )] public float SaturationMin = 0;
		[Range( 0, 1 )] public float SaturationMax = 1;

		[Space]
		[Range( 0, 1 )] public float ValueMin = 0;
		[Range( 0, 1 )] public float ValueMax = 1;

		[Space]
		[Range( 0, 1 )] public float AlphaMin = 1;
		[Range( 0, 1 )] public float AlphaMax = 1;

		public ColorHsvDatum( 
			float hueMin = 0, float hueMax = 1, 
			float saturationMin = 0, float saturationMax = 1, 
			float valueMin = 0, float valueMax = 1, 
			float alphaMin = 0, float alphaMax = 1 )
		{
			HueMin = hueMin;
			HueMax = hueMax;
			
			SaturationMin = saturationMin;
			SaturationMax = saturationMax;
			
			ValueMin = valueMin;
			ValueMax = valueMax;
			
			AlphaMin = alphaMin;
			AlphaMax = alphaMax;
		}

		public Color GetRandom()
		{
			return Random.ColorHSV( 
				HueMin, HueMax, 
				SaturationMin, SaturationMax, 
				ValueMin, ValueMax, 
				AlphaMin, AlphaMax 
			);
		}
	}
}