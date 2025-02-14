using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ItemPickup _itemPickup;
    private PlayerInput _playerInput;
    private Transform _playerCamera;

    private float _rotationX = 0f;
    private const float MoveSpeed = 6f;
    private const float LookSpeed = 5f;

    public void Init(PlayerInput playerInput, Transform playerCamera, ItemPickup itemPickup)
    {
        _playerInput = playerInput;
        _playerCamera = playerCamera;
        _itemPickup = itemPickup;

        _playerInput.OnMove += HandleMovement;
        _playerInput.OnLook += HandleLook;
        _playerInput.OnTap += HandleTap;
        //_playerInput.OnHold += HandleHold;
    }

    private void OnDisable()
    {
        _playerInput.OnMove -= HandleMovement;
        _playerInput.OnLook -= HandleLook;
        _playerInput.OnTap -= HandleTap;
        //_playerInput.OnHold -= HandleHold;
    }

    private void HandleMovement(Vector2 moveInput)
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(move * MoveSpeed * Time.deltaTime, Space.Self);
    }

    private void HandleLook(Vector2 lookDelta)
    {
        float lookX = lookDelta.x * LookSpeed * Time.deltaTime;
        float lookY = lookDelta.y * LookSpeed * Time.deltaTime;

        _rotationX -= lookY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        _playerCamera.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.Rotate(Vector3.up * lookX);
    }

    private void HandleTap()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        _itemPickup.TryPickup(ray);
    }

    //private void HandleHold()
    //{
    //    Debug.Log("Зажатие!");
    //}
}