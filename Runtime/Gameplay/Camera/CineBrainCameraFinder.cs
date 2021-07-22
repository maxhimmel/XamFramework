using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Xam.Gameplay.Cameras
{
	public class CineBrainCameraFinder : MonoBehaviour, ICameraFinder
	{
		[SerializeField, Min( 0 )] private int m_cineBrainIndex = 0;

		private Camera m_camera = null;

		Camera ICameraFinder.GetCamera()
		{
			if ( m_camera != null ) { return m_camera; }

			CacheCamera();
			return m_camera;
		}

		private void CacheCamera()
		{
			CinemachineCore cineCore = CinemachineCore.Instance;
			if ( cineCore != null && cineCore.BrainCount > 0 )
			{
				CinemachineBrain brain = cineCore.GetActiveBrain( m_cineBrainIndex );
				if ( brain != null )
				{
					m_camera = brain.OutputCamera;
				}
			}
		}
	}
}