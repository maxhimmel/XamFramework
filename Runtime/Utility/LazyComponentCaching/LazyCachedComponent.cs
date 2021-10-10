using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility
{
	public class LazyCachedComponent<T>
		where T : class
	{
		public T this[Component root] 
		{
			get 
			{
				return this[root.gameObject];
			}
			set
			{
				this[root.gameObject] = value;
			}
		}

		public T this[GameObject root] 
		{
			get 
			{
				if ( m_cachedComponent != null ) { return m_cachedComponent; }

				m_cachedComponent = GetComponent( root );
				return m_cachedComponent;
			}
			set
			{
				m_cachedComponent = value;
			}
		}

		private T m_cachedComponent = null;

		protected virtual T GetComponent( GameObject root )
		{
			return root.GetComponent<T>();
		}
	}
}