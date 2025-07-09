using System.Collections;
using GameLogic;
using UnityEngine;

public class Enemy : MovingBody
{
    [SerializeField] private GameObject projectile;

    private float m_horizontalSpeed;
    private float m_verticalSpeed = 0f;
    private int m_scoreIncrement;
    private float initialPositionDeltaX = -1.3f;
    private float initialPositionZ = -0.2f;
    private float minProjectileInterval = 0.7f;
    private float maxProjectileInterval = 2.2f;

    private const string prefabNameEnemySmall = "EnemySmall";
    private const string prefabNameEnemyMid = "EnemyMid";
    private const string prefabNameEnemyLarge = "EnemyLarge";
    private const float m_horizontalSpeedEnemySmall = -5f;
    private const float m_horizontalSpeedEnemyMid = -4f;
    private const float m_horizontalSpeedEnemyLarge = -3f;
    private const int m_scoreIncrementEnemySmall = 5;
    private const int m_scoreIncrementEnemyMid = 3;
    private const int m_scoreIncrementEnemyLarge = 1;

    public override float horizontalSpeed { get { return m_horizontalSpeed; } }
    public override float verticalSpeed { get { return m_verticalSpeed; } }
    public override int scoreIncrement { get { return m_scoreIncrement; } }


    private void Awake()
    {
        if (gameObject.name.StartsWith(prefabNameEnemySmall))
        {
            m_horizontalSpeed = m_horizontalSpeedEnemySmall;
            m_scoreIncrement = m_scoreIncrementEnemySmall;
        }
        else if (gameObject.name.StartsWith(prefabNameEnemyLarge))
        {
            m_horizontalSpeed = m_horizontalSpeedEnemyLarge;
            m_scoreIncrement = m_scoreIncrementEnemyLarge;
        }
        else
        {
            m_horizontalSpeed = m_horizontalSpeedEnemyMid;
            m_scoreIncrement = m_scoreIncrementEnemyMid;
        }
    }


    private void Start()
    {
        StartCoroutine(nameof(HandleProjectileIntervalTimer));
    }


    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("OutOfBound"))
        {
            //When hitting the player or OutOfBound, do not increment the score
            base.OnTriggerEnter(other);
        }
        else
        {
            GameScreenManager.instance.IncrementScore(scoreIncrement);
            base.OnTriggerEnter(other);
        }
    }


    private void CreateProjectile()
    {
        float positionX = gameObject.transform.position.x + initialPositionDeltaX;
        Vector3 projectilePosition = new Vector3(positionX, gameObject.transform.position.y, initialPositionZ);

        Instantiate(projectile, projectilePosition, projectile.transform.rotation);
    }


    private IEnumerator HandleProjectileIntervalTimer()
    {
        yield return new WaitForSeconds(GenerateRandomProjectileInterval());
        CreateProjectile();
        StartCoroutine(nameof(HandleProjectileIntervalTimer));
    }


    private float GenerateRandomProjectileInterval()
    {
        return Random.Range(minProjectileInterval, maxProjectileInterval);
    }
}
