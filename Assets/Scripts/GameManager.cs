using UnityEngine;
using UtilityScripts;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

    public const int invalidScore = -1;

    [SerializeField] private TextMeshProUGUI bestScoreText;

    public static GameManager instance { get; private set; }

   
    
    // ENCAPSULATION
    public string playerName
    {
        get { return m_playerName; }
        set 
        { 
            if (!string.IsNullOrEmpty(value))
                m_playerName = value; 
        }
    }
    public int playerScore
    {
        get { return m_playerScore; }
        set 
        { 
            if (value >= 0)
               m_playerScore = value;
        }
    }

    private string m_playerName;
    private int m_playerScore;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        InitializeLocalVariables();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TurnOnAllDebugging();
        DisplayBestScore();
        GameStateManager.instance.ProcessEvent(GameManager.GameEvents.initCompleted);
        Debugging.instance.InfoLog("GameManager.Start completed");
    }


    private void InitializeLocalVariables()
    {
        m_playerName = string.Empty;
        m_playerScore = 0;
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
        //Start the game only if the player name is not empty, otherwise stay on the Title Screen
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            GameStateManager.instance.ProcessEvent(GameManager.GameEvents.StartPressed);
        }       
    }


    /// <summary>
    /// Routine is called when the user clicks the Exit button
    /// </summary>
    public void HandleExitButton()
    {
        GameDataManager.instance.SaveGameData();
        GameStateManager.instance.ProcessEvent(GameManager.GameEvents.ExitPressed);

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }


    public void DisplayBestScore()
    {
        string toDisplay;
        BestScoreManager.BestScoreEntry entry = BestScoreManager.instance.bestScoreArray[0];
        if (string.IsNullOrEmpty(entry.name) || entry.score < 0)
        {
            toDisplay = $"Best score: none yet";
        }
        else
        {
            toDisplay = $"Best score: {entry.name}: {entry.score}";
        }            
    }



}
