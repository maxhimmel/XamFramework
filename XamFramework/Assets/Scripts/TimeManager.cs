using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay
{
	public class TimeManager : Utility.Patterns.SingletonMono<TimeManager>
	{
		[SerializeField] private AnimationCurve m_scalingCurve = AnimationCurve.Linear( 0, 0, 1, 1 );

		private float m_fixedTimeStep = -1;
		private Coroutine m_timeScaleRoutine = null;

		public void SetTimeScale( float scale, float transitionTime )
		{
			StopScalingOvertime();

			m_timeScaleRoutine = StartCoroutine( SetScaleOverTime_Coroutine( scale, transitionTime ) );
		}

		private void StopScalingOvertime()
		{
			if ( m_timeScaleRoutine != null )
			{
				StopCoroutine( m_timeScaleRoutine );
				m_timeScaleRoutine = null;
			}
		}

		private IEnumerator SetScaleOverTime_Coroutine( float newScale, float transitionTime )
		{
			float timer = 0;
			if ( transitionTime <= 0 )
			{
				// Fake the completion of the coroutine since we want to apply the scale immediately ...
				timer = 1;
			}

			float startScale = Time.timeScale;
			float endScale = newScale;

			while ( timer < 1 )
			{
				timer += Time.unscaledDeltaTime / transitionTime;

				float lerpValue = m_scalingCurve.Evaluate( timer );
				float scale = Mathf.LerpUnclamped( startScale, endScale, lerpValue );

				SetTimeScale_Internal( scale );
				yield return null;
			}
			
			SetTimeScale_Internal( endScale );
			m_timeScaleRoutine = null;
		}

		private void SetTimeScale_Internal( float newScale )
		{
			Time.timeScale = newScale;
			Time.fixedDeltaTime = m_fixedTimeStep * newScale;
		}

		protected override void Awake()
		{
			base.Awake();

			m_fixedTimeStep = Time.fixedDeltaTime;
		}
	}
}