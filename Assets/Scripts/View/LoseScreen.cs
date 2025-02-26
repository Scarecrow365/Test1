using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class LoseScreen : MonoBehaviour
    {
        [Header("Start animation  settings")]
        [SerializeField] private float startDelay;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float startFadeDuration;

        [Header("Fail animation settings")] 
        [SerializeField] private Image failImage;
        [SerializeField] private float startScale;
        [SerializeField] private float fadeDuration;
        [SerializeField] private float failSizeChangeDuration;
        
        [Header("Button Animation settings")]
        [SerializeField] private Button retryButton;
        [SerializeField] private CanvasGroup buttonCanvasGroup;
        [SerializeField] private float buttonAnimationDuration;

        private Sequence sequence;
        
        public event Action OnRetry;

        private void Awake()
        {
            retryButton.onClick.AddListener(() => OnRetry?.Invoke());
        }

        public void Show()
        {
            if (sequence != null && sequence.IsPlaying())
                return;
            
            PrepareAnimation();
            gameObject.SetActive(true);

            sequence = DOTween.Sequence()
                .AppendInterval(startDelay)
                .Append(canvasGroup.DOFade(1, startFadeDuration))
                .Append(failImage.DOFade(1, fadeDuration))
                .Append(failImage.rectTransform.DOScale(Vector3.one, failSizeChangeDuration).SetEase(Ease.OutBack))
                .Append(buttonCanvasGroup.DOFade(1, buttonAnimationDuration));
        }

        private void PrepareAnimation()
        {
            canvasGroup.alpha = 0;
            failImage.DOFade(0, 0);
            failImage.transform.localScale = Vector3.one * startScale;
            buttonCanvasGroup.alpha = 0;
        }
    }
}