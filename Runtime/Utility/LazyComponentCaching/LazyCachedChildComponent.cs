using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility
{
	public class LazyCachedChildComponent<T> : LazyCachedComponent<T>
		where T : class
	{
		private readonly bool k_includeInactiveChildren;

		public LazyCachedChildComponent(bool includeInactiveChildren )
		{
			k_includeInactiveChildren = includeInactiveChildren;
		}

		protected override T GetComponent( GameObject root )
		{
			return root.GetComponentInChildren<T>( k_includeInactiveChildren );
		}
	}
}