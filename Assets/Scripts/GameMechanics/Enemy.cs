using GameLogic;
using UnityEngine;

public class Enemy : MovingBody
{   
    private float m_horizontalSpeed = -5f;
    private float m_verticalSpeed = 0f;
    private int m_scoreIncrement = 3;


    public override float horizontalSpeed { get { return m_horizontalSpeed; } }
    public override float verticalSpeed { get { return m_verticalSpeed; } }
    public override int scoreIncrement { get { return m_scoreIncrement; } }


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
}
