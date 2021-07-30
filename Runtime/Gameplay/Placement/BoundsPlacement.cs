using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay
{
	public abstract class BoundsPlacement : InstancedPlacement
	{
		protected Vector3 BoundsCenter { get { return transform.position; } }

		protected override Vector3 GetNextPosition( int placementIndex, int totalPlacements )
		{
			return GetRandomPositionWithinBounds();
		}

		protected abstract Vector3 GetRandomPositionWithinBounds();

		protected override Quaternion GetNextRotation( int placementIndex, int totalPlacements, Vector3 position )
		{
			Vector3 posToCenter = (BoundsCenter - position);
			return m_lookRotation.GetLookRotation( posToCenter, transform.up );
		}
	}
}