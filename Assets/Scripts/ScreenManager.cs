using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    public const int screenNumberTitleScreen = 0;
    public const int screenNumberGameScreen = 1;

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
}
