using UnityEngine;

/// <summary>
/// Defines damageable object properties and methods
/// </summary>
public interface IDamageable
{
	/// <summary>
	/// Team ID of the object
	/// </summary>
	byte Team { get; }

	/// <summary>
	/// Current health of the object
	/// </summary>
	float Health { get; }
	/// <summary>
	/// Current position of the object
	/// </summary>
	Vector2 Position { get; }

	/// <summary>
	/// Applies damage to the object
	/// </summary>
	/// <param name="damage">damage power</param>
	void TakeDamage(float damage);
}