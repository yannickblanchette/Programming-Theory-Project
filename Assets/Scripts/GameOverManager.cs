using System;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance {  get; private set; }
    
    [SerializeField] private TextMeshProUGUI bestScoreListText;


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
        DisplayBestScoreList();
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

}
