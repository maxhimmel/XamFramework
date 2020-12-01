using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Audio
{
	public class SfxClip : MonoBehaviour
	{
		[SerializeField] private bool m_playOnStart = false;

		[Space]
		[SerializeField] private SoundDatum m_soundData = new SoundDatum();

		private void Start()
		{
			if ( m_playOnStart )
			{
				PlaySfx();
			}
		}

		[ContextMenu( "Play SFX" )]
		public virtual void PlaySfx()
		{
			if ( !Application.isPlaying ) { return; }

			AudioManager.Instance.PlaySound( m_soundData );
		}


#if UNITY_EDITOR
		private void OnDrawGizmosSelected()
		{
			if ( m_soundData.Is3d )
			{
				Transform center = transform;
				if ( m_soundData.Attachment3d.Attachment != null )
				{
					center = m_soundData.Attachment3d.Attachment;
				}

				Gizmos.color = Color.white;
				Gizmos.DrawWireSphere( center.position, m_soundData.Attachment3d.MaxDistance );
				Gizmos.DrawWireSphere( center.position, m_soundData.Attachment3d.MinDistance );
			}
		}
#endif
	}
}