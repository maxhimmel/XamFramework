using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Xam.Editor
{
	using Xam.Utility.Randomization;

	[CustomPropertyDrawer( typeof( WeightedList ), true )]
	public class WeightedListDrawer : PropertyDrawer
	{
		public const string k_itemsPropertyName = "m_items";

		private ReorderableList m_reorderableNodes = null;
		private SerializedProperty m_itemsProperty = null;

		public WeightedListDrawer()
		{
			m_itemsProperty = null;
			m_reorderableNodes = null;
		}

		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{
			OnGUI_ItemsList( position, property );

			if ( property.serializedObject.hasModifiedProperties )
			{
				NormalizeWeights( m_itemsProperty );

				property.serializedObject.ApplyModifiedProperties();
			}
		}

		private void OnGUI_ItemsList( Rect position, SerializedProperty property )
		{
			if ( m_reorderableNodes == null )
			{
				InitializeReorderableList( property );
			}

			m_reorderableNodes.drawHeaderCallback = ( Rect headerPos ) =>
			{
				EditorGUI.LabelField( headerPos, property.displayName );
			};

			m_reorderableNodes.drawElementCallback = ( Rect elementPos, int index, bool isActive, bool isFocused ) =>
			{
				SerializedProperty elementProperty = m_itemsProperty.GetArrayElementAtIndex( index );
				elementPos.height = EditorGUI.GetPropertyHeight( elementProperty );
				elementPos.y += EditorGUIUtility.standardVerticalSpacing;

				EditorGUI.PropertyField( elementPos, elementProperty );
			};

			m_reorderableNodes.elementHeightCallback = ( int index ) =>
			{
				SerializedProperty elementProperty = m_itemsProperty.GetArrayElementAtIndex( index );
				float height = EditorGUI.GetPropertyHeight( elementProperty );

				return height + EditorGUIUtility.singleLineHeight / 2f;
			};

			m_reorderableNodes.DoList( position );
		}

		private void InitializeReorderableList( SerializedProperty property )
		{
			m_itemsProperty = property.FindPropertyRelative( k_itemsPropertyName );
			m_reorderableNodes = new ReorderableList( property.serializedObject, m_itemsProperty, true, true, true, true );
		}

		protected void NormalizeWeights( SerializedProperty itemsProperty )
		{
			int weightSum = 0;
			for ( int idx = 0; idx < itemsProperty.arraySize; ++idx )
			{
				SerializedProperty elementProperty = itemsProperty.GetArrayElementAtIndex( idx );
				SerializedProperty elementWeightProperty = elementProperty.FindPropertyRelative( WeightedNodeDrawer.k_normalizedWeightPropertyName );

				weightSum += elementWeightProperty.intValue;
			}

			if ( weightSum <= 0 ) { return; }

			for ( int idx = 0; idx < itemsProperty.arraySize; ++idx )
			{
				SerializedProperty elementProperty = itemsProperty.GetArrayElementAtIndex( idx );
				SerializedProperty elementWeightProperty = elementProperty.FindPropertyRelative( WeightedNodeDrawer.k_normalizedWeightPropertyName );

				float influence = elementWeightProperty.intValue / (float)weightSum;
				int normalizedWeight = Mathf.RoundToInt( influence * WeightedList.k_maxWeight );

				elementWeightProperty.intValue = normalizedWeight;
			}
		}

		public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
		{
			if ( m_reorderableNodes == null )
			{
				SerializedProperty itemsProperty = property.FindPropertyRelative( k_itemsPropertyName );
				if ( itemsProperty.arraySize <= 0 || !itemsProperty.isExpanded ) { return EditorGUIUtility.singleLineHeight; }

				SerializedProperty firstArrayProperty = itemsProperty.GetArrayElementAtIndex( 0 );
				float arrayItemHeight = EditorGUI.GetPropertyHeight( firstArrayProperty );

				return (itemsProperty.arraySize) * arrayItemHeight + 3 * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
			}

			return m_reorderableNodes.GetHeight();
		}
	}


	[CustomPropertyDrawer( typeof( WeightedNode ), true )]
	public class WeightedNodeDrawer : PropertyDrawer
	{
		public const string k_normalizedWeightPropertyName = "m_normalizedWeight";
		public const string k_weightPropertyName = "Weight";
		public const string k_itemPropertyName = "Item";

		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{
			EditorGUI.DrawRect( position, new Color( 0, 0, 0, 0.2f ) );

			position.height = EditorGUIUtility.singleLineHeight;

			SerializedProperty weightProperty = property.FindPropertyRelative( k_normalizedWeightPropertyName );
			EditorGUI.PropertyField( position, weightProperty, new GUIContent( "Weight" ) );
			position.y += EditorGUI.GetPropertyHeight( weightProperty );

			position.y += EditorGUIUtility.standardVerticalSpacing;

			SerializedProperty itemProperty = property.FindPropertyRelative( k_itemPropertyName );
			if ( itemProperty != null )
			{
				EditorGUI.PropertyField( position, itemProperty, true );
			}
			else
			{
				EditorGUI.LabelField( position, "Non-serialized property in use" );
			}
		}

		public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
		{
			int numProperties = 1; // "Weight" property ...

			SerializedProperty itemProperty = property.FindPropertyRelative( k_itemPropertyName );
			numProperties += itemProperty != null
				? itemProperty.CountInProperty()
				: 1; // "Non-serialized property in use" warning ...

			return numProperties * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
		}
	}
}