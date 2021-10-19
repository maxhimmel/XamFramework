using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Juicy
{
	[RequireComponent( typeof( Renderer ) )]
	public class RendererColorModifier : MonoBehaviour, IColorModifier
	{
		private Renderer m_renderer;

		public Color GetCurrentColor()
		{
			return m_renderer.material.color;
		}

		public void SetCurrentColor( Color color )
		{
			m_renderer.material.color = color;
		}

		private void Awake()
		{
			m_renderer = GetComponent<Renderer>();
		}
	}
}