using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility
{
	public class LookRotation3d : MonoBehaviour, ILookRotation
	{
		Quaternion ILookRotation.GetLookRotation( Vector3 direction, Vector3 up )
		{
			return Quaternion.LookRotation( direction, up );
		}
	}
}