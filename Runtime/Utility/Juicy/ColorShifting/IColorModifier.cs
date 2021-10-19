using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility.Juicy
{
	public interface IColorModifier
	{
		Color GetCurrentColor();
		void SetCurrentColor( Color color );
	}
}