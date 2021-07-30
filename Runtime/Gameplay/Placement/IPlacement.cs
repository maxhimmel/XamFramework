using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay
{
	public interface IPlacement
	{
		void GetNextOrientation( int placementIndex, int totalPlacements, out Vector3 position, out Quaternion rotation );
	}
}