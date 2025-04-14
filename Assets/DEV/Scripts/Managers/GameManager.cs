using GameInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	private void Start()
	{
		MouseUser.Init();
	}
}
