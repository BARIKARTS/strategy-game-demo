using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePanelController : MonoBehaviour
{

	public abstract void Deactive();

	protected abstract void Subscribe();
	protected abstract void Unsubscribe();

}
