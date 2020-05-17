using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Xam.Ui
{
	[RequireComponent( typeof( Selectable ) )]
	public class SelectOnEnable : MonoBehaviour
	{
		private Selectable m_selectable = null;

		private void OnEnable()
		{
			if ( m_selectable == null )
			{
				m_selectable = GetComponent<Selectable>();
			}

			if ( EventSystem.current != null )
			{
				EventSystem.current.SetSelectedGameObject( m_selectable.gameObject );
			}
		}
	}
}