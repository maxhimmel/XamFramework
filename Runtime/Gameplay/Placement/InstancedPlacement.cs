﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay
{
	using Xam.Utility;

	public abstract class InstancedPlacement : MonoBehaviour, IPlacement
	{
		public virtual Vector3 Center => transform.position;

		protected ILookRotation m_lookRotation = null;

		void IPlacement.GetNextOrientation( int placementIndex, int totalPlacements, out Vector3 position, out Quaternion rotation, Space space )
		{
			position = GetNextPosition( placementIndex, totalPlacements, space );
			rotation = GetNextRotation( placementIndex, totalPlacements, position );
		}

		protected abstract Vector3 GetNextPosition( int placementIndex, int totalPlacements, Space space );
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