using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay
{
	public abstract class GlobalPlacement : ScriptableObject, IPlacement
	{
		void IPlacement.GetNextOrientation( int placementIndex, int totalPlacements, out Vector3 position, out Quaternion rotation )
		{
			position = GetNextPosition( placementIndex, totalPlacements );
			rotation = GetNextRotation( placementIndex, totalPlacements, position );
		}

		protected abstract Vector3 GetNextPosition( int placementIndex, int totalPlacements );
		protected abstract Quaternion GetNextRotation( int placementIndex, int totalPlacements, Vector3 position );
	}
}