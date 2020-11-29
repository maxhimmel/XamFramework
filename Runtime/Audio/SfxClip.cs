using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Audio
{
	public class SfxClip : MonoBehaviour
	{
		[SerializeField] private SoundDatum m_soundData = default;

		[ContextMenu( "Play SFX" )]
		public virtual void PlaySfx()
		{
			if ( !Application.isPlaying ) { return; }

			AudioManager.Instance.PlaySound( m_soundData );
		}
	}
}