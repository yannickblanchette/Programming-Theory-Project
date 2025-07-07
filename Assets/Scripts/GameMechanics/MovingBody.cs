using UnityEngine;

public abstract class MovingBody : MonoBehaviour
{
    abstract public float horizontalSpeed { get; }
    abstract public float verticalSpeed {  get; }


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
}
