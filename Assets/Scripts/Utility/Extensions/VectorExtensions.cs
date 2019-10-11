using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Extensions
{
	public static class VectorExtensions
	{
		public static Vector3 Flatten( this Vector3 self )
		{
			return new Vector3()
			{
				x = self.x,
				z = self.z
			};
		}

		public static Vector3 FlattenNormalized( this Vector3 self )
		{
			return new Vector3()
			{
				x = self.x,
				z = self.z
			}.normalized;
		}

		public static Vector3 Divide( this Vector3 numerator, Vector3 denominator )
		{
			return new Vector3()
			{
				x = numerator.x / denominator.x,
				y = numerator.y / denominator.y,
				z = numerator.z / denominator.z
			};
		}
	}
}