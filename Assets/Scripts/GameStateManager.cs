using System;
using System.Collections.Generic;
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
    private List<FSMTransition.EventHandler>[,] eventListeners;


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
        InitializeLocalVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // ABSTRACTION
    private void InitializeLocalVariables()
    {
        InitializeGameFSM();
        InitializeEventListeners();
    }


    private void InitializeGameFSM()
    {
        gameFSM = new FSM(Enum.GetNames(typeof(GameManager.GameStates)).Length, Enum.GetNames(typeof(GameManager.GameEvents)).Length, Debugging.instance);
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.init), (int)GameManager.GameEvents.initCompleted, (int)GameManager.GameStates.OnTitleScreen, this.HandleInitToOnTitleScreenTransition));
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.OnTitleScreen), (int)GameManager.GameEvents.StartPressed, (int)GameManager.GameStates.WaitingToPlay, this.HandleOnTitleScreenToWaitingToPlayTransition));
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.OnTitleScreen), (int)GameManager.GameEvents.ExitPressed, (int)GameManager.GameStates.Exit, this.HandleOnTitleScreenToExitTransition));
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.WaitingToPlay), (int)GameManager.GameEvents.PlayPressed, (int)GameManager.GameStates.InProgress, this.HandleWaitingToPlayToInProgressTransition));
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.InProgress), (int)GameManager.GameEvents.GameOver, (int)GameManager.GameStates.GameOver, this.HandleInProgressToGameOverTransition));
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.GameOver), (int)GameManager.GameEvents.RestartPressed, (int)GameManager.GameStates.WaitingToPlay, this.HandleOnBestScoreScreenToWaitingToPlayTransition));
        gameFSM.InsertTransition(new(((int)GameManager.GameStates.GameOver), (int)GameManager.GameEvents.BackToTitlePressed, (int)GameManager.GameStates.init, this.HandleOnBestScoreScreenToInitTransition));
        gameFSM.SetInitialState((int)GameManager.GameStates.init);
    }


    private void InitializeEventListeners()
    {
        eventListeners = new List<FSMTransition.EventHandler>[Enum.GetNames(typeof(GameManager.GameStates)).Length, Enum.GetNames(typeof(GameManager.GameEvents)).Length];
        //initialize array with HandleInvalidEvent
        for (int outer = 0; outer < Enum.GetNames(typeof(GameManager.GameStates)).Length; outer++)
        {
            for (int inner = 0; inner < Enum.GetNames(typeof(GameManager.GameEvents)).Length; inner++)
            {
                eventListeners[outer, inner] = new List<FSMTransition.EventHandler>();
            }
        }
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


    /// <summary>
    /// Add the passed in event listener to the list of event listener that will be executed when this game transition happens
    /// </summary>
    /// <param name="gameState">the actual game state at the time of the event</param>
    /// <param name="gameEvent">the actual event that occured</param>
    /// <param name="eventListener">the event listener to be called on the transition</param>
    public void AddEventListener(GameManager.GameStates gameState, GameManager.GameEvents gameEvent, FSMTransition.EventHandler eventListener)
    {
        eventListeners[(int)gameState, (int)gameEvent].Add(eventListener);
    }


    /// <summary>
    /// Go through the list of previously added event listener and execute each event listener
    /// </summary>
    /// <param name="fromState"></param>
    /// <param name="appliedEvent"></param>
    /// <param name="toState"></param>
    private void ExecuteEventHandler(int fromState, int appliedEvent, int toState)
    {
        List<FSMTransition.EventHandler> listeners = eventListeners[fromState, appliedEvent];
        //Go through the list of eventListeners if there is at least one
        if (listeners.Count >= 1)
        {
            foreach (FSMTransition.EventHandler listener in listeners)
            {
                listener(fromState, appliedEvent, toState);
            }
        }
    }


    public void HandleInitToOnTitleScreenTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleInitToOnTitleScreenTransition ");
        ExecuteEventHandler(fromState, appliedEvent, toState);
    }


    public void HandleOnTitleScreenToWaitingToPlayTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleOnTitleScrenToWaitingToPlayTransition ");
        ExecuteEventHandler(fromState, appliedEvent, toState);
    }


    public void HandleOnTitleScreenToExitTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleOnTitleScrenToExitTransition ");
        ExecuteEventHandler(fromState, appliedEvent, toState);
    }


    public void HandleWaitingToPlayToInProgressTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleWaitingToPlayToInProgressTransition ");
        ExecuteEventHandler(fromState, appliedEvent, toState);
    }


    public void HandleInProgressToGameOverTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleInProgressToGameOverTransition ");
        ExecuteEventHandler(fromState, appliedEvent, toState);
    }


    public void HandleOnBestScoreScreenToWaitingToPlayTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleInProgressToGameOverTransition ");
        ExecuteEventHandler(fromState, appliedEvent, toState);
    }


    public void HandleOnBestScoreScreenToInitTransition(int fromState, int appliedEvent, int toState)
    {
        Debugging.instance.InfoLog("GameStateManager.HandleOnBestScoreScreenToOnTitleScreenTransition ");
        ExecuteEventHandler(fromState, appliedEvent, toState);
    }
}
