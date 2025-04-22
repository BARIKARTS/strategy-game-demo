
public abstract class BaseUnitState
{
	protected BaseUnitController m_controller;
	public BaseUnitState(BaseUnitController controller)
	{
		m_controller = controller;
	}
	public abstract void Update( );
	public abstract bool IsFinished( );
}