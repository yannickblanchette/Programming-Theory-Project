using UnityEngine;
using UnityEngine.SceneManagement;
using UtilityScripts;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance { get; private set; }

    public const int sceneNumberTitleScreen = 0;
    public const int sceneNumberGameScreen = 1;
    public const int sceneNumberGameOverScreen = 2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
        GameStateManager.instance.AddEventListener(GameManager.GameStates.OnTitleScreen, GameManager.GameEvents.StartPressed, HandleOnTitleScreenStartPressed);
        GameStateManager.instance.AddEventListener(GameManager.GameStates.InProgress, GameManager.GameEvents.GameOver, HandleInProgressGameOverEvent);
    }


    private void HandleOnTitleScreenStartPressed(int fromState, int appliedEvent, int toState)
    {
        LoadGameScreen();
    }


    private void HandleInProgressGameOverEvent(int fromState, int appliedEvent, int toState)
    {
        LoadGameOverScreen();
    }

}
