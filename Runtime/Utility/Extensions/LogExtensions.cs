using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace Xam.Utility.Extensions
{
	public enum Colors
	{
		Aqua,
		Black,
		Blue,
		Brown,
		Cyan,
		DarkBlue,
		Fuchsia,
		Green,
		Grey,
		LightBlue,
		Lime,
		Magenta,
		Maroon,
		Navy,
		Olive,
		Orange,
		Purple,
		Red,
		Silver,
		Teal,
		White,
		Yellow
	}

	public static class LogExtensions
	{
		private const Colors k_classNameColor = Colors.Navy;
		private const Colors k_defaultLogColor = Colors.Black;

		private const int k_classStylingCount = 3; // [gameObjectName]_
		private const int k_emboldenCharCount = 7; // <b></b>
		private const int k_coloredCharCount = 16; // <color=></color>

		private static string m_classNameColor;

		public static void Log( this MonoBehaviour self, string message, Colors color = k_defaultLogColor )
		{
			TryInitRefs();
			ConfigLoggingData( self, message, color, out int capacity, out string messageColor );

			StringBuilder sb = new StringBuilder( capacity );
			using ( new RtfBold( sb ) )
			{
				using ( new RtfColor( sb, m_classNameColor ) )
				{
					sb.AppendFormat( "[{0}] ", self.name );
				}
			}

			if ( string.IsNullOrEmpty( messageColor ) )
			{
				sb.Append( message );
			}
			else
			{
				using ( new RtfColor( sb, messageColor ) )
				{
					sb.AppendFormat( message );
				}
			}

			Debug.Log( sb, self );
		}

		private static void TryInitRefs()
		{
			if ( string.IsNullOrEmpty( m_classNameColor ) )
			{
				m_classNameColor = ToColor( k_classNameColor );
			}
		}

		private static void ConfigLoggingData( MonoBehaviour sender, string message, Colors color, out int messageCapacity, out string messageColor )
		{
			messageColor = (color != k_defaultLogColor) ? ToColor( color ) : string.Empty;

			int messageLength = string.IsNullOrEmpty( message ) ? 0 : message.Length;
			messageCapacity = messageLength;
			messageCapacity += sender.name.Length; // We always include the gameobject's name
			messageCapacity += k_emboldenCharCount; // We always embolden the class name
			messageCapacity += k_coloredCharCount + m_classNameColor.Length; // We always color the class name
			messageCapacity += k_classStylingCount; // We always add a space after the class name and wrap the class name with brackets []

			if ( !string.IsNullOrEmpty( messageColor ) )
			{
				messageCapacity += k_coloredCharCount + messageColor.Length;
			}
		}

		private static string ToColor( Colors color )
		{
			switch ( color )
			{
				default:
				case Colors.Grey: return "grey";

				case Colors.Aqua:
				case Colors.Cyan: return "cyan";

				case Colors.Black: return "black";
				case Colors.Blue: return "blue";
				case Colors.Brown: return "brown";
				case Colors.DarkBlue: return "darkblue";

				case Colors.Fuchsia:
				case Colors.Magenta: return "magenta";

				case Colors.Green: return "green";
				case Colors.LightBlue: return "lightblue";
				case Colors.Lime: return "lime";
				case Colors.Maroon: return "maroon";
				case Colors.Navy: return "navy";
				case Colors.Olive: return "olive";
				case Colors.Orange: return "orange";
				case Colors.Purple: return "purple";
				case Colors.Red: return "red";
				case Colors.Silver: return "silver";
				case Colors.Teal: return "teal";
				case Colors.White: return "white";
				case Colors.Yellow: return "yellow";
			}
		}
	}
}