using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility
{
	public interface IWaveEvaluator
	{
		float Evaluate( WaveDatum data, float time );
	}
}