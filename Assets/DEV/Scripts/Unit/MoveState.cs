using UnityEngine;

public class MoveState : BaseUnitState
{
	private bool _onComplate = false;
	public MoveState(BaseUnitController controller, Vector2 targetPosition) : base(controller)
	{
		//_moveSpeed = 5f;
		controller.UnitPathfinding.GoMove(targetPosition, 5, 1f, Fnish);
	}
	private void Fnish()
	{
		_onComplate = true;
	}
	public override void Update()
	{
	}

	public override bool IsFinished() => _onComplate;
}