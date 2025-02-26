using DG.Tweening;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Pin pin;
    [SerializeField] private SpriteRenderer hand;

    [Header("Animations")] 
    [SerializeField, Range(0, 2f)] private float startDelay; 
    [SerializeField, Range(0.1f, 2)] private float fadeInDuration = 0.5f;
    [SerializeField, Range(0.1f, 2)] private float fadeOutDuration = 0.2f;
    [SerializeField, Range(0.1f, 2)] private float directionMultiply = 1.5f;
    [SerializeField, Range(0.1f, 2)] private float moveDirectionDuration = 1.5f;
    [SerializeField, Range(0.1f, 2)] private float moveToStartPointDuration = 1f;

    private Sequence sequence;
        
    private void Awake()
    {
        hand.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
            Hide();
    }

    public void Show()
    {
        PrepareForShowAnimation();

        sequence ??= DOTween.Sequence();
        sequence
            .AppendInterval(startDelay)
            .Append(hand.DOFade(1, moveToStartPointDuration))
            .Join(hand.transform.DOMove(pin.transform.position, moveToStartPointDuration))
            .Append(LoopAnimation());
    }

    private Tween LoopAnimation()
    {
        var startPos = pin.transform.position;
        var finishPos = pin.transform.position + (-pin.transform.up * directionMultiply);
        finishPos.z = -2;

        return DOTween.Sequence()
            .Append(hand.DOFade(1, fadeOutDuration)
                .OnComplete(() => pin.InputHold()))
            .Append(hand.transform.DOMove(finishPos, moveDirectionDuration))
            .Join(pin.ShowTutorial())
            .Append(hand.DOFade(0, fadeInDuration)
                .OnComplete(() => pin.InputRelease()))
            .Append(hand.transform.DOMove(startPos, 0.2f))
            .Join(pin.HideTutorial())
            .SetLoops(-1);
    }

    private void PrepareForShowAnimation()
    {
        hand.color = new Color(1, 1, 1, 0);
        hand.gameObject.SetActive(true);
    }

    private void Hide()
    {
        hand.gameObject.SetActive(false);
        sequence?.Kill();
        pin.HideTutorial();
        pin = null;
        gameObject.SetActive(false);
    }
}