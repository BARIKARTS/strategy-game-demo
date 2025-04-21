
public abstract class UnitCommand
{
	protected StandartUnitController m_controller;
	public UnitCommand(StandartUnitController controller)
	{
		m_controller = controller;
	}
	public abstract void Execute( );
	public abstract bool IsFinished( );
}