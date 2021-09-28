using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility
{
	public class CosWaveEvaluator : MonoBehaviour, IWaveEvaluator
	{
		public float Evaluate( WaveDatum data, float time )
		{
			return data.Amplitude * Mathf.Cos( time * data.Frequency + data.Phase );
		}
	}
}