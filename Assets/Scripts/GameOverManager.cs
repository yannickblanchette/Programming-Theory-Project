using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance {  get; private set; }
    
    [SerializeField] private TextMeshProUGUI bestScoreListText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject gameOverObject;
    [SerializeField] private GameObject bestScoreListObject;
    [SerializeField] private GameObject restartObject;
    [SerializeField] private GameObject backToMainObject;

    public const float gameOverTimeout = 2f;

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
        DisplayGameOverText();
        StartCoroutine(GameOverTimer());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    
    // ABSTRACTION
    private void DisplayBestScoreList()
    {
        //First display the title of the table
        string toDisplay = "Best Scores";
        
        //Display each best score in the list if a best score exists
        for (int i = 0; i < BestScoreManager.bestScoreArrayLength; i++)
        {
            toDisplay += Environment.NewLine;

            //Check if the entry is suitable to be displayed
            BestScoreManager.BestScoreEntry entry = BestScoreManager.instance.bestScoreArray[i];
            if (!string.IsNullOrEmpty(entry.name) && entry.score >= 0)
            {
                toDisplay += $"{i}: {entry.name} : {entry.score}";
            }
        }

        bestScoreListText.text = toDisplay;
    }


    public void HandleRestartButtonClicked()
    {
        GameStateManager.instance.ProcessEvent(GameManager.GameEvents.RestartPressed);
    }


    public void HandleBackToMainButtonClicked()
    {
        GameStateManager.instance.ProcessEvent(GameManager.GameEvents.BackToTitlePressed);
    }


    private void DisableGameOver()
    {
        gameOverObject.SetActive(false);
    }


    private void EnableBestScore()
    {
        bestScoreListObject.SetActive(true);
        restartObject.SetActive(true);
        backToMainObject.SetActive(true);
    }


    private IEnumerator GameOverTimer()
    {
        yield return new WaitForSeconds(gameOverTimeout);
        DisableGameOver();
        EnableBestScore();
        DisplayBestScoreList();
    }


    private void DisplayGameOverText()
    {
        string toDisplay = "GAME OVER" + Environment.NewLine;
        toDisplay += $"YOUR SCORE: {GameManager.instance.playerScore}";
        gameOverText.text = toDisplay;
    }
}
