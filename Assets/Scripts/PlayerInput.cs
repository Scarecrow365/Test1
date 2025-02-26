using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float swipeThreshold = 5f;

    private IInputHandler inputHandler;
    private Vector2 startTouchPosition;

    private bool isActive;

    public void Enable() => isActive = true;
    public void Disable() => isActive = false;

    private void Update()
    {
        if (!isActive)
            return;
            
        Tap();
        Slide();
        Release();
    }

    private void Tap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;

            var results = new RaycastHit2D[10];
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hitCount = Physics2D.RaycastNonAlloc(mousePosition, Vector2.zero, results);
                
            for (var i = 0; i < hitCount; i++)
            {
                if (results[i].collider != null && results[i].collider.TryGetComponent(out inputHandler))
                {
                    inputHandler.InputHold();
                    return;
                }
            }
        }
    }

    private void Slide()
    {
        if (Input.GetMouseButton(0))
        {
            var swipeVector = (Vector2) Input.mousePosition - startTouchPosition;

            if (swipeVector.magnitude <= swipeThreshold)
            {
                startTouchPosition = Input.mousePosition;
                return;
            }

            var swipePosition = (Vector2) Input.mousePosition - startTouchPosition;
            startTouchPosition = Input.mousePosition;
            inputHandler?.Move(swipePosition);
        }
    }

    private void Release()
    {
        if (Input.GetMouseButtonUp(0))
        {
            inputHandler?.InputRelease();
            inputHandler = null;
            startTouchPosition = Vector3.zero;
        }
    }
}