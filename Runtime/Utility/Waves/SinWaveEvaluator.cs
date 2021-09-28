using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility
{
	public class SinWaveEvaluator : MonoBehaviour, IWaveEvaluator
	{
		float IWaveEvaluator.Evaluate( WaveDatum data, float time )
		{
			return data.Amplitude * Mathf.Sin( time * data.Frequency + data.Phase );
		}
	}
}