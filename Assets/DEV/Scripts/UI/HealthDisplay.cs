using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
	[SerializeField] private Image _healthFillImage;
	[SerializeField] private TextMeshProUGUI _healthText;
	public void UpdateHealth(float maxHealth, float health)
	{
		float fill = Mathf.Clamp01(health / maxHealth);
		_healthFillImage.fillAmount = fill;
		_healthText.text = $"{(int)health}";
	}
}
