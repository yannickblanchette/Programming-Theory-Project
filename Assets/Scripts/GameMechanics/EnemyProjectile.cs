using UnityEngine;

public class EnemyProjectile : MovingBody
{
    private float m_horizontalSpeed = -10f;
    private float m_verticalSpeed = 0f;
    private int m_scoreIncrement = 0;

    public override float horizontalSpeed { get { return m_horizontalSpeed; } }
    public override float verticalSpeed { get { return m_verticalSpeed; } }
    public override int scoreIncrement { get { return m_scoreIncrement; } }
}
