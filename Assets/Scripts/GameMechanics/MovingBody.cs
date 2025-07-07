using GameLogic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UtilityScripts;

public abstract class MovingBody : MonoBehaviour
{
    abstract public float horizontalSpeed { get; }
    abstract public float verticalSpeed {  get; }
    abstract public int scoreIncrement { get; }


    protected virtual void Update()
    {
        if (GameStateManager.instance.currentGameState == GameStates.InProgress)
        {
            MoveHorizontally();
            MoveVertically();
        }       
    }


    protected virtual void MoveHorizontally()
    {
        transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
    }


    protected virtual void MoveVertically()
    {
        transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        UtilityScripts.Debugging.instance.DebugLog("OnTriggerEnter own object " + gameObject.name + " other " + other.name);
        Destroy(gameObject);
    }
}
