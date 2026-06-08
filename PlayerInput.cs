using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }
    
    public Vector2 MoveInput { get; private set; }
    public Vector2 MouseInput { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
    
    public void OnLook(InputValue value)
    {
        MouseInput = value.Get<Vector2>();

        if (TargetFinder.Instance.OnTarget)
        {
            if (MouseInput.x > .5)
            {
                TargetFinder.Instance.TargetSelect(1);
            }
            if (MouseInput.x <= -.5)
            {
                TargetFinder.Instance.TargetSelect(-1);
            }
        }
    }
    
    public void OnTargetLock(InputValue button)
    {
        if (button.isPressed && !TargetFinder.Instance.OnTarget)
        {
            TargetFinder.Instance.TargetLockOn();
            TargetMarkerC.MarkerActive();
        }
        else if (button.isPressed && TargetFinder.Instance.OnTarget)
        {
            TargetFinder.Instance.TargetLockOff();
        }
    }
}