using System.Collections.Generic;
using UnityEngine;

namespace TabSystemLogic
{
    public class TabSystem : MonoBehaviour
    {
        public IReadOnlyList<Tab> Tabs => _tabs;

        [SerializeField] private Tab[] _tabs;

        private void Awake()
        {
            if(_tabs.Length == 0)
                _tabs = GetComponentsInChildren<Tab>(true);

            foreach (var tab in _tabs)
            {
                if (!tab) continue;
                tab.OnActivated += OnTabTriggered;
            }
        }

        private void OnEnable()
        {
            var state = State.Active;
            foreach (var tab in _tabs)
            {
                if (!tab) continue;
                if (tab.TabState == State.Locked) continue;

                tab.SetState(state);
                state = State.Inactive;
            }
        }

        private void OnDestroy()
        {
            foreach (var tab in _tabs)
            {
                if (!tab) continue;

                tab.OnActivated -= OnTabTriggered;
            }
        }

        private void OnTabTriggered(Tab triggeredTab)
        {
            foreach(var tab in _tabs)
            {
                if (!tab) continue;
                if (tab == triggeredTab) continue;
                if (tab.TabState == State.Locked) continue;

                tab.SetState(State.Inactive);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Get Tabs")]
        private void GetTabs()
        {
            if (Application.isPlaying) return;

            _tabs = GetComponentsInChildren<Tab>(true);
            UnityEditor.EditorUtility.SetDirty(this);
        } 
#endif
    }
}

