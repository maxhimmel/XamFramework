using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Gameplay.Patterns
{
	public interface IFactory<T>
	{
		event System.EventHandler<T> Created;

		T Create( Vector3 position = default, Quaternion rotation = default, Transform parent = null );
	}
}