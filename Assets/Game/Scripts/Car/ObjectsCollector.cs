using System;
using TMPro;
using UnityEngine;

public class ObjectsCollector : MonoBehaviour
{
    [SerializeField] private int _maxValue;

    public event Action OnCollectAll;

    private TextMeshProUGUI _textMeshProUGUI;
    private int _currentCountOfObjects;

    public void Init(TextMeshProUGUI textMeshProUGUI)
    {
        _textMeshProUGUI = textMeshProUGUI;
        UpdateAmount();
    }

    private void OnTriggerEnter(Collider pickUpCollider)
    {
        HandleObjectCollision(pickUpCollider, 1);
    }

    private void OnTriggerExit(Collider pickUpCollider)
    {
        HandleObjectCollision(pickUpCollider, -1);
    }

    private void HandleObjectCollision(Collider pickUpCollider, int valueChange)
    {
        if (pickUpCollider.gameObject.GetComponent<PickupableObject>() != null)
        {
            _currentCountOfObjects += valueChange;
            UpdateAmount();
        }
    }

    private void UpdateAmount()
    {
        _textMeshProUGUI.text = $"{_currentCountOfObjects}/{_maxValue}";

        if (_currentCountOfObjects >= _maxValue)
        {
            OnCollectAll?.Invoke();
        }
    }
}
