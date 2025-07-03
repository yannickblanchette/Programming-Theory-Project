using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameScreenManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int playerScore;

    private const string bestScoreTextBase = "Best Score: {name}: {score}";
    private const string scoreTextBase = "Score: {name}: {score}";


    private void Awake()
    {
        InitiliazeLocalVariables();
        UpdateScoreText();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateBestScoreText();
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
        bestScoreText.text = $"Best Score: Yannick: 9";
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
