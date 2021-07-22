using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Xam.Initialization
{
	using Utility;

	public class CircleZoomTransition : MonoBehaviour, ITransition
	{
		private const float k_edgeBuffer = 50;

		[SerializeField] private ColorHsvDatum m_hsvColor = new ColorHsvDatum( 0, 1, 0.6f, 1, 0.7f, 1, 1, 1 );

		private Image m_circleElement = null;
		private Coroutine m_zoomRoutine = null;

		void ITransition.Open( float duration )
		{
			StartZoom( duration, Vector2.zero );
		}

		void ITransition.Close( float duration )
		{
			Vector2 screenSize = GetCanvasSize();
			float screenFillSize = screenSize.magnitude + k_edgeBuffer;
			
			Vector2 circlePos = m_circleElement.rectTransform.anchoredPosition;
			float distFromCenter = circlePos.magnitude * 2;

			Vector2 targetZoom = Vector2.one * (screenFillSize + distFromCenter);
			StartZoom( duration, targetZoom );
		}

		private void StartZoom( float duration, Vector2 zoomTarget )
		{
			if ( duration > 0 )
			{
				m_circleElement.color = m_hsvColor.GetRandom();
			}

			StopZoom();
			m_zoomRoutine = StartCoroutine( UpdateZoom( duration, zoomTarget ) );
		}

		private void StopZoom()
		{
			if ( m_zoomRoutine != null )
			{
				StopCoroutine( m_zoomRoutine );
				m_zoomRoutine = null;
			}
		}

		private IEnumerator UpdateZoom( float duration, Vector2 zoomTarget )
		{
			float timer = 0;
			if ( duration <= 0 )
			{
				timer = 1;
			}

			Vector2 startSize = m_circleElement.rectTransform.sizeDelta;

			while ( timer < 1 )
			{
				timer += Time.deltaTime / duration;
				Vector2 nextSize = Vector2.Lerp( startSize, zoomTarget, timer );

				m_circleElement.rectTransform.sizeDelta = nextSize;
				yield return null;
			}

			m_circleElement.rectTransform.sizeDelta = zoomTarget;
		}

		private Vector2 GetCanvasSize()
		{
			RectTransform rectTrans = transform as RectTransform;
			return rectTrans.sizeDelta;
		}

		private void Awake()
		{
			m_circleElement = GetComponentInChildren<Image>();
		}
	}
}