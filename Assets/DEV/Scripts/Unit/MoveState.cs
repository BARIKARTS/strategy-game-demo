using UnityEngine;

/// <summary>
/// Manages unit movement state
/// </summary>
public class MoveState : BaseUnitState
{
	private bool _onComplate = false;
	public MoveState(BaseUnitController controller, Vector2 targetPosition, float speed) : base(controller)
	{
		controller.UnitPathfinding.GoMove(targetPosition, speed, 1f, Fnish);
	}
	private void Fnish()
	{
		_onComplate = true;
	}
	public override void Update()
	{
	}

	/// <summary>
	/// Checks if movement is finished
	/// </summary>
	/// <returns>status</returns>
	public override bool IsFinished() => _onComplate;
}