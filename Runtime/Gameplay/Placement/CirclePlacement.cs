using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay
{
	using Utility.Extensions;

	public class CirclePlacement : BoundsPlacement
	{
		private float MaxRadiusScale { get { return Mathf.Max( transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z ); } }

		[SerializeField, Min( 0 )] private float m_minRadius = 0;
		[SerializeField, Min( 0 )] private float m_maxRadius = 0.5f;

		protected override Vector3 GetRandomPositionWithinBounds( Space space )
		{
			Vector3 minScale = transform.lossyScale * m_minRadius;
			Vector3 maxScale = transform.lossyScale * m_maxRadius;

			Vector3 randNormalizedDir = Random.onUnitSphere;
			randNormalizedDir = VectorExtensions.Multiply( randNormalizedDir, minScale ).normalized;

			Vector3 randNormalizedValues = new Vector3( Random.value, Random.value, Random.value );

			Vector3 randOffsetDir = new Vector3()
			{
				x = randNormalizedDir.x * Mathf.Lerp( minScale.x, maxScale.x, randNormalizedValues.x ),
				y = randNormalizedDir.y * Mathf.Lerp( minScale.y, maxScale.y, randNormalizedValues.y ),
				z = randNormalizedDir.z * Mathf.Lerp( minScale.z, maxScale.z, randNormalizedValues.z ),
			};

			if ( space == Space.World )
			{
				return randOffsetDir + BoundsCenter;
			}
			else
			{
				return randOffsetDir;
			}
		}


#if UNITY_EDITOR
		[Header( "Tools / Editor" )]
		[SerializeField] private Color m_radiusColor = new Color( 0.1f, 1, 0, 0.45f );

		private void OnDrawGizmos()
		{
			Gizmos.color = m_radiusColor;
			Gizmos.DrawWireSphere( BoundsCenter, m_minRadius * MaxRadiusScale );
			Gizmos.DrawWireSphere( BoundsCenter, m_maxRadius * MaxRadiusScale );
		}

		protected override void OnValidate()
		{
			base.OnValidate();

			ClampMinRadius();
		}

		private void ClampMinRadius()
		{
			m_minRadius = Mathf.Min( m_minRadius, m_maxRadius );
		}
#endif
	}
}