using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    public const int sceneNumberTitleScreen = 0;
    public const int sceneNumberGameScreen = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
