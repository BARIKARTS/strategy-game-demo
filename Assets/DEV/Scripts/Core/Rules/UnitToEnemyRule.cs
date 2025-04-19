//public class UnitToEnemyRule : IInteractionRule
//{
//	public bool CanHandle(ISelectable selected, IInteractable clicked)
//		=> selected is Unit && clicked is Unit target && target.IsEnemy;

//	public void Execute(ISelectable selected, IInteractable clicked)
//	{
//		var attacker = (Unit)selected;
//		var enemy = (Unit)clicked;
//		attacker.Attack(enemy);
//	}
//}
