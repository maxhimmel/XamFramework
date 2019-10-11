using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Audio
{
	public class SfxClip : MonoBehaviour
	{
		[SerializeField] private AudioClip m_clip = default;
		[SerializeField] private bool m_randomizePicth = true;

		public virtual void PlaySfx()
		{
			AudioManager.Instance.PlaySfx( m_clip, m_randomizePicth );
		}
	}
}