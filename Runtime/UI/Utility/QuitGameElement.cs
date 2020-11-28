using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Ui
{
	public class QuitGameElement : MonoBehaviour
	{
		public void Quit()
		{
			Application.Quit();

#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
#endif
		}
	}
}