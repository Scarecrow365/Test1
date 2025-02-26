using DG.Tweening;
using TMPro;
using UnityEngine;

namespace View
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text infoText;
        [SerializeField, Range(0, 1)] private float textAnimationDuration = 0.5f; 
        
        private float currentResult;
        private Tween resultTextAnimation;

        public void Show() => gameObject.SetActive(true);
        
        public void UpdateResultText(float result)
        {
            var duration = result <= 0 ? 0 : textAnimationDuration;
            resultTextAnimation?.Kill();
            resultTextAnimation = DOTween
                .To(() => currentResult, x => currentResult = x, result, duration)
                .OnUpdate(() => { infoText.SetText($"{currentResult:F0}%"); });
        }

        public void Complete()
        {
            resultTextAnimation.Complete();
            gameObject.SetActive(false);
        }
    }
}