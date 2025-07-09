using UnityEngine;
using UnityEngine.SceneManagement;
using UtilityScripts;


namespace GameLogic
{
    public class ScreenManager : MonoBehaviour
    {
        // ENCAPSULATION
        public static ScreenManager instance { get; private set; }

        public const int sceneNumberTitleScreen = 0;
        public const int sceneNumberGameScreen = 1;
        public const int sceneNumberGameOverScreen = 2;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            // end of new code

            instance = this;
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            AddEventListeners();
            Debugging.instance.InfoLog("ScreenManager.Start completed");
        }

        // Update is called once per frame
        void Update()
        {

        }

        // ABSTRACTION
        /// <summary>
        /// Loads the Game Screen scene
        /// </summary>
        public void LoadGameScreen()
        {
            SceneManager.LoadScene(sceneNumberGameScreen);
        }


        /// <summary>
        /// Loads the Title Screen scene
        /// </summary>
        public void LoadTitleScreen()
        {
            SceneManager.LoadScene(sceneNumberTitleScreen);
        }


        public void LoadGameOverScreen()
        {
            SceneManager.LoadScene(sceneNumberGameOverScreen);
        }


        private void AddEventListeners()
        {
            GameStateManager.instance.onTitleScreenStartPressedEvent.AddListener(HandleOnTitleScreenStartPressed);
            GameStateManager.instance.inProgressGameOverEvent.AddListener(HandleInProgressGameOverEvent);
            GameStateManager.instance.gameOverRestartPressedEvent.AddListener(HandleGameOverRestartPressedEvent);
            GameStateManager.instance.gameOverBackToTitlePressedEvent.AddListener(HandleGameOverBackToTitlePressedEvent);
        }


        private void HandleOnTitleScreenStartPressed()
        {
            LoadGameScreen();
        }


        private void HandleInProgressGameOverEvent()
        {
            LoadGameOverScreen();
        }


        private void HandleGameOverRestartPressedEvent()
        {
            LoadGameScreen();
        }


        private void HandleGameOverBackToTitlePressedEvent()
        {
            LoadTitleScreen();
        }
    }

}

