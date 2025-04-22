using UnityEngine;

public interface IDamageable
{
	float Health { get;  }
	Vector2 Position { get; }
	void TakeDamage(float damage);
}