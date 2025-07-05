#nullable enable

namespace UtilityScripts
{
    public class FSMTransition
    {
        public delegate void EventHandler(int fromState, int appliedEvent, int toState);

        public int fromState;
        public int appliedEvent;
        public int toState;
        public EventHandler? eventHandler;


        public FSMTransition(int fromState, int appliedEvent, int toState, EventHandler eventHandler)
        {
            if ((fromState >= 0) &&
                (appliedEvent >= 0) &&
                (toState >= 0))
            {
                this.fromState = fromState;
                this.appliedEvent = appliedEvent;
                this.toState = toState;
                this.eventHandler = eventHandler;
            }
            else
            {
                //UnityEngine.Debug.Log("Invalid parameters");
            }
        }
    }



    public class FSM : IFSM
    {

        private FSMTransition[,] transitionArray;
        private int currentState;

        private IDebugging debugObject;


        public FSM(int numberOfStates, int numberOfEvents, IDebugging debugObj)
        {
            debugObject = debugObj;

            transitionArray = new FSMTransition[numberOfStates, numberOfEvents];

            //initialize array with HandleInvalidEvent
            for (int outer = 0; outer < numberOfStates; outer++)
            {
                for (int inner = 0; inner < numberOfEvents; inner++)
                {
                    transitionArray[outer, inner] = new(outer, inner, outer, InvalidEventHandler);
                }
            }

            currentState = 0;
        }


        public int GetCurrentState()
        {
            return currentState;
        }


        public void SetInitialState(int initialState)
        {
            this.currentState = initialState;
        }


        public void InsertTransition(FSMTransition transition)
        {
            if (transition == null)
            {
                debugObject.ErrorLog("transition is null");
                return;
            }
            if ((transition.fromState < 0) ||
                (transition.toState < 0) ||
                (transition.appliedEvent < 0))
            {
                debugObject.ErrorLog("states or event are null");
                return;
            }
            if (transition.eventHandler == null)
            {
                debugObject.ErrorLog("event handler is null");
                return;
            }

            transitionArray[transition.fromState, transition.appliedEvent] = transition;
        }


        public void InsertGenericTransition(FSMTransition transition)
        {
            transition.eventHandler = GenericEventHandler;
            transitionArray[transition.fromState, transition.appliedEvent] = transition;
        }


        public void ProcessEvent(int thisEvent)
        {
            FSMTransition transition = transitionArray[this.currentState, thisEvent];
            if ((transition != null) && transition.eventHandler != null)
            {
                transition.eventHandler(transition.fromState, transition.appliedEvent, transition.toState);
                this.currentState = transition.toState;
                debugObject.InfoLog("Transition from: " + transition.fromState.ToString() + " event " + transition.appliedEvent.ToString());
            }
            else if (transition == null)
            {
                debugObject.ErrorLog("State transition entry is null: ");
            }
            else
            {
                debugObject.ErrorLog("Event handler is null: " + transition.fromState.ToString() + " " + transition.appliedEvent.ToString());
            }
        }


        public void InvalidEventHandler(int fromState, int appliedEvent, int toState)
        {
            debugObject.DebugLog("Invalid transition: " + fromState.ToString() + " " + appliedEvent.ToString() + " " + toState.ToString());
        }


        public void GenericEventHandler(int fromState, int appliedEvent, int toState)
        {
            //the generic event handler does nothing during the transition
        }

    }
}


