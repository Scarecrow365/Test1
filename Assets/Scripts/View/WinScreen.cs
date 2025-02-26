using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class WinScreen : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] private Button retryButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private TMP_Text currentScore;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private ParticleSystem particleSystem;
        
        [Header("Animation parts")] 
        [SerializeField] private Transform tab;
        [SerializeField] private Image balloonImage;
        [SerializeField] private CanvasGroup buttonsCanvasGroup;
        
        [Header("Timers")]
        [SerializeField] private float startDelay;
        [SerializeField] private float startFadeDuration;
        [SerializeField] private float tabDuration;
        [SerializeField] private float buttonDuration;

        private int score;
        private int totalScore;

        private Sequence sequence;

        public event Action OnQuit;
        public event Action OnNext;
        public event Action OnRetry;

        private void Awake()
        {
            retryButton.onClick.AddListener(() => OnRetry?.Invoke());
            continueButton.onClick.AddListener(() => OnNext?.Invoke());
        }
        
        private void OnDestroy()
        {
            retryButton.onClick.RemoveAllListeners();
            continueButton.onClick.RemoveAllListeners();
        }

        public void Show()
        {
            if (sequence != null && sequence.IsPlaying())
                return;
            
            Prepare();

            sequence = DOTween.Sequence()
                .AppendInterval(startDelay)
                .Append(canvasGroup.DOFade(1, startFadeDuration))
                .AppendCallback(() => { particleSystem.Play(); })
                .Append(tab.DOScale(1, tabDuration).SetEase(Ease.OutBack))
                .Join(balloonImage.transform.DOScale(1, tabDuration).SetEase(Ease.OutBack))
                .Append(buttonsCanvasGroup.DOFade(1, buttonDuration))
                .OnComplete(()=>{currentScore.SetText(totalScore.ToString());});
        }

        public void SetScores(int score, int totalScore)
        {
            this.score = score;
            this.totalScore = totalScore;
            currentScore.SetText(score.ToString());
        }

        private void Prepare()
        {
            canvasGroup.alpha = 0;
            buttonsCanvasGroup.alpha = 0;

            tab.localScale = Vector3.zero;
            balloonImage.transform.localScale = Vector3.zero;
            
            gameObject.SetActive(true);
        }
    }
}