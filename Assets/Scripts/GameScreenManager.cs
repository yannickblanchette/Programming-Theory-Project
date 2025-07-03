using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UtilityScripts;

public class GameScreenManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int playerScore;


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

    }


    private void InitiliazeLocalVariables()
    {
        playerScore = 0;
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
        scoreText.text = $"Score: {GameManager.instance.playerName}: {playerScore}" ;
    }


    public void IncrementScore(int score)
    {
        playerScore += score;
        UpdateScoreText();
    }
}
