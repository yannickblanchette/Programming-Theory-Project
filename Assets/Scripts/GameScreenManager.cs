using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UtilityScripts;


namespace GameLogic
{
    public class GameScreenManager : MonoBehaviour
    {
        public static GameScreenManager instance {  get; private set; }
        
        [SerializeField] private TextMeshProUGUI bestScoreText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI instructionsText;
        [SerializeField] private GameObject instructionsArea;
        [SerializeField] private GameObject player;

        public const float verticalRange = 3.8f;
        public const float startPositionX = 8.3f;
        public const float positionZ = -0.2f;


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            // end of new code

            instance = this;
            InitiliazeLocalVariables();
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GameManager.instance.playerScore = 0;
            AddAllListeners();
            UpdateBestScoreText();
            UpdateScoreText();
            Debugging.instance.InfoLog("GameScreenManager.Start completed");
        }


        // Update is called once per frame
        void Update()
        {
            GameStates gameState = GameStateManager.instance.currentGameState;

            switch (gameState)
            {
                case GameStates.WaitingToPlay:
                    HandleWaitingToPlayState();
                    break;
                case GameStates.InProgress:
                    HandleInProgressState();
                    break;
                default:
                    break;
            }
        }


        // ABSTRACTION
        public void IncrementScore(int score)
        {
            GameManager.instance.playerScore += score;
            UpdateScoreText();
        }


        public static void HandleGameOver()
        {
            BestScoreManager.instance.CompareScoreWithBestScores(GameManager.instance.playerName, GameManager.instance.playerScore);
            GameStateManager.instance.ProcessEvent(GameEvents.GameOver);
        }


        private void InitiliazeLocalVariables()
        {

        }


        private void UpdateBestScoreText()
        {
            //The best score is the entry at index 0 of the best score array
            BestScoreManager.BestScoreEntry entry = BestScoreManager.instance.bestScoreArray[0];
            string name = entry.name;
            int bestScore = entry.score;

            if (string.IsNullOrEmpty(name))
            {
                name = "no one yet";
                bestScore = 0;
            }
            bestScoreText.text = $"Best Score: {name}: {bestScore}";
        }


        private void UpdateScoreText()
        {
            scoreText.text = $"Score: {GameManager.instance.playerName}: {GameManager.instance.playerScore}";
        }


        private void HandleWaitingToPlayState()
        {
            if (KeyboardManager.instance.IsSpacebarPressed())
            {
                if (instructionsArea.activeSelf)
                {
                    instructionsArea.SetActive(false);
                }
                GameStateManager.instance.ProcessEvent(GameEvents.PlayPressed);
            }
        }


        private void HandleInProgressState()
        {

        }


        private void AddAllListeners()
        {
            GameStateManager.instance.waitingToPlayPlayPressedEvent.AddListener(HandleWaitingToPlayPlayPressedEvent);
        }


        private void HandleWaitingToPlayPlayPressedEvent()
        {
            //Enable the player object
            player.SetActive(true);
        }

    }

}

