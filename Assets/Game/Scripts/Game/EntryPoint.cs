using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntryPoint : MonoBehaviour
{
    [Header("Player Controll")]
    [SerializeField] private FloatingJoystick _moveJoystick;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _playerCamera;

    [Header("Item Pickup")]
    [SerializeField] private ItemPickup _itemPickup;
    [SerializeField] private Transform _holdPosition;
    [SerializeField] private Button _dropButton;

    [Header("ObjectCollector")]
    [SerializeField] private ObjectsCollector _collectObjects;
    [SerializeField] private TextMeshProUGUI _pickupObjectsCounter;

    [Header("GameEnder")]
    [SerializeField] private GameEnder _gameEnder;
    [SerializeField] private GameObject _winImage;

    private IEnumerator Start()
    {
        PlayerPickupInit();
        PlayerInputInit();
        PlayerControllerInit();
        ObjectsCollectorInit();
        GameEnderInit();
        return null;
    }

    private void PlayerControllerInit()
    {
        _playerController.Init(_playerInput, _playerCamera, _itemPickup);
    }
    private void PlayerInputInit()
    {
        _playerInput.Init(_moveJoystick);
    }
    private void PlayerPickupInit()
    {
        _itemPickup.Init(_holdPosition, _dropButton);
    }
    private void ObjectsCollectorInit()
    {
        _collectObjects.Init(_pickupObjectsCounter);
    }
    private void GameEnderInit()
    {
        _gameEnder.Init(_winImage, _collectObjects);
    }
}
