using TabSystemLogic.States;
using UnityEngine;

namespace TabSystemLogic
{
    [System.Serializable]
    public class TabStatesList 
    {
        [SerializeField]
        private TabState _enabledState;
        [SerializeField]
        private TabState _disabledState;
        [SerializeField]
        private TabState _lockedState;

        public TabState GetValue(State state)
        {
            return state switch
            {
                State.Enabled => _enabledState,
                State.Disabled => _disabledState,
                State.Locked => _lockedState,
                _ => _disabledState
            };
        }
    }
}

