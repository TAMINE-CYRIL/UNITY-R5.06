using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    public Item item;
    public int amount = 1;

    // Option 1 : ramassage automatique via trigger
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inv = other.GetComponent<PlayerInventory>();
        if (inv == null) return;

        int leftover = inv.AddItem(item, amount); // retourne la quantité non ajoutée
        if (leftover <= 0)
        {
            Destroy(gameObject); // tout ramassé
        }
        else
        {
            amount = leftover; // reste sur le sol
        }
    }
}