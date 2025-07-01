using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Routine is called when the user clicks the Start button.
    /// </summary>
    public void HandleStartButton()
    {

    }


    /// <summary>
    /// Routine is called when the user clicks the Exit button
    /// </summary>
    public void HandleExitButton()
    {

    }
}
