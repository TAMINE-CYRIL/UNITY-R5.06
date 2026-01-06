using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyId;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            inventory.AddKey(keyId);
        }

        Destroy(gameObject);
    }
}
