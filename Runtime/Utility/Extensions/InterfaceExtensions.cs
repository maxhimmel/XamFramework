using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Extensions
{
	public static class InterfaceExtensions
	{
		public static Transform ToTransform<TInterface>( this TInterface obj ) where TInterface : class
		{
			Component component = obj as Component;
			if ( component == null ) { return null; }

			return component.transform;
		}
		public static GameObject ToGameObject<TInterface>( this TInterface obj ) where TInterface : class
		{
			Component component = obj as Component;
			if ( component == null ) { return null; }

			return component.gameObject;
		}
	}
}