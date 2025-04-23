using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
	/// <summary>
	/// A component for managing the preview layer of a building in the building system.
	/// Handles activation, deactivation, and updating of the preview sprite and its appearance.
	/// </summary>
	public class PreviewLayer : MonoBehaviour
	{
		[SerializeField] private ContactFilter2D _contactFilter;
		private PolygonCollider2D _collider;
		private SpriteRenderer _spriteRenderer;

		private Collider2D[] _result = new Collider2D[10];
		private void Start()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_collider = GetComponent<PolygonCollider2D>();
		}

		/// <summary>
		/// Activates the preview layer with the specified sprite.
		/// Sets the sprite and makes the game object visible if the sprite is valid.
		/// </summary>
		/// <param name="sprite">The sprite to display in the preview.</param>
		public void ActivePreview(Sprite sprite)
		{
			if (sprite != null)
			{
				_spriteRenderer.sprite = sprite;
				UpdateCollider();
				gameObject.SetActive(true);
			}
		}

		/// <summary>
		/// Deactivates the preview layer by hiding the game object.
		/// </summary>
		public void DeActivePreview()
		{
			gameObject.SetActive(false);
		}

		/// <summary>
		/// Updates the position and color of the preview layer.
		/// Moves the preview to the specified world position and sets the sprite renderer's color.
		/// </summary>
		/// <param name="worldPos">The world position to move the preview to.</param>
		/// <param name="color">The color to apply to the sprite renderer.</param>
		public void UpdatePreview(Vector2 worldPos, Color color)
		{
			transform.position = worldPos;
			_spriteRenderer.color = color;
		}

		public bool CheckSurroundings()
		{
			int count = _collider.OverlapCollider(_contactFilter, _result);
			return count <= 0;
		}
		private void UpdateCollider()
		{

			_collider.pathCount = _spriteRenderer.sprite.GetPhysicsShapeCount();

			List<Vector2> path = new List<Vector2>();
			for (int i = 0; i < _collider.pathCount; i++)
			{
				path.Clear();
				_spriteRenderer.sprite.GetPhysicsShape(i, path);
				_collider.SetPath(i, path.ToArray());
			}
		}
	}
}
