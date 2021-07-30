using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility
{
	public interface ILookRotation
	{
		Quaternion GetLookRotation( Vector3 direction, Vector3 up );
	}
}