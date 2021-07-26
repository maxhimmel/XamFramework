using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Extensions
{
	public static class ColliderExtensions
	{
		public static Collider[] OverlapColliders( this Collider collider, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal )
		{
			if ( TryGetCollider( collider, out BoxCollider box ) )
			{
				return OverlapColliders( box, layerMask, queryTriggerInteraction );
			}
			else if ( TryGetCollider( collider, out SphereCollider sphere ) )
			{
				return OverlapColliders( sphere, layerMask, queryTriggerInteraction );
			}
			else if ( TryGetCollider( collider, out CapsuleCollider capsule ) )
			{
				return OverlapColliders( capsule, layerMask, queryTriggerInteraction );
			}
			else
			{
				throw new System.NotImplementedException( "OverlapColliders only supports basic primitive types." );
			}
		}

		private static bool TryGetCollider<T>( Collider collider, out T castedCollider ) where T : Collider
		{
			castedCollider = collider as T;
			if ( castedCollider != null )
			{
				return true;
			}
			return false;
		}

		public static Collider[] OverlapColliders( this BoxCollider collider, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal )
		{
			Vector3 center = collider.bounds.center;
			Quaternion rotation = collider.transform.rotation;

			Vector3 halfExtents = collider.size / 2f;
			Vector3 scaledHalfExtents = VectorExtensions.Multiply( halfExtents, collider.transform.lossyScale );

			return Physics.OverlapBox( center, scaledHalfExtents, rotation, layerMask, queryTriggerInteraction );
		}

		public static Collider[] OverlapColliders( this SphereCollider collider, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal )
		{
			Vector3 center = collider.bounds.center;

			float radius = collider.radius;
			Vector3 scale = collider.transform.lossyScale;
			float scaledRadius = radius * Mathf.Max( scale.x, scale.y, scale.z );

			return Physics.OverlapSphere( center, scaledRadius, layerMask, queryTriggerInteraction );
		}

		public static Collider[] OverlapColliders( this CapsuleCollider collider, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal )
		{
			Vector3 center = collider.bounds.center;
			float radius = GetScaledRadius( collider );
			float height = GetScaledHeight( collider );

			if ( radius >= height / 2f )
			{
				return Physics.OverlapSphere( center, radius, layerMask, queryTriggerInteraction );
			}

			Vector3 localDir = GetDirection( collider.direction );
			Vector3 axisDir = collider.transform.TransformDirection( localDir );
			float offset = height / 2f - radius;

			Vector3 startPos = center - axisDir * offset;
			Vector3 endPos = center + axisDir * offset;

			return Physics.OverlapCapsule( startPos, endPos, radius, layerMask, queryTriggerInteraction );
		}

		private static float GetScaledRadius( CapsuleCollider capsule )
		{
			float radius = capsule.radius;
			Vector3 scale = capsule.transform.lossyScale;

			int direction = capsule.direction;
			switch ( direction )
			{
				default: return 0;
				case 0: return radius * Mathf.Max( scale.y, scale.z );
				case 1: return radius * Mathf.Max( scale.x, scale.z );
				case 2: return radius * Mathf.Max( scale.x, scale.y );
			}
		}

		private static float GetScaledHeight( CapsuleCollider capsule )
		{
			float height = capsule.height;
			Vector3 scale = capsule.transform.lossyScale;

			int direction = capsule.direction;
			switch ( direction )
			{
				default: return 0;
				case 0: return height * scale.x;
				case 1: return height * scale.y;
				case 2: return height * scale.z;
			}
		}

		private static Vector3 GetDirection( int direction )
		{
			switch ( direction )
			{
				default: return Vector3.zero;
				case 0: return Vector3.right;
				case 1: return Vector3.up;
				case 2: return Vector3.forward;
			}
		}
	}
}