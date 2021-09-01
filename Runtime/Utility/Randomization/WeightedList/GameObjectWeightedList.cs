using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Randomization
{
	[System.Serializable]
	public class GameObjectWeightedNode : WeightedNode<GameObject> { }

	[System.Serializable]
	public class GameObjectWeightedList : WeightedList<GameObjectWeightedNode, GameObject> { }
}