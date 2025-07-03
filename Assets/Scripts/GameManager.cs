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

    public const int bestScoreArrayLength = 5;
    public const int invalidScore = -1;





    [System.Serializable]
    public class BestScoreEntry
    {
        public string name { get; set; }
        public int score { get; set; }
    }
    public static GameManager instance { get; private set; }
    public BestScoreEntry[] bestScoreArray { get; private set; }
   
    
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


    private string m_playerName;

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
        FillBestScoreArray();   
        GameStateManager.instance.ProcessEvent(GameManager.GameEvents.initCompleted);
    }


    private void InitializeLocalVariables()
    {
        m_playerName = string.Empty;
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


    private void FillBestScoreArray()
    {
        bestScoreArray = new BestScoreEntry[bestScoreArrayLength];
        for (int i = 0; i < bestScoreArrayLength; i++)
        {
            BestScoreEntry entry = new BestScoreEntry();

            entry.name = GameDataManager.instance.saveData.bestScoreArray[i].name;
            entry.score = GameDataManager.instance.saveData.bestScoreArray[i].score;
            bestScoreArray[i] = entry;
        }
    }



}
