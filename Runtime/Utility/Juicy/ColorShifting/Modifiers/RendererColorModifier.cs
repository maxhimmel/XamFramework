using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Juicy
{
	[RequireComponent( typeof( Renderer ) )]
	public class RendererColorModifier : MonoBehaviour, IColorModifier
	{
		[SerializeField] private string m_colorPropertyName = "_Color";

		private Renderer m_renderer;
		private MaterialPropertyBlock m_propertyBlock;

		public Color GetCurrentColor()
		{
			return m_renderer.material.color;
		}

		public void SetCurrentColor( Color color )
		{
			m_propertyBlock.SetColor( m_colorPropertyName, color );
			m_renderer.SetPropertyBlock( m_propertyBlock );
		}

		private void Awake()
		{
			m_renderer = GetComponent<Renderer>();
			m_propertyBlock = new MaterialPropertyBlock();
		}
	}
}