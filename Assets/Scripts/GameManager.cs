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

    public const int bestScoreArrayLength = 5;
    public const string emptyName = "";
    public const int invalidScore = -1;


    [System.Serializable]
    public class BestScoreEntry
    {
        public string name { get; set; }
        public int score { get; set; }
    }



    public static GameManager instance { get; private set; }

    public BestScoreEntry[] bestScoreArray { get; private set; }


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
        GameDataManager.instance.SaveGameData();
        GameStateManager.instance.ProcessEvent(GameManager.GameEvents.ExitPressed);
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
