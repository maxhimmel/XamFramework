using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Randomization
{
	public static class RandomUtility
	{
		public static int Sign()
		{
			return Bool()
				? 1
				: -1;
		}

		public static bool Bool()
		{
			return Random.value <= 0.5f;
		}
	}
}