using UnityEngine;

public class MoveCommand : UnitCommand
{
	private bool _onComplate = false;
	public MoveCommand(StandartUnitController controller, Vector2 targetPosition) : base(controller)
	{
		//_moveSpeed = 5f;
		controller.UnitPathfinding.GoMove(targetPosition, 5, 1f, Fnish);
	}
	private void Fnish()
	{
		_onComplate = true;
	}
	public override void Execute()
	{
	}

	public override bool IsFinished() => _onComplate;
}