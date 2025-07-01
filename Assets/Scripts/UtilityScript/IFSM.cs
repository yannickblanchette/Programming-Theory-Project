namespace UtilityScripts
{
    public interface IFSM
    {
        public void GenericEventHandler(int fromState, int appliedEvent, int toState);

        public int GetCurrentState();

        public void InsertGenericTransition(FSMTransition transition);

        public void InsertTransition(FSMTransition transition);

        public void InvalidEventHandler(int fromState, int appliedEvent, int toState);

        public void ProcessEvent(int thisEvent);

        public void SetInitialState(int initialState);
    }
}

