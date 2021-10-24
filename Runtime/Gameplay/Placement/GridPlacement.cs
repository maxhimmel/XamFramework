using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay
{
	public class GridPlacement : InstancedPlacement
	{
		public int CellCount => m_stepCount.x * m_stepCount.y;

		[Header( "Grid" )]
		[SerializeField] private Vector2 m_bounds = Vector2.one * 10;
		[SerializeField] private Vector2Int m_stepCount = Vector2Int.one * 10;

		public override Vector3 GetNextPosition( int placementIndex, int totalPlacements, Space space )
		{
			return ToPosition( placementIndex );
		}

		public override Quaternion GetNextRotation( int placementIndex, int totalPlacements, Vector3 position )
		{
			return m_lookRotation.GetLookRotation( transform.forward, transform.up );
		}

		private Vector3 ToPosition( int index )
		{
			Vector2Int rowColumn = ToRowColumn( index ); 			
			float x = ((float)rowColumn.x / (m_stepCount.x - 1)) * m_bounds.x;
			float y = ((float)rowColumn.y / (m_stepCount.y - 1)) * m_bounds.y;

			Vector3 position = new Vector3( x, y, 0 );
			Vector3 offset = GetNormalBounds() / 2f;

			position -= offset;
			position = transform.TransformPoint( position );

			return position;
		}

		private Vector2Int ToRowColumn( int index )
		{
			index %= CellCount;

			int columnCount = m_stepCount.y;

			int row = Mathf.FloorToInt( index / columnCount );
			int col = index % columnCount;

			return new Vector2Int( row, col );
		}

		private Vector3 GetNormalBounds()
		{
			//Quaternion rotation = Quaternion.AngleAxis( 90, Vector3.right );
			return m_bounds;
		}


#if UNITY_EDITOR
		[Header( "Editor/Tools" )]
		[SerializeField] private Color m_gizmoColor = new Color( 0.91f, 0.64f, 0 );

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = m_gizmoColor;
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawWireCube( Vector3.zero, GetNormalBounds() );

			Color posColor = m_gizmoColor;
			posColor.a *= 0.5f;
			Gizmos.color = posColor;
			Gizmos.matrix = Matrix4x4.identity;

			for ( int idx = 0; idx < CellCount; ++idx )
			{
				Vector3 position = ToPosition( idx );
				Gizmos.DrawSphere( position, 0.25f );
			}
		}
#endif
	}
}