using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Xam.Utility.Patterns
{
	public class DynamicPool : SingletonMono<DynamicPool>
	{
		private Dictionary<System.Type, List<Component>> m_pooledObjectsByType = new Dictionary<System.Type, List<Component>>();

		public T GetFirstPooledObjectByType<T>() where T : Component
		{
			IEnumerable<T> pooledObjects = GetPooledObjectsByType<T>( out int count );

			return count > 0
				? pooledObjects.First()
				: null;
		}

		public IEnumerable<T> GetPooledObjectsByType<T>() where T : Component
		{
			if ( m_pooledObjectsByType.TryGetValue( typeof( T ), out List<Component> result ) && result != null )
			{
				return result.Cast<T>();
			}
			return null;
		}

		public IEnumerable<T> GetPooledObjectsByType<T>( out int count ) where T : Component
		{
			if ( m_pooledObjectsByType.TryGetValue( typeof( T ), out List<Component> result ) && result != null )
			{
				count = result.Count;
				return result.Cast<T>();
			}

			count = 0;
			return null;
		}

		public void AddPooledObjectByType<T>( Component newObj ) where T : Component
		{
			if ( newObj == null ) { return; }

			System.Type type = typeof( T );
			if ( m_pooledObjectsByType.TryGetValue( type, out List<Component> result ) )
			{
				result.Add( newObj );
				return;
			}

			result = new List<Component>() { newObj };
			m_pooledObjectsByType[type] = result;
		}

		public void RemovePooledObjectByType<T>( Component staleObj ) where T : Component
		{
			if ( staleObj == null ) { return; }

			System.Type type = typeof( T );
			if ( m_pooledObjectsByType.TryGetValue( type, out List<Component> result ) )
			{
				result.Remove( staleObj );
				return;
			}
		}
	}
}