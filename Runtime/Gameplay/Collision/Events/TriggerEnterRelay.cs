using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay
{
	[RequireComponent( typeof( Collider ) )]
	public class TriggerEnterRelay : MonoBehaviour
	{
		public event System.EventHandler<Collider> OnTriggerEnterHandler;

		public Collider Collider { get; private set; }

		private void OnTriggerEnter( Collider other )
		{
			OnTriggerEnterHandler?.Invoke( this, other );
		}

		private void Awake()
		{
			Collider = GetComponent<Collider>();
		}
	}
}