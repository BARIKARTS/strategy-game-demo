using System.Collections.Generic;
using UnityEngine;
using Pathfinding.Models;
using UnityEngine.Events;
using System;
using UnityEditor;

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

		private AstarPathfinding pathfinding;
		private CustomGrid grid;
		[SerializeField] private Vector2 _targetPosTest;
		public FnishEvent _onComplate = new FnishEvent();
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
			ClearValues();
		}

		void Update()
		{
			if (isMoving)
			{
				Debug.Log("moving");
				MoveAlongPath();
			}
		}
		[ContextMenu(nameof(TEtsTarget))]
		public void TEtsTarget() => SetTarget(_targetPosTest);
		public void GoMove(Vector2 targetPos, float speed, float stoppingDistance, UnityAction onComplate = null)
		{
			_speed = speed;
			_stoppingDistance = stoppingDistance;
			SetTarget(targetPos,onComplate);
		}
		public void SetTarget(Vector2 targetPos,UnityAction onComplate=null)
		{
			ClearValues();
			if(onComplate != null) _onComplate.AddListener(onComplate);
			Debug.LogError(989);
			targetPosition = targetPos;
			path = pathfinding.FindPath(transform.position, targetPosition).ToArray();
			isMoving = true;
		}

		private void a()
		{
			Debug.Log("a");

		}
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

		private bool IsAtTarget()
		{
			return Vector2.Distance(transform.position, targetPosition) <= _stoppingDistance;
		}
		private void ClearValues()
		{
			isMoving = false;
			_onComplate.RemoveAllListeners();
			currentPathIndex = 0;
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