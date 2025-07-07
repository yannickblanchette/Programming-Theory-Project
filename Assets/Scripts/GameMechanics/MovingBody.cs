using GameLogic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MovingBody : MonoBehaviour
{
    abstract public float horizontalSpeed { get; }
    abstract public float verticalSpeed {  get; }
    abstract public int scoreIncrement { get; }


    protected void Update()
    {
        MoveHorizontally();
        MoveVertically();        
    }


    protected virtual void MoveHorizontally()
    {
        transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
    }

    protected virtual void MoveVertically()
    {
        transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);
    }


    protected void OnTriggerEnter(Collider other)
    {
        //Check if the projectile hit an enemy or the player
        if (other.CompareTag("Enemy"))
        {
            GameManager.instance.playerScore += other.GetComponent<MovingBody>().scoreIncrement;
        }
        else if (other.CompareTag("Player"))
        {
            //Player has been hit so it is Game Over
            GameStateManager.instance.ProcessEvent(GameEvents.GameOver);
        }
        //Destroy the object that has been hit, as well as our own object
        Destroy(gameObject);
    }
}
