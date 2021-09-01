using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Randomization
{
	[System.Serializable]
	public class FloatWeightedNode : WeightedNode<float> { }

	[System.Serializable]
	public class FloatWeightedList : WeightedList<FloatWeightedNode, float> { }
}