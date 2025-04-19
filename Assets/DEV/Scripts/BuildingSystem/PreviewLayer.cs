using TMPro;
using UnityEngine;

namespace BuildingSystem
{

	public class PreviewLayer : MonoBehaviour
	{


		private SpriteRenderer _spriteRenderer;
		private void Start()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}
		public void ActivePreview(Sprite sprite)
		{
			if (sprite != null)
			{
				_spriteRenderer.sprite = sprite;
				gameObject.SetActive(true);
			}
		}
		public void DeActivePreview()
		{
			gameObject.SetActive(false);
		}
		public void UpdatePreview(Vector2 worldPos, Color color)
		{
			transform.position = worldPos;
			_spriteRenderer.color = color;
		}

		


	}
}
