using UnityEngine;
using UtilityScripts;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        init,
        OnTitleScreen,
        WaitingToPlay,
        InProgress,
        GameOver,
        OnBestScoreScreen,
        Exit
    }

    public enum GameEvents
    {
        initCompleted,
        StartPressed,
        ExitPressed,
        PlayPressed,
        GameOver,
        GameOverTimeout,
        RestartPressed,
        BackToTitlePressed
    }


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
        TurnOnAllDebugging();
        GameStateManager.instance.ProcessEvent(GameManager.GameEvents.initCompleted);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void InitializeLocalVariables()
    {
        
    }


    private void TurnOnAllDebugging()
    {
        Debugging.instance.TurnOnErrorLogging();
        Debugging.instance.TurnOnWarningLogging();
        Debugging.instance.TurnOnDebugLogging();
        Debugging.instance.TurnOnInfoLogging();
    }


    /// <summary>
    /// Routine is called when the user clicks the Start button.
    /// </summary>
    public void HandleStartButton()
    {
        GameStateManager.instance.ProcessEvent(GameManager.GameEvents.StartPressed);
    }


    /// <summary>
    /// Routine is called when the user clicks the Exit button
    /// </summary>
    public void HandleExitButton()
    {
        GameStateManager.instance.ProcessEvent(GameManager.GameEvents.ExitPressed);
    }
}
