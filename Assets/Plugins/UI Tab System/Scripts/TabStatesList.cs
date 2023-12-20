using TabSystemLogic.States;
using UnityEngine;

namespace TabSystemLogic
{
    [System.Serializable]
    public class TabStatesList 
    {
        [SerializeField]
        private TabState _activeState;
        [SerializeField]
        private TabState _inactiveState;
        [SerializeField]
        private TabState _lockedState;

        public TabState GetValue(State state)
        {
            return state switch
            {
                State.Active => _activeState,
                State.Inactive => _inactiveState,
                State.Locked => _lockedState,
                _ => _inactiveState
            };
        }
    }
}

