using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Xam.Gameplay
{
	[RequireComponent( typeof( CinemachinePathBase ) )]
	public class PathPlacement : InstancedPlacement
	{
		private CinemachinePathBase m_path = null;

		public override Vector3 GetNextPosition( int placementIndex, int totalPlacements, Space space )
		{
			float pathPos = placementIndex / (float)(totalPlacements - 1);
			Vector3 worldPos = m_path.EvaluatePositionAtUnit( pathPos, CinemachinePathBase.PositionUnits.Normalized );

			if ( space == Space.World )
			{
				return worldPos;
			}
			else
			{
				return m_path.transform.InverseTransformPoint( worldPos );
			}
		}

		public override Quaternion GetNextRotation( int placementIndex, int totalPlacements, Vector3 position )
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