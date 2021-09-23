using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if DO_TWEEN
using DG.Tweening;

namespace Xam.Gameplay.Vfx
{
	using Utility;

	public class MeshFadeEmancipation : Emancipator
	{
		[SerializeField] private float m_fadeDuration = 0.5f;
		[SerializeField] private Ease m_fadeEase = Ease.OutQuad;

		private Renderer m_renderer;
		private Tweener m_fadeTweener;

		public void SetDuration( float duration )
		{
			m_fadeDuration = duration;
		}

		protected override bool IsAlive()
		{
			return m_fadeTweener.IsActive();
		}

		protected override void OnEmancipated()
		{
			m_fadeTweener = DOVirtual.Float( 1, 0, m_fadeDuration, SetMaterialFade )
				.SetEase( m_fadeEase );
		}

		private void SetMaterialFade( float alpha )
		{
			Color color = m_renderer.material.color;
			color.a = alpha;

			m_renderer.material.color = color;
		}

		private void Awake()
		{
			m_renderer = GetComponentInChildren<Renderer>();
		}
	}
}
#endif