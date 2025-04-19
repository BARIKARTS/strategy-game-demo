using UnityEngine;

public abstract class BasePanelController<T1, T2> : MonoBehaviour where T1 : BaseBuildingData where T2 : BaseBuildingDynamicData
{
	protected T2 _dynamicData;
	public abstract void Active(T1 baseData, T2 dynamicData);
	public abstract void Deactive();

	public abstract void Subscribe();
	public abstract void Unsubscribe();

}
