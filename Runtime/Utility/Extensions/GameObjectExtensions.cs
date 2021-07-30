using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Extensions
{
	public static class GameObjectExtensions
	{
		/// <summary>
		/// <para>Returns true if the component was added.</para>
		/// <para>Returns false if the component already existed.</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="component"></param>
		/// <returns></returns>
		public static bool TryAddComponent<T>( this GameObject gameObject, out T component ) where T : Component
		{
			if ( gameObject.TryGetComponent<T>( out component ) ) { return false; }

			component = gameObject.AddComponent<T>();
			return true;
		}
	}
}