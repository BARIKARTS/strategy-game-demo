using GameInput;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	//[SerializeField] private EnemyController _enemyController; //to create enemies
	private FactoryManager _factoryManager => FactoryManager.Instance;
	private void Start()
	{
		MouseUser.Init();
		_factoryManager.Initialize();
		//_enemyController.Initialize(); // to create enemies
	}


}
