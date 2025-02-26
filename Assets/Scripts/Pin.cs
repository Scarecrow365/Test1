using DG.Tweening;
using UnityEngine;

public class Pin : MonoBehaviour, IInputHandler
{
    [Header("Tutorial Settings")] 
    [SerializeField] private GameObject spriteRenderer;
    [SerializeField] private GameObject glow;
    [SerializeField, Range(0, 2)] private float directionMultiply = 0.2f;
    [SerializeField, Range(0, 2)] private float moveDirectionDuration = 1.5f;

    [Header("Input settings")]
    [SerializeField] private float hideDuration = 5;
    [SerializeField] private float angleThreshold = 15;
    [SerializeField] private float throwOutMultiple = 10;

    private Tween glowTween;
        
    public void InputHold() => Select();

    public void InputRelease() => Deselect();

    private void OnDisable() => glowTween?.Complete();

    public void Move(Vector2 direction)
    {
        direction = direction.normalized;

        var angle = Vector2.Angle(direction, -transform.up);
        if (angle > angleThreshold)
            return;
            
        var newPosition = transform.position - (transform.up * throwOutMultiple);
        transform.DOMove(newPosition, hideDuration);
    }

    public Tween ShowTutorial()
    {
        var finishPos = -Vector3.up * directionMultiply;
        return spriteRenderer.transform.DOLocalMove(finishPos, moveDirectionDuration);
    }

    public Tween HideTutorial()
    {
        Deselect();
        return spriteRenderer.transform.DOLocalMove(Vector3.zero, moveDirectionDuration);
    }

    private void Select()
    {
        glowTween?.Complete();
        glow.gameObject.SetActive(true);
        // glowTween = glow.DOFade(0, 0);
        // glow.DOFade(1, 0.2f);
    }

    private void Deselect()
    {
        glowTween?.Complete();
        // glow.DOFade(0, 0.2f)
        //     .OnComplete(() => glow.gameObject.SetActive(false));
        glow.gameObject.SetActive(false);
    }
}