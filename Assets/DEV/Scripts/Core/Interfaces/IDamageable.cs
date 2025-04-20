using UnityEngine;

public interface IDamageable
{
	float Health { get;  }
	Vector2 GetPosition();
	void TakeDamage(float damage);
}