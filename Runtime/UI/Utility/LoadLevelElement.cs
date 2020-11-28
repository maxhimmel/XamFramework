using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Xam.Ui
{
	using Level;

	public class LoadLevelElement : MonoBehaviour
	{
		public void LoadLevel( string levelName )
		{
			if ( GameManager.Exists )
			{
				GameManager.Instance.LoadLevel( levelName );
			}
		}
	}
}