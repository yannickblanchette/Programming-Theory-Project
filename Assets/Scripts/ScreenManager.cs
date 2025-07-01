using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance { get; private set; }

    public const int sceneNumberTitleScreen = 0;
    public const int sceneNumberGameScreen = 1;

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
        AddEventListeners();
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


    private void AddEventListeners()
    {
        GameStateManager.instance.AddEventListener(GameManager.GameStates.OnTitleScreen, GameManager.GameEvents.StartPressed, HandleOnTitleScreenStartPressed);
    }


    private void HandleOnTitleScreenStartPressed(int fromState, int appliedEvent, int toState)
    {
        LoadGameScreen();
    }



}
