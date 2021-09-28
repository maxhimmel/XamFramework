using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility
{
	[System.Serializable]
	public struct WaveDatum
	{
		public float Amplitude;
		public float Frequency;
		public float Phase;

		public WaveDatum( float amplitude, float frequency, float phase )
		{
			Amplitude = amplitude;
			Frequency = frequency;
			Phase = phase;
		}
	}
}