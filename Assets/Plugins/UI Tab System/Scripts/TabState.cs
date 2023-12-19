using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TabSystemLogic.States
{
    [System.Serializable]
    public class TabState
    {
        [Min(0), SerializeField] public float AnimationDuration;

        [Space]
        [Header("Button Settings")]
        [SerializeField] 
        public bool IsButtonActive = true;

        [Space]
        [Header("Canvas Settings")]
        [SerializeField] 
        public bool OverrideSorting;
        [SerializeField] 
        public int AdditiveSortig;

        [Space]
        [Header("Icon Settings")]
        [SerializeField] 
        public Sprite IconSprite;
        [SerializeField] 
        public Color IconColor = Color.white;
        [SerializeField] 
        public bool OverrideIconPosition = true;
        [SerializeField] 
        public Vector3 TargetIconPosition;
        [SerializeField]
        public Ease IconEase;
        [Range(0, 1f)]
        [SerializeField]
        public float IconDurationCoefficient = 1f;

        [Space]
        [Header("Background Settings")]
        [SerializeField] 
        public Sprite BackgroundSprite;
        [SerializeField] 
        public Color BackgroundColor = Color.white;
        [SerializeField] 
        public Ease BackgroundEase;
        [Range(0, 1f)]
        [SerializeField]
        public float BackgroundDurationCoefficient = 1f;


        [Space]
        [Header("Text Settings")]
        [SerializeField] 
        public Color TextColor = Color.white;
        [SerializeField] 
        public Ease TextEase;
        [Range(0, 1f)]
        [SerializeField]
        public float TextDurationCoefficient = 0.6f;

        [Space]
        [Header("Layout Settings")]
        [Min(0)]
        [SerializeField] 
        public Vector2 MinSize;
        [SerializeField] 
        public Ease SizeEase;
        [Range(0, 1f)]
        [SerializeField]
        public float LayoutDurationCoefficient = 0.5f;

        [Space]
        [SerializeField] 
        public List<GameObject> ObjectToEnable;
        [SerializeField] 
        public List<GameObject> ObjectToDisable;

        public void ApplyState(Tab tab)
        {
            if (!tab) return;

            if (AnimationDuration < 0)
                AnimationDuration = 0;

            if(MinSize.x < 0)
                MinSize.x = 0;

            if(MinSize.y < 0)
                MinSize.y = 0;

            ObjectToEnable.ForEach(obj => SetObjectState(obj, true));
            ObjectToDisable.ForEach(obj => SetObjectState(obj, false));

            if (tab.Canvas)
            {
                tab.Canvas.overrideSorting = OverrideSorting;

                if (OverrideSorting && !tab.Canvas.isRootCanvas)
                {
                    tab.Canvas.sortingOrder = tab.Canvas.rootCanvas.sortingOrder + AdditiveSortig;
                }

#if UNITY_EDITOR
                SetDirty(tab.Canvas); 
#endif
            }

            if (tab.Button)
            {
                tab.Button.interactable = IsButtonActive;

#if UNITY_EDITOR
                SetDirty(tab.Button); 
#endif
            }

            if (tab.Background)
            {
                tab.Background.DOKill();
                tab.Background.DOColor(BackgroundColor, AnimationDuration * BackgroundDurationCoefficient).SetEase(BackgroundEase).SetLink(tab.Background.gameObject);

                if (BackgroundSprite)
                {
                    tab.Background.sprite = BackgroundSprite;
                }

#if UNITY_EDITOR
                if (!Application.isPlaying)
                    tab.Background.color = BackgroundColor;

                SetDirty(tab.Background);
#endif
            }

            if (tab.Icon)
            {
                tab.Icon.DOKill();
                tab.Icon.DOColor(IconColor, AnimationDuration).SetEase(IconEase).SetLink(tab.Icon.gameObject);
                tab.Icon.rectTransform.DOAnchorPos(TargetIconPosition, AnimationDuration * IconDurationCoefficient).SetEase(IconEase).SetLink(tab.Icon.gameObject);

                if (IconSprite)
                {
                    tab.Icon.sprite = IconSprite;
                }

#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    tab.Icon.color = IconColor;
                    tab.Icon.rectTransform.anchoredPosition = TargetIconPosition;
                }

                SetDirty(tab.Icon);
#endif
            }

            if (tab.TabText)
            {
                tab.TabText.DOKill();
                tab.TabText.DOColor(TextColor, AnimationDuration * TextDurationCoefficient).SetEase(TextEase).SetLink(tab.TabText.gameObject);

#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    tab.TabText.color = TextColor;
                }

                SetDirty(tab.TabText);
#endif
            }

            if(tab.LayoutElement)
            {
                tab.LayoutElement.DOKill();

                tab.LayoutElement.DOMinSize(MinSize, AnimationDuration * LayoutDurationCoefficient)
                    .SetEase(SizeEase)
                    .SetLink(tab.LayoutElement.gameObject)
                    .OnComplete(() => LayoutRebuilder.ForceRebuildLayoutImmediate(tab.LayoutElement.transform as RectTransform));

#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    tab.LayoutElement.minHeight = MinSize.y;
                    tab.LayoutElement.minWidth = MinSize.x;
                }

                SetDirty(tab.LayoutElement); 
#endif
            }
        }

        private void SetObjectState(GameObject go, bool isActive)
        {
            if (!go) return;

            go.SetActive(isActive);
        }

#if UNITY_EDITOR
        private void SetDirty(Object obj)
        {
            UnityEditor.EditorUtility.SetDirty(obj);
        } 
#endif
    }
}
