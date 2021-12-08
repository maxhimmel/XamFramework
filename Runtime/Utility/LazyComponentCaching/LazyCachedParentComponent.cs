using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility
{
	public class LazyCachedParentComponent<T> : LazyCachedComponent<T>
		where T : class
	{
		private readonly bool k_includeInactiveChildren;

		public LazyCachedParentComponent( bool includeInactiveParents )
		{
			k_includeInactiveChildren = includeInactiveParents;
		}

		protected override T GetComponent( GameObject root )
		{
			return root.GetComponentInParent<T>( k_includeInactiveChildren );
		}
	}
}