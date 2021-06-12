using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Xam.Editor
{
	using Attributes;

	[CustomPropertyDrawer( typeof( TypeConstraintAttribute ), true )]
	public class TypeConstraintDrawer : PropertyDrawer
	{
		private System.Type Constraint { get { return (attribute as TypeConstraintAttribute).Constraint; } }

		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{
			UpdateInvalidDragAndDrop();

			Object propObj = property.objectReferenceValue;
			System.Type propertyObjRefType = GetObjectType( propObj );

			if ( !CanBeAssigned( propertyObjRefType ) )
			{
				ClearInvalidAssignment( property );
			}

			TryAssignTypeConstraintComponent( property );

			EditorGUI.PropertyField( position, property, label );
		}

		private void UpdateInvalidDragAndDrop()
		{
			switch ( Event.current.type )
			{
				default: return;

				case EventType.Layout:
				case EventType.Repaint:
				case EventType.DragUpdated:
					Object draggedObject = DragAndDrop.objectReferences.Length > 0
						? DragAndDrop.objectReferences[0]
						: null;

					System.Type draggedType = GetObjectType( draggedObject );

					if ( !CanBeAssigned( draggedType ) )
					{
						DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
					}
					break;
			}
		}

		private System.Type GetObjectType( Object obj )
		{
			if ( obj == null ) { return null; }

			System.Type result = obj.GetType();

			Component foundComponent = GetObjectConstraintComponent( obj );
			if ( foundComponent != null )
			{
				result = foundComponent.GetType();
			}

			return result;
		}

		private Component GetObjectConstraintComponent( Object obj )
		{
			GameObject draggedGameObject = obj as GameObject;
			if ( draggedGameObject == null ) { return null; }

			Component foundComponent = draggedGameObject.GetComponent( Constraint );
			return foundComponent;
		}

		private bool CanBeAssigned( System.Type sample )
		{
			return Constraint.IsAssignableFrom( sample );
		}

		private void ClearInvalidAssignment( SerializedProperty property )
		{
			using ( var check = new EditorGUI.ChangeCheckScope() )
			{
				property.objectReferenceValue = null;

				if ( check.changed )
				{
					property.serializedObject.Update();
					property.serializedObject.ApplyModifiedPropertiesWithoutUndo();
				}
			}
		}

		private void TryAssignTypeConstraintComponent( SerializedProperty property )
		{
			GameObject propGameObj = property.objectReferenceValue as GameObject;
			if ( propGameObj == null ) { return; }

			Component objComponent = GetObjectConstraintComponent( propGameObj );
			if ( objComponent == null ) { return; }

			using ( var check = new EditorGUI.ChangeCheckScope() )
			{
				property.objectReferenceValue = objComponent;

				if ( check.changed )
				{
					property.serializedObject.Update();
					property.serializedObject.ApplyModifiedPropertiesWithoutUndo();
				}
			}
		}
	}
}