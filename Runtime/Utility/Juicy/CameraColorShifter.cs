using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Juicy
{
	public class CameraColorShifter : MonoBehaviour
	{
		[SerializeField] private bool m_playOnStart = true;
		[SerializeField] private bool m_useRandomColorAtStart = false;
		[SerializeField] private float m_shiftDuration = 2;

		[SerializeField] private ColorHsvDatum m_colorData = new ColorHsvDatum();

		private Camera m_camera = null;
		private Coroutine m_shiftingRoutine = null;

		private void Start()
		{
			if ( m_playOnStart )
			{
				Color startColor = m_useRandomColorAtStart
					? GetRandomColor()
					: GetCurrentColor();
				StartShiftingColors( startColor, GetRandomColor(), m_shiftDuration );
			}
		}

		private Color GetCurrentColor()
		{
			return m_camera.backgroundColor;
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
				SetCameraColor( newColor );

				yield return null;
			}

			SetCameraColor( nextColor );

			StartShiftingColors( nextColor, GetRandomColor(), m_shiftDuration );
		}

		private void SetCameraColor( Color color )
		{
			m_camera.backgroundColor = color;
		}

		private void Awake()
		{
			m_camera = GetComponent<Camera>();
		}
	}
}