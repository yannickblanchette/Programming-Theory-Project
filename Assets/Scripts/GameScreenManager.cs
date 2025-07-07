using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UtilityScripts;


namespace GameLogic
{
    public class GameScreenManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI bestScoreText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI instructionsText;
        [SerializeField] private GameObject instructionsArea;
        [SerializeField] private GameObject projectile;
        

        private int playerScore;
        private int numProjectiles;


        private void Awake()
        {
            InitiliazeLocalVariables();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
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


        private void InitiliazeLocalVariables()
        {
            playerScore = 0;
            numProjectiles = 0;
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
            scoreText.text = $"Score: {GameManager.instance.playerName}: {playerScore}";
        }


        public void IncrementScore(int score)
        {
            playerScore += score;
            UpdateScoreText();
        }


        private void HandleWaitingToPlayState()
        {
            if (KeyboardManager.instance.IsSpacebarPressed())
            {
                UpdateIntructionsText();
                GameStateManager.instance.ProcessEvent(GameEvents.PlayPressed);
            }
        }


        private void UpdateIntructionsText()
        {
            instructionsText.text = "GAME IS IN PROGRESS";
        }


        private void HandleInProgressState()
        {
            if (KeyboardManager.instance.IsSpacebarPressed())
            {
                if (instructionsArea.activeSelf)
                {
                    instructionsArea.SetActive(false);
                    CreateProjectile();
                }
                else
                {
                    if (numProjectiles >= 5)
                    {
                        int randomScore = Random.Range(1, 100);
                        GameManager.instance.playerScore = randomScore;
                        BestScoreManager.instance.CompareScoreWithBestScores(GameManager.instance.playerName, GameManager.instance.playerScore);
                        GameStateManager.instance.ProcessEvent(GameEvents.GameOver);
                    }
                }
            }
        }

    
        private void CreateProjectile()
        {
            Instantiate(projectile, projectile.transform.position, projectile.transform.rotation);
            numProjectiles++;
        }
    
    }

}

