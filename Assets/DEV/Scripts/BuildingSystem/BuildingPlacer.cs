using GameInput;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;
using System.Collections;

namespace BuildingSystem
{
	public class BuildingPlacer : MonoBehaviour
	{
		[SerializeField] private Color _validPlacementColor;
		[SerializeField] private Color _invalidPlacementColor;
		[field: SerializeField] public BaseBuildingData ActiveBuildable { get; private set; }
		[SerializeField] private Tilemap _baseTileMap;
		[SerializeField] private PreviewLayer _previewLayer;
		private Coroutine _coroutine;

		private void Start()
		{
			if (_previewLayer != null) _previewLayer.DeActivePreview();
		}
		public void Active()
		{
			if (_previewLayer == null || _baseTileMap == null) return;
			if (_coroutine != null) StopCoroutine(_coroutine);
			_previewLayer.ActivePreview(ActiveBuildable.PreviewSprite);
			_coroutine = StartCoroutine(UpdateLayer_Coroutine());

		}

		public void Deactive()
		{
			if (_coroutine != null) StopCoroutine(_coroutine);
			if (_previewLayer != null) _previewLayer.DeActivePreview();

		}

		private IEnumerator UpdateLayer_Coroutine()
		{
			Color previewColor;
			bool canBuild;
			Vector2 mouseWorldPos;
			while (true)
			{
				mouseWorldPos = MouseUser.MouseInWorldPosition;
				canBuild = CheckSurroundings();
				previewColor = (canBuild ? _validPlacementColor : _invalidPlacementColor);
				_previewLayer.UpdatePreview(mouseWorldPos, previewColor);
				//Vector2 newPreviewPos = GetNewPreviewPosition(mouseWorldPos);
				//bool isEmpty = _constructionLayer.IsEmpty(newPreviewPos);
				if (MouseUser.IsMouseButtonPressed(MouseButton.Left) && ActiveBuildable != null && canBuild)
				{
					Deactive();
					GameObject createObj = Instantiate(ActiveBuildable.Prefab, mouseWorldPos, Quaternion.identity);
					AstarPathfindingManager.Instance.PlaceStructure(createObj);
					BarracksData barracksData = new BarracksData();
					createObj.GetComponent<BarracksController>().Initialize(new BarracksData());
				}
				yield return null;
			}
		}

		public bool CheckSurroundings()
		{

			Vector2 spriteSize = ActiveBuildable.PreviewSprite.bounds.size;

			Vector2 boxSize = spriteSize;

			Vector2 boxCenter = _previewLayer.transform.position + ActiveBuildable.PreviewSprite.bounds.center;
			Debug.Log(ActiveBuildable.PreviewSprite.bounds.center);

			Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 10f);

			if (hit != null) Debug.Log($"Tespit edilen nesne: {hit.gameObject.name}");
			return hit == null;
		}


	}

}
