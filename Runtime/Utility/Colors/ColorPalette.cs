using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Utility
{
    [CreateAssetMenu( fileName = "NewColorPalette", menuName = "Xam/Color Palette", order = 0 )]
    public class ColorPalette : ScriptableObject
    {
        public int Count => m_gradients.Length;

        [SerializeField] private Gradient[] m_gradients = new Gradient[0];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">Invalid indices return magenta.</param>
        /// <param name="gradient">A value between 0 and 1.</param>
        /// <returns></returns>
        public Color GetColor( int index, float gradient = 0 )
		{
            if ( index < 0 || index >= m_gradients.Length ) { return Color.magenta; }

            return m_gradients[index].Evaluate( gradient );
		}
    }
}