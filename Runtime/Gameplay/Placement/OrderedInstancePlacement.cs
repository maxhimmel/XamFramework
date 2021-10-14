using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay
{
	using Utility;

	public class OrderedInstancePlacement : InstancedPlacement
	{
		[SerializeField] private bool m_randomizeOnAwake = false;
		[SerializeField] private InstancedPlacement[] m_placements = new InstancedPlacement[0];

		public override Vector3 GetNextPosition( int placementIndex, int totalPlacements, Space space )
		{
			if ( TryGetPlacement( placementIndex, out var placement ) )
			{
				return placement.GetNextPosition( placementIndex, totalPlacements, space );
			}

			return Vector3.zero;
		}

		public override Quaternion GetNextRotation( int placementIndex, int totalPlacements, Vector3 position )
		{
			if ( TryGetPlacement( placementIndex, out var placement ) )
			{
				return placement.GetNextRotation( placementIndex, totalPlacements, position );
			}

			return Quaternion.identity;
		}

		private bool TryGetPlacement( int index, out InstancedPlacement placement )
		{
			if ( index < 0 || m_placements.Length <= 0 )
			{
				placement = null;
				return false;
			}

			index %= m_placements.Length;
			placement = m_placements[index];

			return placement != null;
		}

		protected override void Awake()
		{
			base.Awake();

			if ( m_randomizeOnAwake )
			{
				AlgorithmUtility.FisherYatesShuffle( m_placements );
			}
		}
	}
}