using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Xam.Utility.Extensions;

namespace Xam.Gameplay
{
	public class BoxPlacement : BoundsPlacement
	{
		//private float MaxRadiusScale { get { return Mathf.Max( transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z ); } }

		[SerializeField, Min( 0 )] private Vector2 m_minSize = Vector2.zero;
		[SerializeField, Min( 0 )] private Vector2 m_maxSize = Vector2.one;

		protected override Vector3 GetRandomPositionWithinBounds()
		{
			Vector3 randDir = Random.insideUnitCircle.normalized;
			randDir *= m_minSize.magnitude;

			Bounds b = new Bounds( Vector3.zero, m_minSize );
			return b.ClosestPoint( randDir ) + BoundsCenter;


			//Vector3 randPos = RandomPointNearRect( transform.position.x, transform.position.y, m_maxSize.x, m_maxSize.y, 0, 1 );
			//return randPos;

			//Vector3 scale = transform.lossyScale;

			//Vector3 minScale = new Vector3( scale.x * m_minSize.x, scale.y * m_minSize.y );
			//Bounds minBounds = new Bounds( BoundsCenter, minScale );

			//Vector3 maxScale = new Vector3( scale.x * m_maxSize.x, scale.y * m_maxSize.y );
			//Bounds maxBounds = new Bounds( BoundsCenter, maxScale );

			//Vector3 minOffset = new Vector3()
			//{
			//	x = Random.Range( minBounds.min.x, maxBounds.min.x ),
			//	y = Random.Range( minBounds.min.y, maxBounds.min.y ),
			//	z = Random.Range( minBounds.min.z, maxBounds.min.z )
			//};
			//Debug.DrawRay( minOffset, Vector3.left, Color.magenta, 30 );

			//Vector3 maxOffset = new Vector3()
			//{
			//	x = Random.Range( minBounds.max.x, maxBounds.max.x ),
			//	y = Random.Range( minBounds.max.y, maxBounds.max.y ),
			//	z = Random.Range( minBounds.max.z, maxBounds.max.z )
			//};
			//Debug.DrawRay( maxOffset, Vector3.right, Color.cyan, 30 );

			//Vector3 randOffset = new Vector3()
			//{
			//	x = Random.Range( minOffset.x, maxOffset.x ),
			//	y = Random.Range( minOffset.y, maxOffset.y ),
			//	z = Random.Range( minOffset.z, maxOffset.z )
			//};


			////Vector3 randValues = Random.insideUnitCircle;
			////Vector3 randNormalizedValues = new Vector3( Random.value, Random.value, Random.value );

			////Vector3 randOffsetDir = 0.5f * new Vector3()
			////{
			////	x = randValues.x * Mathf.Lerp( minScale.x, maxScale.x, randNormalizedValues.x ),
			////	y = randValues.y * Mathf.Lerp( minScale.y, maxScale.y, randNormalizedValues.y ),
			////	z = randValues.z * Mathf.Lerp( minScale.z, maxScale.z, randNormalizedValues.z ),
			////};

			//return randOffset + BoundsCenter;
		}

#if UNITY_EDITOR
		[Header( "Tools / Editor" )]
		[SerializeField] private Color m_boxColor = new Color( 0.1f, 1, 0, 0.45f );

		private void OnDrawGizmos()
		{
			Gizmos.color = m_boxColor;

			Vector3 scale = transform.lossyScale;
			Vector3 minScale = new Vector3( scale.x * m_minSize.x, scale.y * m_minSize.y );
			Vector3 maxScale = new Vector3( scale.x * m_maxSize.x, scale.y * m_maxSize.y );

			Gizmos.DrawWireCube( BoundsCenter, minScale );
			Gizmos.DrawWireCube( BoundsCenter, maxScale );
		}

		protected override void OnValidate()
		{
			base.OnValidate();

			ClampMinSize();
		}

		private void ClampMinSize()
		{
			m_minSize = Vector3.Min( m_minSize, m_maxSize );
		}
#endif
	}
}