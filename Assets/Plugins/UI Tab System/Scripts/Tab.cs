using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TabSystemLogic
{
    public enum State
    {
        Enabled,
        Disabled,
        Locked
    }

    public class Tab : MonoBehaviour
    {
        public event Action<Tab> OnActivated;
        public event Action<Tab> OnDeactivated;
        public event Action<State> OnStateChanged;

        public bool IsActive => _isActive;
        public State TabState => _tabState;
        public Image Background => _background;
        public Image Icon => _icon;
        public TMP_Text TabText => _tabText;
        public LayoutElement LayoutElement => _layoutElement;
        public Button Button => _button;
        public Canvas Canvas => _canvas;
        public TabStatesList States => _states;

        [SerializeField] 
        private State _tabState;

        [Space]
        [SerializeField] 
        private Image _background;
        [SerializeField] 
        private Image _icon;
        [SerializeField] 
        private TMP_Text _tabText;
        [SerializeField] 
        private LayoutElement _layoutElement;
        [SerializeField] 
        private Button _button;
        [SerializeField] 
        private Canvas _canvas;
        
        [Space]
        [SerializeField] 
        private TabStatesList _states;

        [Space]
        [SerializeField] 
        private RectTransform _targetObject;
        [SerializeField] 
        private bool _updateLayoutOnEnable;

        [Space]
        [Tooltip("Invokes when user clicks on tab and it`s not locked")]
        [SerializeField] 
        private UnityEvent _onActivated;
        [Tooltip("Invokes when user clicks on another tab")]
        [SerializeField]
        private UnityEvent _onDeactivated;

        private bool _isActive;

#if UNITY_EDITOR
        [HideInInspector] 
        [SerializeField]
        private State _lastState; 
#endif

        protected virtual void Start()
        {
            if (Button)
                Button.onClick.AddListener(OnClick);
        }

        protected virtual void OnDestroy()
        {
            if (Button)
                Button.onClick.RemoveListener(OnClick);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isPlaying) return;
            if (_lastState == TabState) return;

            SetState(TabState, false);
            _lastState = TabState;
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif

        public virtual void OnClick()
        {
            if (TabState == State.Enabled) return;

            SetState(State.Enabled);
        }

        public virtual void SetState(State targetState, bool invokeEvents = true)
        {
            _tabState = targetState;
            _isActive = TabState == State.Enabled;

            var state = States.GetValue(TabState);
            state.ApplyState(this);

            if (_targetObject)
            {
                _targetObject.gameObject.SetActive(IsActive);

                if (_updateLayoutOnEnable)
                    LayoutRebuilder.ForceRebuildLayoutImmediate(_targetObject);
            }

            if (!invokeEvents) return;

            OnStateChanged?.Invoke(TabState);

            if (IsActive)
            {
                OnActivated?.Invoke(this);
                _onActivated?.Invoke();
            }
            else
            {
                OnDeactivated?.Invoke(this);
                _onDeactivated?.Invoke();
            }
        }
    }
}

