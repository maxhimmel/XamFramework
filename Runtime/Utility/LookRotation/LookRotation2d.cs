using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility
{
	public class LookRotation2d : MonoBehaviour, ILookRotation
	{
		private const float k_rotateAngle = 90;

		[SerializeField] private Vector3 m_worldForward = Vector3.forward;

		Quaternion ILookRotation.GetLookRotation( Vector3 direction, Vector3 up )
		{
			Quaternion rotation = Quaternion.Euler( m_worldForward * k_rotateAngle );
			return Quaternion.LookRotation( m_worldForward, rotation * direction );
		}
	}
}