using System.Collections.Generic;
using UnityEngine;
using Pathfinding.Models;
using UnityEngine.Events;

namespace Pathfinding
{
	public class UnitPathfinding : MonoBehaviour
	{
		[SerializeField] private float _speed = 5f;
		[SerializeField] private float _stoppingDistance = 0.1f;

		private Vector2 targetPosition;
		private Vector2[] path;
		private Queue<Node> nodePath;
		private int currentPathIndex;
		private bool isMoving = false;

		public FnishEvent _onComplate = new FnishEvent();
		private AstarPathfindingManager _pathfindingManager => AstarPathfindingManager.Instance;
		

		private void OnEnable()
		{
			CustomGrid.OnWalkabilityChanged += HandleWalkabilityChanged;
		}

		private void OnDisable()
		{
			CustomGrid.OnWalkabilityChanged -= HandleWalkabilityChanged;
			ClearValues();
		}

		void Update()
		{
			if (isMoving)
			{
				MoveAlongPath();
			}
		}

		/// <summary>
		/// Initiates movement towards a target position with specified speed and stopping distance.
		/// Optionally, a callback can be provided to execute upon completion.
		/// </summary>
		/// <param name="targetPos">The target position to move towards.</param>
		/// <param name="speed">The movement speed of the unit.</param>
		/// <param name="stoppingDistance">The distance at which the unit stops from the target.</param>
		/// <param name="onComplate">Optional callback to invoke when movement is complete.</param>
		public void GoMove(Vector2 targetPos, float speed, float stoppingDistance, UnityAction onComplate = null)
		{
			_speed = speed;
			_stoppingDistance = stoppingDistance;
			SetTarget(targetPos, onComplate);
		}


		/// <summary>
		/// Sets a new target position for the unit to move towards and calculates the path.
		/// Optionally, a callback can be provided to execute upon reaching the target.
		/// </summary>
		/// <param name="targetPos">The target position to move towards.</param>
		/// <param name="onComplate">Optional callback to invoke when the target is reached.</param>
		public void SetTarget(Vector2 targetPos, UnityAction onComplate = null)
		{
			ClearValues();
			if (onComplate != null) _onComplate.AddListener(onComplate);
			Queue<Vector2> pahtPositions = _pathfindingManager.FindPath(transform.position, targetPos);
			if (pahtPositions != null && pahtPositions.Count > 0)
			{
				path = pahtPositions.ToArray();
				targetPosition = path[^1];
				isMoving = true;
			}
		}
		/// <summary>
		/// Moves the unit along the calculated path towards the next waypoint.
		/// Invokes the completion callback when the target is reached and clears movement data.
		/// </summary>
		private void MoveAlongPath()
		{
			if (currentPathIndex >= path.Length)
			{
				isMoving = false;
				return;
			}

			Vector2 nextPosition = path[currentPathIndex];
			transform.position = Vector2.MoveTowards(transform.position, nextPosition, _speed * Time.deltaTime);
			if (Vector2.Distance(transform.position, nextPosition) < 0.01f)
			{
				currentPathIndex++;
			}

			if (IsAtTarget())
			{
				_onComplate?.Invoke();

				ClearValues();
			}
		}

		/// <summary>
		/// Checks if the unit has reached the target position within the stopping distance.
		/// </summary>
		/// <returns>True if the unit is within the stopping distance of the target, false otherwise.</returns>
		private bool IsAtTarget()
		{
			return Vector2.Distance(transform.position, targetPosition) <= _stoppingDistance;
		}

		/// <summary>
		/// Resets movement-related data, including movement state, event listeners, and path index.
		/// </summary>
		private void ClearValues()
		{
			isMoving = false;
			_onComplate.RemoveAllListeners();
			currentPathIndex = 0;
		}

		/// <summary>
		/// Handles changes in node walkability by recalculating the path if the current path is affected.
		/// </summary>
		/// <param name="changedNode">The node whose walkability has changed.</param>
		private void HandleWalkabilityChanged(Node changedNode)
		{
			if (isMoving && nodePath != null && nodePath.Contains(changedNode) && !changedNode.IsWalkable)
			{
				SetTarget(targetPosition);
			}
		}
	}
}