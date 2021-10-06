using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xam.Utility.Randomization;

namespace Xam.Gameplay
{
	public class WeightedInstancePlacement : InstancedPlacement
	{
		[Tooltip( "If true, separate placements can be used to determine positions and rotations for the same object." )]
		[SerializeField] private bool m_canMismatchOrientations = false;
		[SerializeField] private WeightedInstancePlacementList m_placements = new WeightedInstancePlacementList();

		private int m_currentPlacementIndex = -1;
		private InstancedPlacement m_currentPlacement;
		private Vector3 m_currentPosition;

		public override Vector3 GetNextPosition( int placementIndex, int totalPlacements, Space space )
		{
			InstancedPlacement placement = TryGetValidPlacement( placementIndex );
			if ( placement == null ) { return Vector3.zero; }

			m_currentPosition = placement.GetNextPosition( placementIndex, totalPlacements, space );
			return m_currentPosition;
		}

		public override Quaternion GetNextRotation( int placementIndex, int totalPlacements, Vector3 position )
		{
			InstancedPlacement placement = TryGetValidPlacement( placementIndex );
			if ( placement == null ) { return Quaternion.identity; }

			return placement.GetNextRotation( placementIndex, totalPlacements, m_currentPosition );
		}

		private InstancedPlacement TryGetValidPlacement( int placementIndex )
		{
			if ( m_canMismatchOrientations || m_currentPlacementIndex != placementIndex )
			{
				m_currentPlacementIndex = placementIndex;
				m_currentPlacement = m_placements.GetRandomItem();
			}

			return m_currentPlacement;
		}

		protected override void Awake()
		{
			base.Awake();

			m_placements.Init();
		}
	}

	[System.Serializable]
	public class WeightedInstancePlacementList : WeightedList<WeightedInstancePlacementNode, InstancedPlacement>
	{
	}

	[System.Serializable]
	public class WeightedInstancePlacementNode : WeightedNode<InstancedPlacement>
	{
	}
}