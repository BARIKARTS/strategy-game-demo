using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/// <summary>
/// A UI component for displaying production-related information and handling user interactions.
/// Manages the display of a name, icon, and button with a clickable action.
/// </summary>
public class ProductionUIElemet : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _nameTMP;
	[SerializeField] private Image _iconImage;
	[SerializeField] private Button _button;

	/// <summary>
	/// Initializes the UI element with a name, icon, and click action.
	/// Sets the text, sprite, and button listener, then activates the game object.
	/// </summary>
	/// <param name="name">The name to display on the UI element.</param>
	/// <param name="icon">The sprite to display as the icon.</param>
	/// <param name="onClickAction">The action to invoke when the button is clicked.</param>
	public void Initialize(string name, Sprite icon, UnityAction onClickAction)
	{
		_nameTMP.text = name;
		_iconImage.sprite = icon;
		if (_button != null && onClickAction != null) _button.onClick.AddListener(onClickAction);
		gameObject.SetActive(true);
	}

	/// <summary>
	/// Hides the UI element and removes all button click listeners.
	/// </summary>
	public void Hide()
	{
		gameObject.SetActive(false);
		_button.onClick.RemoveAllListeners();
	}
}
