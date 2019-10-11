using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Xam.Utility
{
	public class FadeTransition : Patterns.SingletonMono<FadeTransition>
	{
		private Image FadeElement
		{
			get
			{
				if ( m_fadeElement == null )
				{
					m_fadeElement = GetComponentInChildren<Image>();
				}
				return m_fadeElement;
			}
		}

		[Header( "Fade Transition" )]
		[SerializeField] private bool m_startBlackedOut = false;

		private Image m_fadeElement = null;
		private Coroutine m_fadeRoutine = null;

		public void FadeOut( float timespan )
		{
			CrossFade( 1, timespan );
		}

		public void FadeIn( float timespan )
		{
			CrossFade( 0, timespan );
		}

		public void CrossFade( float targetAlpha, float timespan )
		{
			if ( m_fadeElement == null )
			{
				Debug.LogWarning( $"Cannot fade transition without image element. Was called without a fader in the scene?", this );
				return;
			}

			StopFade();
			m_fadeRoutine = StartCoroutine( CrossFade_Coroutine( targetAlpha, timespan ) );
		}

		public void StopFade()
		{
			if ( m_fadeRoutine != null )
			{
				StopCoroutine( m_fadeRoutine );
				m_fadeRoutine = null;
			}
		}

		protected override void Awake()
		{
			if ( m_fadeElement == null )
			{
				m_fadeElement = GetComponentInChildren<Image>();
			}

			if ( m_startBlackedOut )
			{
				FadeElement.color = new Color( 0, 0, 0, 1 );
			}

			base.Awake();
		}

		private IEnumerator CrossFade_Coroutine( float targetAlpha, float timespan )
		{
			float timer = 0;
			if ( timespan <= 0 )
			{
				timer = 1;
			}

			float start = m_fadeElement.color.a;

			while ( timer < 1 )
			{
				timer += Time.unscaledDeltaTime / timespan;
				float newAlpha = Mathf.Lerp( start, targetAlpha, timer );

				m_fadeElement.color = new Color( 0, 0, 0, newAlpha );
				yield return null;
			}

			m_fadeElement.color = new Color( 0, 0, 0, targetAlpha );
			m_fadeRoutine = null;
		}
	}
}