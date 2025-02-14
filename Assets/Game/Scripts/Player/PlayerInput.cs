using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const float HoldThreshold = 0.3f;

    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action OnTap;
    public event Action OnHold;

    private FloatingJoystick _joystick;
    private float _holdTime = 0f;
    private bool _isHolding = false;

    public void Init(FloatingJoystick joystick)
    {
        _joystick = joystick;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
        HandleTouchInput();
    }

    private void HandleMovement()
    {
        Vector2 moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);
        if (moveInput != Vector2.zero)
        {
            OnMove?.Invoke(moveInput);
        }
    }

    private void HandleLook()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved && !IsTouchOverJoystick(touch))
            {
                Vector2 lookDelta = touch.deltaPosition;
                OnLook?.Invoke(lookDelta);
            }
        }
    }

    private void HandleTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _holdTime = Time.time;
                _isHolding = true;
            }

            if (_isHolding && (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved))
            {
                if (Time.time - _holdTime >= HoldThreshold)
                {
                    OnHold?.Invoke();
                    _isHolding = false;
                }
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (Time.time - _holdTime < HoldThreshold)
                {
                    OnTap?.Invoke();
                }

                _isHolding = false;
            }
        }
    }

    private bool IsTouchOverJoystick(Touch touch)
    {
        Vector2 joystickPos = (Vector2)_joystick.transform.position;
        float joystickRadius = _joystick.GetComponent<RectTransform>().sizeDelta.x * 0.6f;
        return Vector2.Distance(touch.position, joystickPos) < joystickRadius;
    }
}
