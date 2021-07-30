﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Xam.Gameplay
{
	[RequireComponent( typeof( CinemachinePathBase ) )]
	public class PathPlacement : InstancedPlacement
	{
		private CinemachinePathBase m_path = null;

		protected override Vector3 GetNextPosition( int placementIndex, int totalPlacements )
		{
			float pathPos = placementIndex / (float)totalPlacements;
			return m_path.EvaluatePositionAtUnit( pathPos, CinemachinePathBase.PositionUnits.Normalized );
		}

		protected override Quaternion GetNextRotation( int placementIndex, int totalPlacements, Vector3 position )
		{
			float pathPos = placementIndex / (float)totalPlacements;
			Quaternion pathRotation = m_path.EvaluateOrientationAtUnit( pathPos, CinemachinePathBase.PositionUnits.Normalized );

			if ( m_lookRotation != null )
			{
				pathRotation = m_lookRotation.GetLookRotation( pathRotation * Vector3.forward, pathRotation * Vector3.up );
			}

			return pathRotation;
		}

		protected override void Awake()
		{
			base.Awake();

			m_path = GetComponentInChildren<CinemachinePathBase>();
		}
	}
}