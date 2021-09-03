using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay
{
	[RequireComponent( typeof( Collider ) )]
	public class TriggerExitRelay : MonoBehaviour
	{
		public event System.EventHandler<Collider> OnTriggerExitHandler;

		public Collider Collider { get; private set; }

		private void OnTriggerExit( Collider other )
		{
			OnTriggerExitHandler?.Invoke( this, other );
		}

		private void Awake()
		{
			Collider = GetComponent<Collider>();
		}
	}
}