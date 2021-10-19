using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Juicy
{
	[RequireComponent( typeof( Camera ) )]
	public class CameraColorModifier : MonoBehaviour, IColorModifier
	{
		private Camera m_camera;

		public Color GetCurrentColor()
		{
			return m_camera.backgroundColor;
		}

		public void SetCurrentColor( Color color )
		{
			m_camera.backgroundColor = color;
		}

		private void Awake()
		{
			m_camera = GetComponent<Camera>();
		}
	}
}