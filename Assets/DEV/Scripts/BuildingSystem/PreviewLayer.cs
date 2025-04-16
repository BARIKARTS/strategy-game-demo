using BuildingSystem.Models;
using UnityEngine;

namespace BuildingSystem
{

	public class PreviewLayer : MonoBehaviour
	{
		[SerializeField] private Color _validPlacementColor;
		[SerializeField] private Color _invalidPlacementColor;

		private SpriteRenderer _spriteRenderer;
		public bool CanBuild => CheckSurroundings();
		private void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}
		public void ActivePreview(Sprite sprite)
		{
			if (sprite != null)
			{
				_spriteRenderer.sprite = sprite;
				enabled = true;
			}
		}
		public void DeActivePreview()
		{
			enabled = false;
		}
		public void UpdatePreview(Vector2 worldPos)
		{
			transform.position = worldPos;
			_spriteRenderer.color = (CanBuild ? _validPlacementColor : _invalidPlacementColor);
		}

		public bool CheckSurroundings()
		{

			Vector2 spriteSize = _spriteRenderer.sprite.bounds.size;

			Vector2 boxSize = spriteSize;

			Vector2 boxCenter = transform.position + _spriteRenderer.sprite.bounds.center;

			Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 10f);

			if (hit != null) Debug.Log($"Tespit edilen nesne: {hit.gameObject.name}");
			return hit == null;
		}


	}
}
