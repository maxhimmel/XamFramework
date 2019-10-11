using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Audio
{
	public class AudioManager : Utility.Patterns.SingletonMono<AudioManager>
	{
		[Header( "Modifiers" )]
		[SerializeField] private float m_lowPitchRange = 0.95f;
		[SerializeField] private float m_highPitchRange = 1.05f;

		[Header( "References" )]
		[SerializeField] private UnityEngine.Audio.AudioMixer m_mainMixer = default;

		[Space]
		[SerializeField] private AudioSource m_sfxSource = default;
		[SerializeField] private AudioSource m_musicSource = default;

		public void PlaySfx( AudioClip clip, bool randomizePitch = true )
		{
			if ( clip == null ) { return; }


			m_sfxSource.clip = clip;

			m_sfxSource.pitch = randomizePitch
				? Random.Range( m_lowPitchRange, m_highPitchRange )
				: 1;

			m_sfxSource.Play();
		}

		public void PlayMusic( AudioClip clip )
		{
		}

		protected override void Awake()
		{
			base.Awake();

			m_sfxSource.outputAudioMixerGroup = m_mainMixer.FindMatchingGroups( "Sfx" )[0];
			m_musicSource.outputAudioMixerGroup = m_mainMixer.FindMatchingGroups( "Music" )[0];
		}
	}
}