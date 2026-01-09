using UnityEngine;

public class Chest : MonoBehaviour
{
    public WeaponItem weaponInside;
    private bool opened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (opened) return;
        if (!other.CompareTag("Player")) return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null) return;

        int leftover = inventory.AddItem(weaponInside, 1);
        if (leftover > 0)
        {
            Debug.Log("Inventaire plein");
            return;
        }

        opened = true;
        Debug.Log("Coffre ouvert : " + weaponInside.itemName);

        OpenChestVisual();
    }

    void OpenChestVisual()
    {
        GetComponent<Collider>().enabled = false;
        transform.Rotate(0, 90, 0);
    }
}
