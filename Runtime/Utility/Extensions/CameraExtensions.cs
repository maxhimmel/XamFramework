using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Extensions
{
	public static class CameraExtensions
	{
		/// <summary>
		/// Returned bounds is centered at <paramref name="camera"/>'s transform position.
		/// </summary>
		/// <param name="camera"></param>
		/// <returns></returns>
		public static Bounds GetOrthoWorldBounds( this Camera camera )
		{
			float orthoHeight = camera.orthographicSize * 2;
			float orthoWidth = camera.aspect * orthoHeight;

			Vector3 orthoWorldSize = new Vector3( orthoWidth, orthoHeight );
			return new Bounds( camera.transform.position, orthoWorldSize );
		}
	}
}