using System.Collections.Generic;
using UnityEngine;
using Pathfinding.Models;

namespace Pathfinding
{
	public class UnitPathfinding : MonoBehaviour
	{
		[SerializeField] private float speed = 5f;
		[SerializeField] private float stoppingDistance = 0.1f;

		private Vector2 targetPosition;
		private Vector2[] path;
		private Queue<Node> nodePath;
		private int currentPathIndex;
		private bool isMoving = false;

		private AstarPathfinding pathfinding;
		private CustomGrid grid;

		private void Start()
		{
			grid = AstarPathfindingManager.Instance.grid;
			pathfinding = new AstarPathfinding(grid);
		}

		private void OnEnable()
		{
			CustomGrid.OnWalkabilityChanged += HandleWalkabilityChanged;
		}

		private void OnDisable()
		{
			CustomGrid.OnWalkabilityChanged -= HandleWalkabilityChanged;
		}

		void Update()
		{
			if (isMoving)
			{
				MoveAlongPath();
			}
		}
		[ContextMenu(nameof(TEtsTarget))]
		public void TEtsTarget() => SetTarget(Vector2.zero);
		public void SetTarget(Vector2 targetPos)
		{
			targetPosition = targetPos;
			nodePath = pathfinding.FindPath(transform.position, targetPosition);
			if (nodePath != null && nodePath.Count > 0)
			{
				Node node;
				path = new Vector2[nodePath.Count];
				for (int i1 = 0; i1 < nodePath.Count; ++i1)
				{
					node = nodePath.Dequeue();
					path[i1] = grid.GetCellCenter(node.X, node.Y);
				}
				currentPathIndex = 0;
				isMoving = true;
			}
			else
			{
				isMoving = false;
				Debug.LogWarning("Hedefe yol bulunamadý!");
			}
		}

		private void MoveAlongPath()
		{
			if (currentPathIndex >= path.Length)
			{
				isMoving = false;
				return;
			}

			Vector2 nextPosition = path[currentPathIndex];
			transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

			if (Vector2.Distance(transform.position, nextPosition) < 0.01f)
			{
				currentPathIndex++;
			}

			if (IsAtTarget())
			{
				isMoving = false;
			}
		}

		private bool IsAtTarget()
		{
			return Vector2.Distance(transform.position, targetPosition) <= stoppingDistance;
		}

		private void HandleWalkabilityChanged(Node changedNode)
		{
			if (isMoving && nodePath != null && nodePath.Contains(changedNode) && !changedNode.IsWalkable)
			{
				SetTarget(targetPosition);
			}
		}
	}
}