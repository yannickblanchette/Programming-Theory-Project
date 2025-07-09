using UnityEngine;
using UtilityScripts;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace GameLogic
{
    public enum GameStates
    {
        init,
        OnTitleScreen,
        WaitingToPlay,
        InProgress,
        GameOver,
        Exit
    }

    public enum GameEvents
    {
        initCompleted,
        StartPressed,
        ExitPressed,
        PlayPressed,
        GameOver,
        RestartPressed,
        BackToTitlePressed
    }


    public class GameManager : MonoBehaviour
    {
        public const int invalidScore = -1;
        public const string canvasName = "Canvas";
        public const string bestScoreTextName = "BestScoreText";
        public const string startButtonName = "StartButton";
        public const string exitButtonName = "ExitButton";

        public static GameManager instance { get; private set; }

        private TextMeshProUGUI bestScoreText;



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
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            // end of new code

            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeLocalVariables();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }


        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == ScreenManager.sceneNumberTitleScreen)
            {
                Debugging.instance.DebugLog("GameManager.OnSceneLoaded: " + scene.name + " mode " + mode);
                InitializeGameObjectReferences();
                AddAllListeners();
                DisplayBestScore();
                GameStateManager.instance.ProcessEvent(GameEvents.initCompleted);
            }
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }


        private void InitializeLocalVariables()
        {
            m_playerName = string.Empty;
            m_playerScore = 0;
        }


        // ABSTRACTION
        /// <summary>
        /// Routine is called when the user clicks the Start button.
        /// </summary>
        public void HandleStartButton()
        {
            //Start the game only if the player name is not empty, otherwise stay on the Title Screen
            if (!string.IsNullOrWhiteSpace(this.m_playerName))
            {
                GameStateManager.instance.ProcessEvent(GameEvents.StartPressed);
            }
        }


        /// <summary>
        /// Routine is called when the user clicks the Exit button
        /// </summary>
        public void HandleExitButton()
        {
            GameDataManager.instance.SaveGameData();
            GameStateManager.instance.ProcessEvent(GameEvents.ExitPressed);

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

            bestScoreText.text = toDisplay;
        }


        private void InitializeGameObjectReferences()
        {
            bestScoreText = GameObject.Find(bestScoreTextName).GetComponent<TextMeshProUGUI>();
        }


        private void AddAllListeners()
        {
            GameObject.Find(startButtonName).GetComponent<Button>().onClick.AddListener(HandleStartButton);
            GameObject.Find(exitButtonName).GetComponent<Button>().onClick.AddListener(HandleExitButton);
        }
    }

}

