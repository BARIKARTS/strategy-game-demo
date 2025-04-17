using GameInput;
using Pathfinding;
using Pathfinding.Models;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{

	private void Start()
	{
		MouseUser.Init();
	}

	
}
