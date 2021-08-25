using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Randomization
{
	public abstract class RandomRange<T>
	{
		public T Min = default;
		public T Max = default;

		public RandomRange() { }
		public RandomRange( T min, T max )
		{
			Min = min;
			Max = max;
		}

		public abstract T Evaluate();
	}

	[System.Serializable]
	public class RandomFloatRange : RandomRange<float>
	{
		public RandomFloatRange() { }
		public RandomFloatRange( float min, float max ) : base( min, max )
		{
		}

		public override float Evaluate()
		{
			return Random.Range( Min, Max );
		}
	}

	[System.Serializable]
	public class RandomIntRange : RandomRange<int>
	{
		public RandomIntRange() { }
		public RandomIntRange( int min, int max ) : base( min, max )
		{
		}

		public override int Evaluate()
		{
			return Random.Range( Min, Max );
		}
	}
}