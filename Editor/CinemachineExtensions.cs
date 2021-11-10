using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine;

namespace Xam.Editor.Extensions
{
	public static class CinemachineExtensions
	{
		[MenuItem( "CONTEXT/CinemachineImpulseSource/Generate Impulse" )]
		public static void GenerateImpulse( MenuCommand command )
		{
			if ( !Application.isPlaying ) { return; }

			var impulseSource = command.context as CinemachineImpulseSource;
			impulseSource.GenerateImpulse();
		}
	}
}