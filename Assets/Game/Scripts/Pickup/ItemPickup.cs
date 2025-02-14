using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private float _dropForce = 5f;

    private Transform _holdPosition;
    private Button _dropButton;
    private GameObject _heldItem;
    private Rigidbody _heldRigidbody;
    private Collider _heldCollider;

    public void Init(Transform holdPosition, Button dropButton)
    {
        _holdPosition = holdPosition;
        _dropButton = dropButton;
        _dropButton.onClick.AddListener(DropItem);
        _dropButton.gameObject.SetActive(false);
    }

    public void TryPickup(Ray ray)
    {
        if (_heldItem != null) return;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<PickupableObject>(out var pickupableObject) &&
                hit.collider.TryGetComponent<Rigidbody>(out _heldRigidbody))
            {
                PickupItem(hit.collider.gameObject);
            }
        }
    }

    private void PickupItem(GameObject item)
    {
        _heldItem = item;
        _heldRigidbody = _heldItem.GetComponent<Rigidbody>();
        _heldCollider = _heldItem.GetComponent<Collider>();

        _heldRigidbody.isKinematic = true;
        _heldCollider.enabled = false;
        _heldItem.transform.SetParent(_holdPosition);
        _heldItem.transform.localPosition = Vector3.zero;
        _heldItem.transform.localRotation = Quaternion.identity;

        _dropButton.gameObject.SetActive(true);
    }

    public void DropItem()
    {
        if (_heldItem == null) return;

        _heldRigidbody.isKinematic = false;
        _heldCollider.enabled = true;
        _heldItem.transform.SetParent(null);
        _heldRigidbody.AddForce(Camera.main.transform.forward * _dropForce, ForceMode.Impulse);

        _heldItem = null;
        _heldRigidbody = null;
        _heldCollider = null;
        _dropButton.gameObject.SetActive(false);
    }
}
