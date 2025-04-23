using GameInput;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;
using System.Collections;

namespace BuildingSystem
{
	public class BuildingPlacer : SingletonMonoBehaviour<BuildingPlacer>
	{
		[SerializeField] private Color _validPlacementColor;
		[SerializeField] private Color _invalidPlacementColor;
		[SerializeField] private Tilemap _baseTileMap;
		[SerializeField] private PreviewLayer _previewLayer;
		[SerializeField] private ContactFilter2D _contactFilter2D;

		private Coroutine _coroutine;
		private BuildingType _currentType;
		private BaseBuildingData _activeBuildable;
		private CommonData _commonData => CommonData.Instance;
		private FactoryManager _factoryManager => FactoryManager.Instance;
		private void Start()
		{
			if (_previewLayer != null) _previewLayer.DeActivePreview();
		}
		public void Active(BuildingType buildingType)
		{
			if (_previewLayer == null || _baseTileMap == null) return;
			if (_coroutine != null) StopCoroutine(_coroutine);
			if (_commonData.TryGetBuildingData(buildingType, out BaseBuildingData data))
			{
				_activeBuildable = data;
				_currentType = buildingType;
				_previewLayer.ActivePreview(_activeBuildable.PreviewSprite);
				_coroutine = StartCoroutine(UpdateLayer_Coroutine());
			}
			else
			{
				Debug.LogError($"{buildingType} is null");
			}

		}

		public void Deactive()
		{
			if (_coroutine != null) StopCoroutine(_coroutine);
			if (_previewLayer != null) _previewLayer.DeActivePreview();
			_currentType = BuildingType.None;
		}

		private IEnumerator UpdateLayer_Coroutine()
		{
			Color previewColor;
			bool canBuild;
			Vector2 mouseWorldPos;
			while (true)
			{
				mouseWorldPos = MouseUser.MouseInWorldPosition;
				canBuild = _previewLayer.CheckSurroundings();
				previewColor = (canBuild ? _validPlacementColor : _invalidPlacementColor);
				_previewLayer.UpdatePreview(mouseWorldPos, previewColor);
				if (MouseUser.IsMouseButtonPressed(MouseButton.Left) && _activeBuildable != null && canBuild)
				{
					_ = _factoryManager.BuildingSpawn(_currentType, mouseWorldPos);
					Deactive();

				}
				yield return null;
			}
		}




	}

}
