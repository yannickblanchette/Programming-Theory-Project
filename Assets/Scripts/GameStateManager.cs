using System;
using System.Collections.Generic;
using UnityEngine;
using UtilityScripts;
using UnityEngine.Events;

namespace GameLogic
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager instance { get; private set; }

        public UnityEvent initInitCompletedEvent;
        public UnityEvent onTitleScreenStartPressedEvent;
        public UnityEvent onTitleScreenExitPressedEvent;
        public UnityEvent waitingToPlayPlayPressedEvent;
        public UnityEvent inProgressGameOverEvent;
        public UnityEvent gameOverRestartPressedEvent;
        public UnityEvent gameOverBackToTitlePressedEvent;


        // ENCAPSULATION
        public GameStates currentGameState
        {
            get { return (GameStates)gameFSM.GetCurrentState(); }
        }

        private IFSM gameFSM;


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            // end of new code

            instance = this;
            InitializeLocalVariables();
        }



        private void InitializeLocalVariables()
        {
            InitializeGameFSM();
        }


        private void InitializeGameFSM()
        {
            gameFSM = new FSM(Enum.GetNames(typeof(GameStates)).Length, Enum.GetNames(typeof(GameEvents)).Length, Debugging.instance);
            gameFSM.InsertTransition(new(((int)GameStates.init), (int)GameEvents.initCompleted, (int)GameStates.OnTitleScreen, this.HandleInitInitCompletedEvent));
            gameFSM.InsertTransition(new(((int)GameStates.OnTitleScreen), (int)GameEvents.StartPressed, (int)GameStates.WaitingToPlay, this.HandleOnTitleScreenStartPressedEvent));
            gameFSM.InsertTransition(new(((int)GameStates.OnTitleScreen), (int)GameEvents.ExitPressed, (int)GameStates.Exit, this.HandleOnTitleScreenExitPressedEvent));
            gameFSM.InsertTransition(new(((int)GameStates.WaitingToPlay), (int)GameEvents.PlayPressed, (int)GameStates.InProgress, this.HandleWaitingToPlayPlayPressedEvent));
            gameFSM.InsertTransition(new(((int)GameStates.InProgress), (int)GameEvents.GameOver, (int)GameStates.GameOver, this.HandleInProgressGameOverEvent));
            gameFSM.InsertTransition(new(((int)GameStates.GameOver), (int)GameEvents.RestartPressed, (int)GameStates.WaitingToPlay, this.HandleGameOverRestartPressedEvent));
            gameFSM.InsertTransition(new(((int)GameStates.GameOver), (int)GameEvents.BackToTitlePressed, (int)GameStates.init, this.HandleGameOverBackToTitlePressedEvent));
            gameFSM.SetInitialState((int)GameStates.init);
        }


        // POLYMORPHISM
        public void ProcessEvent(int gameEvent)
        {
            Debugging.instance.InfoLog("GameStateManager.ProcessEvent " + gameEvent);
            gameFSM.ProcessEvent(gameEvent);
        }


        public void ProcessEvent(GameEvents gameEvent)
        {
            this.ProcessEvent((int)gameEvent);
        }


        public void HandleInitInitCompletedEvent(int fromState, int appliedEvent, int toState)
        {
            Debugging.instance.InfoLog("GameStateManager.Handle_init_initCompletedEvent ");
            initInitCompletedEvent.Invoke();
        }


        public void HandleOnTitleScreenStartPressedEvent(int fromState, int appliedEvent, int toState)
        {
            Debugging.instance.InfoLog("GameStateManager.HandleOnTitleScreenStartPressedEvent ");
            onTitleScreenStartPressedEvent.Invoke();
        }


        public void HandleOnTitleScreenExitPressedEvent(int fromState, int appliedEvent, int toState)
        {
            Debugging.instance.InfoLog("GameStateManager.HandleOnTitleScreenExitPressedEvent ");
            onTitleScreenExitPressedEvent.Invoke();
        }


        public void HandleWaitingToPlayPlayPressedEvent(int fromState, int appliedEvent, int toState)
        {
            Debugging.instance.InfoLog("GameStateManager.HandleWaitingToPlayPlayPressedEvent ");
            waitingToPlayPlayPressedEvent.Invoke();
        }


        public void HandleInProgressGameOverEvent(int fromState, int appliedEvent, int toState)
        {
            Debugging.instance.InfoLog("GameStateManager.HandleInProgressGameOverEvent ");
            inProgressGameOverEvent.Invoke();
        }


        public void HandleGameOverRestartPressedEvent(int fromState, int appliedEvent, int toState)
        {
            Debugging.instance.InfoLog("GameStateManager.HandleGameOverRestartPressedEvent ");
            gameOverRestartPressedEvent.Invoke();
        }


        public void HandleGameOverBackToTitlePressedEvent(int fromState, int appliedEvent, int toState)
        {
            Debugging.instance.InfoLog("GameStateManager.HandleGameOverBackToTitlePressedEvent ");
            gameOverBackToTitlePressedEvent.Invoke();  
        }
    }

}

