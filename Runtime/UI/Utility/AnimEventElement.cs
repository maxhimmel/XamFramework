using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Xam.Ui
{
	[RequireComponent( typeof ( EventTrigger ) )]
	public abstract class AnimEventElement : MonoBehaviour
	{
		protected bool IsAnimating { get; private set; }
		protected float DeltaTime { get { return m_useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime; } }

		[Header( "Animation Event Triggers" )]
		[SerializeField] private EventTriggerType m_enabledEvent = EventTriggerType.PointerEnter;
		[SerializeField] private EventTriggerType m_disabledEvent = EventTriggerType.PointerExit;

		[Space]
		[SerializeField] private bool m_useUnscaledTime = true;

		private EventTrigger m_eventTrigger = null;

		protected virtual void Awake()
		{
			m_eventTrigger = GetComponent<EventTrigger>();
			
			if ( !TryGetEvent( m_eventTrigger, m_enabledEvent, out EventTrigger.Entry enabledEntry ) )
			{
				m_eventTrigger.triggers.Add( enabledEntry );
			}
			enabledEntry.callback.AddListener( OnElementEnabled );

			if ( !TryGetEvent( m_eventTrigger, m_disabledEvent, out EventTrigger.Entry disabledEntry ) )
			{
				m_eventTrigger.triggers.Add( disabledEntry );
			}
			disabledEntry.callback.AddListener( OnElementDisabled );
		}

		private bool TryGetEvent( EventTrigger eventTrigger, EventTriggerType eventType, out EventTrigger.Entry entry )
		{
			entry = eventTrigger.triggers.Find( queryEntry => queryEntry.eventID == eventType );
			if ( entry != null ) { return true; }
			
			entry = new EventTrigger.Entry();
			entry.eventID = eventType;
			return false;
		}

		protected virtual void OnElementEnabled( BaseEventData data )
		{
			IsAnimating = true;
		}

		protected virtual void OnElementDisabled( BaseEventData data )
		{
			IsAnimating = false;
		}
	}
}