using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay
{
	public interface IPlacement
	{
		Vector3 Center { get; }

		void GetNextOrientation( int placementIndex, int totalPlacements, out Vector3 position, out Quaternion rotation, Space space = Space.World );
	}
}