using GameLogic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private const float initialEnemyTime = 2f;
    private const float repeatingEnemyTime = 2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddAllListeners();
    }


    private void InstantiateEnemy()
    {
        float positionY = Random.Range(-GameScreenManager.verticalRange, GameScreenManager.verticalRange);
        Vector3 enemyPosition = new Vector3(GameScreenManager.startPositionX, positionY, GameScreenManager.positionZ);

        Instantiate(enemy, enemyPosition, enemy.transform.rotation);
    }


    private void AddAllListeners()
    {
        GameStateManager.instance.waitingToPlayPlayPressedEvent.AddListener(HandleWaitingToPlayPlayPressedEvent);
    }


    private void HandleWaitingToPlayPlayPressedEvent()
    {
        InvokeRepeating(nameof(InstantiateEnemy), initialEnemyTime, repeatingEnemyTime);
    }
}
