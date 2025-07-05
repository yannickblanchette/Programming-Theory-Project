using System;
using System.Collections.Generic;
using UnityEngine;
using UtilityScripts;

namespace GameLogic
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager instance { get; private set; }

        // Encapsulation
        public GameStates currentGameState
        {
            get { return (GameStates)gameFSM.GetCurrentState(); }
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
            InitializeLocalVariables();
        }



        // ABSTRACTION
        private void InitializeLocalVariables()
        {
            InitializeGameFSM();
            InitializeEventListeners();
        }


        private void InitializeGameFSM()
        {
            gameFSM = new FSM(Enum.GetNames(typeof(GameStates)).Length, Enum.GetNames(typeof(GameEvents)).Length, Debugging.instance);
            gameFSM.InsertTransition(new(((int)GameStates.init), (int)GameEvents.initCompleted, (int)GameStates.OnTitleScreen, this.HandleInitToOnTitleScreenTransition));
            gameFSM.InsertTransition(new(((int)GameStates.OnTitleScreen), (int)GameEvents.StartPressed, (int)GameStates.WaitingToPlay, this.HandleOnTitleScreenToWaitingToPlayTransition));
            gameFSM.InsertTransition(new(((int)GameStates.OnTitleScreen), (int)GameEvents.ExitPressed, (int)GameStates.Exit, this.HandleOnTitleScreenToExitTransition));
            gameFSM.InsertTransition(new(((int)GameStates.WaitingToPlay), (int)GameEvents.PlayPressed, (int)GameStates.InProgress, this.HandleWaitingToPlayToInProgressTransition));
            gameFSM.InsertTransition(new(((int)GameStates.InProgress), (int)GameEvents.GameOver, (int)GameStates.GameOver, this.HandleInProgressToGameOverTransition));
            gameFSM.InsertTransition(new(((int)GameStates.GameOver), (int)GameEvents.RestartPressed, (int)GameStates.WaitingToPlay, this.HandleOnBestScoreScreenToWaitingToPlayTransition));
            gameFSM.InsertTransition(new(((int)GameStates.GameOver), (int)GameEvents.BackToTitlePressed, (int)GameStates.init, this.HandleOnBestScoreScreenToInitTransition));
            gameFSM.SetInitialState((int)GameStates.init);
        }


        private void InitializeEventListeners()
        {
            eventListeners = new List<FSMTransition.EventHandler>[Enum.GetNames(typeof(GameStates)).Length, Enum.GetNames(typeof(GameEvents)).Length];
            //initialize array with HandleInvalidEvent
            for (int outer = 0; outer < Enum.GetNames(typeof(GameStates)).Length; outer++)
            {
                for (int inner = 0; inner < Enum.GetNames(typeof(GameEvents)).Length; inner++)
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


        public void ProcessEvent(GameEvents gameEvent)
        {
            this.ProcessEvent((int)gameEvent);
        }


        /// <summary>
        /// Add the passed in event listener to the list of event listener that will be executed when this game transition happens
        /// </summary>
        /// <param name="gameState">the actual game state at the time of the event</param>
        /// <param name="gameEvent">the actual event that occured</param>
        /// <param name="eventListener">the event listener to be called on the transition</param>
        public void AddEventListener(GameStates gameState, GameEvents gameEvent, FSMTransition.EventHandler eventListener)
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

}

