using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Xam.Cinemachine
{
	public class PathLineRenderer : MonoBehaviour
	{
		private LineRenderer m_lineRenderer = null;
		private CinemachinePathBase m_path = null;

		private void Start()
		{
			SetRendererPositions( m_lineRenderer, m_path );

			CreateCollision( m_path );
		}

		private void SetRendererPositions( LineRenderer renderer, CinemachinePathBase path )
		{
			float maxStepLength = 1f / path.m_Resolution;
			int maxPoints = Mathf.CeilToInt( path.MaxPos / maxStepLength ) + 1;

			Vector3[] worldPoints = new Vector3[maxPoints];

			int idx = 0;
			for ( float tdx = 0; tdx < path.MaxPos; tdx += maxStepLength )
			{
				Vector3 worldPos = path.EvaluatePosition( tdx );
				worldPoints[idx++] = worldPos;
			}

			renderer.positionCount = maxPoints;
			renderer.SetPositions( worldPoints );

			renderer.useWorldSpace = true;
			renderer.loop = path.Looped;
		}

		private void CreateCollision( CinemachinePathBase path )
		{
			List<Vector3> vertices = new List<Vector3>();
			List<int> triangles = new List<int>();
			List<Vector3> normals = new List<Vector3>();


			float maxStepLength = 1f / path.m_Resolution;
			int maxPoints = Mathf.CeilToInt( path.MaxPos / maxStepLength ) + 1;
			float halfWidth = path.m_Appearance.width / 2f;


			Vector3 prevPos = path.EvaluatePosition( path.MinPos );
			Quaternion prevOrientation = path.EvaluateOrientation( path.MinPos );

			for ( float tdx = path.MinPos + maxStepLength; tdx < path.MaxPos; tdx += maxStepLength )
			{
				Vector3 nextPos = path.EvaluatePosition( tdx );
				Quaternion nextOrientation = path.EvaluateOrientation( tdx );

				Vector3 normal = nextOrientation * Vector3.up;
				Vector3 rightDir = nextOrientation * Vector3.right * halfWidth;

				Vector3 rightPos = nextPos + rightDir;
				Vector3 leftPos = nextPos - rightDir;



				Vector3 prevNormal = prevOrientation * Vector3.up;
				Vector3 prevRightDir = prevOrientation * Vector3.right * halfWidth;

				Vector3 prevRightPos = prevPos + prevRightDir;
				Vector3 prevLeftPos = prevPos - prevRightDir;




				Vector3 originOffset = transform.position;
				vertices.Add( prevLeftPos - originOffset );
				vertices.Add( rightPos - originOffset );
				vertices.Add( prevRightPos - originOffset );
				vertices.Add( leftPos - originOffset );


				triangles.Add( vertices.Count - 4 );
				triangles.Add( vertices.Count - 3 );
				triangles.Add( vertices.Count - 2 );

				triangles.Add( vertices.Count - 4 );
				triangles.Add( vertices.Count - 1 );
				triangles.Add( vertices.Count - 3 );


				normals.Add( prevNormal );
				normals.Add( normal );
				normals.Add( prevNormal );
				normals.Add( normal );


				prevPos = nextPos;
				prevOrientation = nextOrientation;
			}

			Mesh newMesh = new Mesh();
			newMesh.name = name;

			newMesh.vertices = vertices.ToArray();
			newMesh.triangles = triangles.ToArray();
			newMesh.normals = normals.ToArray();

			GameObject collisionObj = new GameObject( "Collision" );
			collisionObj.transform.SetParent( transform, false );

			MeshCollider collider = collisionObj.AddComponent<MeshCollider>();
			collider.sharedMesh = newMesh;
		}

		private void Awake()
		{
			m_path = GetComponentInParent<CinemachinePathBase>();
			m_lineRenderer = GetComponentInChildren<LineRenderer>();
		}
	}
}