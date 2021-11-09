using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR && UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;

namespace Xam.Utility
{
	public class Notepad : MonoBehaviour
	{
		[HideLabel]
		[TextArea( minLines: 3, maxLines: 20 )]
		[SerializeField] private string m_note;
	}
}
#endif