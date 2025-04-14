using GameInput;
using UnityEngine;
namespace BuildingSystem
{
	public class BuildingPlacer : MonoBehaviour
	{
		[field: SerializeField] public BuildableItem ActiveBuildable { get; private set; }
		[SerializeField] private ConstructionLayer _constructionLayer;
		private void Update()
		{
			if (MouseUser.IsMouseButtonPressed(MouseButton.Left) && ActiveBuildable != null && _constructionLayer != null)
			{
				Debug.Log(MouseUser.MouseInWorldPosition);	
				_constructionLayer.Build(MouseUser.MouseInWorldPosition, ActiveBuildable);
			}
		}
	}

}
