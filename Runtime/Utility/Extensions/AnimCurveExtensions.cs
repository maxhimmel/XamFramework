using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Extensions
{
	public static class AnimCurveExtensions
	{
		public static float GetDuration( this AnimationCurve curve )
		{
			if ( curve.length <= 0 ) { return 0; }

			Keyframe key = curve.keys[curve.length - 1];
			return key.time;
		}
	}
}