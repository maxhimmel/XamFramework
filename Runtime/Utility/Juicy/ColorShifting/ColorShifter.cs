using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Juicy
{
	public class ColorShifter : MonoBehaviour
	{
		[SerializeField] private bool m_useRandomColorAtStart = false;
		[SerializeField] private float m_shiftDuration = 2;

		[SerializeField] private ColorHsvDatum m_colorData = new ColorHsvDatum();

		private IColorModifier m_colorModifier = null;
		private float m_timer = 0;
		private Color m_startColor;
		private Color m_nextColor;

		private void Start()
		{
			m_startColor = m_useRandomColorAtStart
				? GetRandomColor()
				: m_colorModifier.GetCurrentColor();

			m_nextColor = GetRandomColor();
		}

		private Color GetRandomColor()
		{
			return m_colorData.GetRandom();
		}

		private void Update()
		{
			m_timer += Time.deltaTime / m_shiftDuration;

			Color newColor = Color.Lerp( m_startColor, m_nextColor, m_timer );
			m_colorModifier.SetCurrentColor( newColor );

			if ( m_timer >= 1 )
			{
				m_timer = 0;
				m_startColor = m_nextColor;
				m_nextColor = GetRandomColor();
			}
		}

		private void Awake()
		{
			m_colorModifier = GetComponent<IColorModifier>();
		}
	}
}