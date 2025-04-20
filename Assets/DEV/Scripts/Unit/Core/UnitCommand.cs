
public abstract class UnitCommand
{
	protected UnitController m_controller;
	public UnitCommand(UnitController controller)
	{
		m_controller = controller;
	}
	public abstract void Execute( );
	public abstract bool IsFinished( );
}