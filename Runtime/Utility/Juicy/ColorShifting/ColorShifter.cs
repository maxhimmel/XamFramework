using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Juicy
{
	public class ColorShifter : MonoBehaviour
	{
		[SerializeField] private bool m_playOnStart = true;
		[SerializeField] private bool m_useRandomColorAtStart = false;
		[SerializeField] private float m_shiftDuration = 2;

		[SerializeField] private ColorHsvDatum m_colorData = new ColorHsvDatum();

		private IColorModifier m_colorModifier = null;
		private Coroutine m_shiftingRoutine = null;

		private void Start()
		{
			if ( m_playOnStart )
			{
				Color startColor = m_useRandomColorAtStart
					? GetRandomColor()
					: m_colorModifier.GetCurrentColor();
				StartShiftingColors( startColor, GetRandomColor(), m_shiftDuration );
			}
		}

		private Color GetRandomColor()
		{
			return m_colorData.GetRandom();
		}

		private void StartShiftingColors( Color startColor, Color nextColor, float duration )
		{
			StopShiftingColors();

			m_shiftingRoutine = StartCoroutine( ShiftColors_Coroutine( startColor, nextColor, duration ) );
		}

		private void StopShiftingColors()
		{
			if ( m_shiftingRoutine != null )
			{
				StopCoroutine( m_shiftingRoutine );
				m_shiftingRoutine = null;
			}
		}

		private IEnumerator ShiftColors_Coroutine( Color startColor, Color nextColor, float duration )
		{
			float timer = 0;
			while ( timer < 1 )
			{
				timer += Time.deltaTime / duration;

				Color newColor = Color.Lerp( startColor, nextColor, timer );
				m_colorModifier.SetCurrentColor( newColor );

				yield return null;
			}

			m_colorModifier.SetCurrentColor( nextColor );

			StartShiftingColors( nextColor, GetRandomColor(), m_shiftDuration );
		}

		private void Awake()
		{
			m_colorModifier = GetComponent<IColorModifier>();
		}
	}
}