using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay
{
	using Xam.Utility;

	public abstract class InstancedPlacement : MonoBehaviour, IPlacement
	{
		protected ILookRotation m_lookRotation = null;

		void IPlacement.GetNextOrientation( int placementIndex, int totalPlacements, out Vector3 position, out Quaternion rotation )
		{
			position = GetNextPosition( placementIndex, totalPlacements );
			rotation = GetNextRotation( placementIndex, totalPlacements, position );
		}

		protected abstract Vector3 GetNextPosition( int placementIndex, int totalPlacements );
		protected abstract Quaternion GetNextRotation( int placementIndex, int totalPlacements, Vector3 position );

		protected virtual void Awake()
		{
			m_lookRotation = GetComponentInChildren<ILookRotation>();
		}

		protected virtual void OnValidate()
		{

		}
	}
}