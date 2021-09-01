using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Randomization
{
	[System.Serializable]
	public class IntWeightedNode : WeightedNode<int> { }

	[System.Serializable]
	public class IntWeightedList : WeightedList<IntWeightedNode, int> { }
}