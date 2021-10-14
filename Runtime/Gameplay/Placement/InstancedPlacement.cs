using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Xam.Gameplay
{
	using Xam.Utility;

	public abstract class InstancedPlacement : MonoBehaviour, IPlacement
	{
		public virtual Vector3 Center => transform.position;

		protected ILookRotation m_lookRotation = null;

		public void GetNextOrientation( int placementIndex, int totalPlacements, out Vector3 position, out Quaternion rotation, Space space = Space.World )
		{
			position = GetNextPosition( placementIndex, totalPlacements, space );
			rotation = GetNextRotation( placementIndex, totalPlacements, position );
		}

		public abstract Vector3 GetNextPosition( int placementIndex, int totalPlacements, Space space );
		public abstract Quaternion GetNextRotation( int placementIndex, int totalPlacements, Vector3 position );

		protected virtual void Awake()
		{
			m_lookRotation = GetComponentInChildren<ILookRotation>();
		}

		protected virtual void OnValidate()
		{
			ILookRotation lookRotation = GetComponentInChildren<ILookRotation>();
			Debug.Assert( lookRotation != null, "Please attach either a 2D or 3D LookRotation to this object.", this );
		}

#if ODIN_INSPECTOR && UNITY_EDITOR
		[ButtonGroup( VisibleIf = "IsLookRotationMissing" )]
		[Button( "Add 2D Look Rotation", DirtyOnClick = false )]
		private void Add2DLookRotation()
		{
			UnityEditor.Undo.AddComponent<LookRotation2d>( gameObject );
		}

		[ButtonGroup( VisibleIf = "IsLookRotationMissing" )]
		[Button( "Add 3D Look Rotation", DirtyOnClick = false )]
		private void Add3DLookRotation()
		{
			UnityEditor.Undo.AddComponent<LookRotation3d>( gameObject );
		}

		private bool IsLookRotationMissing()
		{
			return GetComponentInChildren<ILookRotation>() == null;
		}
#endif
	}
}