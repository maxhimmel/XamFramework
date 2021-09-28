using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility
{
	public class CosWaveEvaluator : MonoBehaviour, IWaveEvaluator
	{
		float IWaveEvaluator.Evaluate( WaveDatum data, float time )
		{
			return data.Amplitude * Mathf.Cos( time * data.Frequency + data.Phase );
		}
	}
}