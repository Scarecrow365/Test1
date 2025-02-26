using UnityEngine;

public interface IInputHandler
{
    void InputHold();
    void InputRelease();
    void Move(Vector2 direction);
}