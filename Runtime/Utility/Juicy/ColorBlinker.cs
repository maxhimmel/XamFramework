using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xam.Utility.Extensions;

namespace Xam.Utility.Juicy
{
	public class ColorBlinker : MonoBehaviour
	{
		[SerializeField] private float m_blinkFrequency = 0.2f;
		[SerializeField] private Color m_blinkColor = Color.white;

		private IColorModifier m_colorModifier;
		private Coroutine m_blinkRoutine;
		private Color m_initialColor;

		public void SetFrequency( float frequency )
		{
			m_blinkFrequency = frequency;
		}

		public void Play()
		{
			this.TryStopCoroutine( ref m_blinkRoutine );
			m_blinkRoutine = StartCoroutine( UpdateBlinking() );
		}

		private IEnumerator UpdateBlinking()
		{
			bool isBlinkColor = true;

			while ( enabled )
			{
				Color nextColor = isBlinkColor
					? m_blinkColor
					: m_initialColor;

				isBlinkColor = !isBlinkColor;

				m_colorModifier.SetCurrentColor( nextColor );
				yield return new WaitForSeconds( m_blinkFrequency );
			}

			m_blinkRoutine = null;
		}

		public void Stop()
		{
			this.TryStopCoroutine( ref m_blinkRoutine );
			m_colorModifier.SetCurrentColor( m_initialColor );
		}

		private void Start()
		{
			m_initialColor = m_colorModifier.GetCurrentColor();
		}

		private void Awake()
		{
			m_colorModifier = GetComponent<IColorModifier>();
		}
	}
}