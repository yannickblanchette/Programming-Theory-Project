using System;
using UnityEngine;
using UtilityScripts;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance { get; private set; }
    
    // Encapsulation
    public GameManager.GameStates currentGameState
    {
        get { return (GameManager.GameStates)gameFSM.GetCurrentState(); }
    }

    private IFSM gameFSM;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        InitializeLocalVariables();
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
    private void InitializeLocalVariables()
    {
        InitializeGameFSM();
    }


    private void InitializeGameFSM()
    {
        gameFSM = new FSM(Enum.GetNames(typeof(GameManager.GameStates)).Length, Enum.GetNames(typeof(GameManager.GameStates)).Length, Debugging.instance);
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.init), (int)GameManager.GameEvents.initCompleted, (int)GameManager.GameStates.OnTitleScreen, this.HandleInitToOnTitleScreenTransition));
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.OnTitleScreen), (int)GameManager.GameEvents.StartPressed, (int)GameManager.GameStates.WaitingToPlay, this.HandleOnTitleScrenToWaitingToPlayTransition));
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.OnTitleScreen), (int)GameManager.GameEvents.ExitPressed, (int)GameManager.GameStates.Exit, this.HandleOnTitleScrenToExitTransition));
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.WaitingToPlay), (int)GameManager.GameEvents.PlayPressed, (int)GameManager.GameStates.InProgress, this.HandleWaitingToPlayToInProgressTransition));
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.InProgress), (int)GameManager.GameEvents.GameOver, (int)GameManager.GameStates.GameOver, this.HandleInProgressToGameOverTransition));
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.GameOver), (int)GameManager.GameEvents.GameOverTimeout, (int)GameManager.GameStates.OnBestScoreScreen, this.HandleGameOverToOnBestScoreScreenTransition));
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.OnBestScoreScreen), (int)GameManager.GameEvents.RestartPressed, (int)GameManager.GameStates.WaitingToPlay, this.HandleOnBestScoreScreenToWaitingToPlayTransition));
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.OnBestScoreScreen), (int)GameManager.GameEvents.BackToTitlePressed, (int)GameManager.GameStates.OnTitleScreen, this.HandleOnBestScoreScreenToOnTitleScreenTransition));
        gameFSM.SetInitialState((int)GameManager.GameStates.init);
    }


    // POLYMORPHISM
    public void ProcessEvent(int gameEvent)
    {
        Debugging.instance.InfoLog("GameStateManager.ProcessEvent " + gameEvent);
        gameFSM.ProcessEvent(gameEvent);
    }


    public void ProcessEvent(GameManager.GameEvents gameEvent)
    {
        this.ProcessEvent((int)gameEvent);
    }



    public void HandleInitToOnTitleScreenTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleInitToOnTitleScreenTransition ");
    }


    public void HandleOnTitleScrenToWaitingToPlayTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleOnTitleScrenToWaitingToPlayTransition ");
    }


    public void HandleOnTitleScrenToExitTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleOnTitleScrenToExitTransition ");
    }


    public void HandleWaitingToPlayToInProgressTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleWaitingToPlayToInProgressTransition ");
    }


    public void HandleInProgressToGameOverTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleInProgressToGameOverTransition ");
    }


    public void HandleGameOverToOnBestScoreScreenTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleInProgressToGameOverTransition ");
    }


    public void HandleOnBestScoreScreenToWaitingToPlayTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleInProgressToGameOverTransition ");
    }


    public void HandleOnBestScoreScreenToOnTitleScreenTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleOnBestScoreScreenToOnTitleScreenTransition ");
    }
}
