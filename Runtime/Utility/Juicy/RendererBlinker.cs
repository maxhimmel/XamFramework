using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Juicy
{
	using Extensions;

	[RequireComponent( typeof( Renderer ) )]
	public class RendererBlinker : MonoBehaviour
	{
		[SerializeField] private float m_blinkFrequency = 0.2f;

		private Renderer m_renderer;
		private Coroutine m_blinkRoutine;

		public void Play()
		{
			this.TryStopCoroutine( ref m_blinkRoutine );
			m_blinkRoutine = StartCoroutine( UpdateBlinking() );
		}

		private IEnumerator UpdateBlinking()
		{
			while ( enabled )
			{
				m_renderer.enabled = !m_renderer.enabled;
				yield return new WaitForSeconds( m_blinkFrequency );
			}

			m_blinkRoutine = null;
		}

		public void Stop( bool isRendererEnabled )
		{
			this.TryStopCoroutine( ref m_blinkRoutine );
			m_renderer.enabled = isRendererEnabled;
		}

		private void Awake()
		{
			m_renderer = GetComponent<Renderer>();
		}
	}
}