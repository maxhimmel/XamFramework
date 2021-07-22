using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Xam.Editor
{
	using System;
	using System.Linq;
	using UnityEngine.UIElements;
	using Utility;

	[CustomPropertyDrawer( typeof( ColorHsvDatum ), true )]
	public class ColorHsvDatumDrawer : PropertyDrawer
	{
		// https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/GUI/ColorPicker.cs#L330

		private const float k_minSaturation = 0.000001f;
		private static readonly GUIStyle k_sliderBackground = "ColorPickerSliderBackground";
		private static readonly Texture2D k_alphaSliderCheckerBackground =
				EditorGUIUtility.LoadRequired( "Previews/Textures/textureChecker.png" ) as Texture2D;

		private Dictionary<HsvLookup, MinMaxPair> m_hsvLookup = new Dictionary<HsvLookup, MinMaxPair>( 4 );
		private Dictionary<HsvLookup, Texture2D> m_hsvTextures = new Dictionary<HsvLookup, Texture2D>( 4 );
		private Texture2D m_hueTexture; float m_hueTextureS = -1, m_hueTextureV = -1;
		private Texture2D m_satTexture; float m_satTextureH = -1, m_satTextureV = -1;
		private Texture2D m_valTexture; float m_valTextureH = -1, m_valTextureS = -1;
		private Texture2D m_alphaTexture; float m_oldAlpha = -1;
		private GUIStyle m_hsvStyle = null;

		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{
			position.height = EditorGUIUtility.singleLineHeight;
			property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup( position, property.isExpanded, label );

			if ( property.isExpanded )
			{
				InitHsvLookup( property );

				Color.RGBToHSV( GetColor(), out float minH, out float minS, out float minV );

				using ( new EditorGUI.IndentLevelScope( EditorGUI.indentLevel + 1 ) )
				{
					DrawHsvProperty( ref position, property, HsvLookup.H, m_hueTexture, 64, 1, 1, ref m_hueTextureS, ref m_hueTextureV );
					DrawHsvProperty( ref position, property, HsvLookup.S, m_satTexture, 32, minH, Mathf.Max( minV, 0.2f ), ref m_satTextureH, ref m_satTextureV );
					DrawHsvProperty( ref position, property, HsvLookup.V, m_valTexture, 32, minH, minS, ref m_valTextureH, ref m_valTextureS );
					DrawHsvProperty( ref position, property, HsvLookup.A, m_alphaTexture, 32, 0, 0, ref m_oldAlpha, ref m_oldAlpha );

				}
			}

			EditorGUI.EndFoldoutHeaderGroup();
		}

		private void InitHsvLookup( SerializedProperty property )
		{
			TryUpdateHsvLookup( property, "Hue", HsvLookup.H );
			TryUpdateHsvLookup( property, "Saturation", HsvLookup.S );
			TryUpdateHsvLookup( property, "Value", HsvLookup.V );
			TryUpdateHsvLookup( property, "Alpha", HsvLookup.A );
		}

		private bool TryUpdateHsvLookup( SerializedProperty property, string name, HsvLookup id )
		{
			SerializedProperty minProp = property.FindPropertyRelative( $"{name}Min" );
			SerializedProperty maxProp = property.FindPropertyRelative( $"{name}Max" );

			if ( m_hsvLookup.TryGetValue( id, out MinMaxPair pair ) )
			{
				pair.Min = minProp.floatValue;
				pair.Max = maxProp.floatValue;
				return true;
			}
			else
			{
				m_hsvLookup[id] = new MinMaxPair( name, minProp.floatValue, maxProp.floatValue );
				return false;
			}
		}

		private void DrawHsvProperty( 
			ref Rect position, SerializedProperty property, HsvLookup id, Texture2D hsvTexture,
			int width, float hue, float saturation, ref float oldHue, ref float oldSaturation
		)
		{
			MinMaxPair pair = GetHsvProperty( id );
			if ( m_hsvStyle == null )
			{
				m_hsvStyle = new GUIStyle( EditorStyles.label );
			}

			position.y += position.height;
			position.y += EditorGUIUtility.singleLineHeight / 2f;

			using ( var check = new EditorGUI.ChangeCheckScope() )
			{
				Rect texPos = new Rect( position );
				texPos.y -= EditorGUIUtility.singleLineHeight / 2f;
				texPos.x += EditorGUIUtility.labelWidth + 1;
				texPos.width -= EditorGUIUtility.labelWidth - 1;

				TryDrawAlphaCheckBg( texPos, id );

				hsvTexture = Update1DSlider( hsvTexture, width, hue, saturation, ref oldHue, ref oldSaturation, (int)id, id != HsvLookup.A );
				m_hsvStyle.normal.background = hsvTexture;

				GUI.Box( texPos, GUIContent.none, m_hsvStyle );
				EditorGUI.MinMaxSlider( position, pair.Name, ref pair.Min, ref pair.Max, k_minSaturation, 1 );

				if ( check.changed )
				{
					OnColorChanged( property, pair );
				}
			}
		}

		private void TryDrawAlphaCheckBg( Rect position, HsvLookup id )
		{
			if ( id == HsvLookup.A && Event.current.type == EventType.Repaint )
			{
				Rect uvLayout = new Rect
				{
					width = position.width / position.height, // texture aspect is 1:1
					height = 1f
				};
				Graphics.DrawTexture( position, k_alphaSliderCheckerBackground, uvLayout, 0, 0, 0, 0 );
			}
		}

		private Texture2D Update1DSlider( 
			Texture2D tex, int xSize, float const1, float const2, ref float oldConst1, ref float oldConst2, int idx, bool isHsvSpace
		)
		{
			if ( !tex || const1 != oldConst1 || const2 != oldConst2 )
			{
				if ( !tex )
				{
					tex = MakeTexture( xSize, 2 );
				}

				Color[] colors = new Color[xSize * 2];
				Color start = Color.black, step = Color.black;
				switch ( idx )
				{
					case 0:
						start = new Color( 0, const1, const2, 1 );
						step = new Color( 1, 0, 0, 0 );
						break;
					case 1:
						start = new Color( const1, 0, const2, 1 );
						step = new Color( 0, 1, 0, 0 );
						break;
					case 2:
						start = new Color( const1, const2, 0, 1 );
						step = new Color( 0, 0, 1, 0 );
						break;
					case 3:
						start = GetColor();
						start.a = 0f;
						step = new Color( 0, 0, 0, 1 );
						break;
				}

				FillArea( xSize, 2, colors, start, step, new Color( 0, 0, 0, 0 ) );
				if ( isHsvSpace )
				{
					HsvToRgbArray( colors );
				}

				tex.SetPixels( colors );
				tex.Apply();
			}
			return tex;
		}

		public static Texture2D MakeTexture( int width, int height )
		{
			Texture2D tex = new Texture2D( width, height, TextureFormat.RGBA32, false, true )
			{
				hideFlags = HideFlags.HideAndDontSave,
				wrapMode = TextureWrapMode.Clamp
			};
			return tex;
		}

		static void FillArea( int xSize, int ySize, Color[] retval, Color topLeftColor, Color rightGradient, Color downGradient )
		{
			// Calc the deltas for stepping.
			Color rightDelta = new Color( 0, 0, 0, 0 ), downDelta = new Color( 0, 0, 0, 0 );
			if ( xSize > 1 )
				rightDelta = rightGradient / (xSize - 1);
			if ( ySize > 1 )
				downDelta = downGradient / (ySize - 1);

			// Assign all colors into the array
			Color p = topLeftColor;
			int current = 0;
			for ( int y = 0; y < ySize; y++ )
			{
				Color p2 = p;
				for ( int x = 0; x < xSize; x++ )
				{
					retval[current++] = p2;
					p2 += rightDelta;
				}
				p += downDelta;
			}
		}

		static void HsvToRgbArray( Color[] colors )
		{
			for ( int i = 0; i < colors.Length; i++ )
			{
				Color c = colors[i];
				Color c2 = Color.HSVToRGB( c.r, c.g, c.b );
				c2.a = c.a;
				colors[i] = c2;
			}
		}

		private Color GetColor()
		{
			float pingPong = Mathf.PingPong( (float)EditorApplication.timeSinceStartup, 1 );

			MinMaxPair huePair = GetHsvProperty( HsvLookup.H );
			float hue = Mathf.Lerp( huePair.Min, huePair.Max, pingPong );

			Color rgba = Color.HSVToRGB( 
				hue, 
				GetHsvProperty( HsvLookup.S ).Min, 
				GetHsvProperty( HsvLookup.V ).Min 
			);
			rgba.a = GetHsvProperty( HsvLookup.A ).Min;

			return rgba;
		}

		private void OnColorChanged( SerializedProperty property, MinMaxPair pair )
		{
			m_oldAlpha = -1;

			SerializedProperty minProp = property.FindPropertyRelative( $"{pair.Name}Min" );
			minProp.floatValue = pair.Min;
			SerializedProperty maxProp = property.FindPropertyRelative( $"{pair.Name}Max" );
			maxProp.floatValue = pair.Max;
		}

		private MinMaxPair GetHsvProperty( HsvLookup lookup )
		{
			return m_hsvLookup[lookup];
		}

		public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
		{
			float height = EditorGUIUtility.singleLineHeight;

			if ( property.isExpanded )
			{
				IEnumerable<SerializedProperty> childProps = property.GetVisibleChildren();

				float sliderHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.singleLineHeight / 2f;
				height += sliderHeight * childProps.Count() / 2f;
			}

			return height;
		}

		private class MinMaxPair
		{
			public string Name;
			public float Min = k_minSaturation;
			public float Max = 1;

			public MinMaxPair( string name, float min, float max )
			{
				Name = name;
				Min = min;
				Max = max;
			}
		}

		private enum HsvLookup
		{
			H,
			S,
			V,
			A
		}
	}
}