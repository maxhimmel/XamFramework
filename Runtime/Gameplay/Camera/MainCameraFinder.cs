using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay.Cameras
{
	public class MainCameraFinder : MonoBehaviour, ICameraFinder
	{
		private Camera m_camera = null;

		Camera ICameraFinder.GetCamera()
		{
			if ( m_camera != null ) { return m_camera; }

			CacheCamera();
			return m_camera;
		}

		private void CacheCamera()
		{
			m_camera = Camera.main;
		}
	}
}