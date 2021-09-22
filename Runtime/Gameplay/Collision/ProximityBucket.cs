using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay
{
	using Xam.Utility.Extensions;

	/// <summary>
	/// Gathers colliders whose layer matches the <see cref="m_gatherMask"/>.
	/// </summary>
	public class ProximityBucket<T> : MonoBehaviour
		where T : Component
	{
		public IEnumerable<T> Targets { get { return m_targets; } }
		public bool HasAvailableTargets { get { return m_targets.Count > 0; } }
		public int Count { get { return m_targets.Count; } }

		public event System.EventHandler<T> OnTargetEnterEvent;
		public event System.EventHandler<T> OnTargetExitEvent;

		[SerializeField] private LayerMask m_gatherMask = -1;
		[SerializeField] private QueryTriggerInteraction m_queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;

		[Header( "Trigger Relays" )]
		[SerializeField] private TriggerEnterRelay m_enterRelay = default;
		[SerializeField] private TriggerExitRelay m_exitRelay = default;

		private List<T> m_targets = new List<T>();
		private static Collider[] m_forceCheckedColliders = new Collider[0];

		public void ClearBucket()
		{
			m_targets.Clear();
		}

		public void CheckProximityNonAlloc( int allocationSize )
		{
			if ( m_forceCheckedColliders.Length < allocationSize )
			{
				m_forceCheckedColliders = new Collider[allocationSize];
			}

			int numOverlaps = m_enterRelay.Collider.OverlapCollidersNonAlloc( m_forceCheckedColliders, m_gatherMask, m_queryTriggerInteraction );
			for ( int idx = 0; idx < numOverlaps; ++idx )
			{
				Collider overlap = m_forceCheckedColliders[idx];
				OnTriggerEntered( m_enterRelay, overlap );
			}
		}

		private void OnTriggerEntered( object sender, Collider other )
		{
			if ( CanEnterBucket( other ) )
			{
				T target = other.attachedRigidbody?.GetComponentInParent<T>();
				if ( IsInsideBucket( target ) ) { return; }

				AddTarget( target );
				OnTargetEnterEvent?.Invoke( this, target );
			}
		}

		private void OnTriggerExited( object sender, Collider other )
		{
			if ( CanEnterBucket( other ) )
			{
				T target = other.attachedRigidbody?.GetComponentInParent<T>();
				if ( !IsInsideBucket( target ) ) { return; }

				RemoveTarget( target );
				OnTargetExitEvent?.Invoke( this, target );
			}
		}

		private bool CanEnterBucket( Collider collider )
		{
			int otherLayer = 1 << collider.gameObject.layer;
			return (otherLayer & m_gatherMask) != 0;
		}

		private bool IsInsideBucket( T other )
		{
			return m_targets.Contains( other );
		}

		private void AddTarget( T target )
		{
			m_targets.Add( target );
		}

		private void RemoveTarget( T target )
		{
			m_targets.Remove( target );
		}

		protected virtual void Awake()
		{
			m_enterRelay.OnTriggerEnterHandler += OnTriggerEntered;
			m_exitRelay.OnTriggerExitHandler += OnTriggerExited;
		}
	}
}