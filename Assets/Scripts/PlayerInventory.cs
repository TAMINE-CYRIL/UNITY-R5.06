using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class Slot
{
    public int maxPerSlot = 5;
    public Item item = null;
    public int quantity = 0;

    public Slot(Item item = null, int quantity = 0)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public bool IsFull()
    {
        if(item == null) return false;
        return quantity >= maxPerSlot && quantity >= item.maxStack;
    }

    public bool WillBeFullWith(int amount)
    {
        if(item == null) return false;
        return (quantity + amount) > Mathf.Min(maxPerSlot, item.maxStack);
    }

    public bool IsEmpty()
    {
        return quantity <= 0;
    }

    public void AddItem(Item newItem, int amount = 1)
    {
        if (item == null || item == newItem)
        {
            item = newItem;
            quantity += amount;
        }
    }

    public void RemoveItem(int amount = 1)
    {
        quantity -= amount;
        if (quantity < 0) quantity = 0;
        if (quantity == 0) item = null;
    }

    public void GetItemInfo(out Item currentItem, out int currentQuantity)
    {
        currentItem = item;
        currentQuantity = quantity;
    }

    public void useObject(GameObject user = null)
    {
        item.Use(user);
    }
}

public class PlayerInventory : MonoBehaviour
{
    public int inventorySize = 5;
    public List<Slot> items = new();
    public GameObject player;
    public int selectedSlot = 0;

    public Image inv1;
    public Image inv2;
    public Image inv3;
    public Image inv4;
    public Image inv5;

    public TextMeshProUGUI selectedInv;

    void Start()
    {
        for(int i = 0; i < inventorySize; i++)
        {
            items.Add(new Slot());
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public bool HasItem(Item item)
    {
        foreach (Slot slot in items)
        {
            if (!slot.IsEmpty() && slot.item == item) return true;
        }
        return false;
    }

    public int AddItem(Item item, int amount = 1) {
        foreach (Slot slot in items)
        {
            if (!slot.IsFull() && (slot.item == null || slot.item == item) && !slot.WillBeFullWith(amount))
            {
                slot.AddItem(item, amount);
                return 0;
            } else if (!slot.IsFull() && (slot.item == null || slot.item == item))
            {
                int spaceLeft = Mathf.Min(slot.maxPerSlot, item.maxStack) - slot.quantity;
                int amountToAdd = Mathf.Min(spaceLeft, amount);
                slot.AddItem(item, amountToAdd);
                return amount - amountToAdd;
            }
            return amount;
        }
        return amount;
    }

    public void RemoveItem(Item item) {
        foreach (Slot slot in items)
        {
            if (!slot.IsEmpty() && slot.item == item)
            {
                slot.RemoveItem();
                return;
            }
        }
    }

    public void GetItemInfo(out Item item, out int quantity, int? slotIndex = null)
    {
        if(slotIndex == null) slotIndex = selectedSlot;
        if (slotIndex >= 0 && slotIndex < items.Count)
        {
            if (items[slotIndex.Value].IsEmpty())
            {
                item = null;
                quantity = 0;
            }
            else items[slotIndex.Value].GetItemInfo(out item, out quantity);
        }
        else
        {
            item = null;
            quantity = 0;
        }
    }

    public void UseItem(int? slotIndex = null)
    {
        if(slotIndex == null) slotIndex = selectedSlot;
        if (slotIndex >= 0 && slotIndex < items.Count)
        {
            Slot slot = items[slotIndex.Value];
            if (slot.IsEmpty()) return;
            slot.useObject(player);

            if (slot.item.itemType == ItemType.Consumable) slot.RemoveItem();
        }
    }

    public void SelectSlot(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            selectedSlot = index;
        }
    }

    public void Update()
    {
        // touches en haut du clavier pour sélectionner les slots 1 à 5
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(4);

        // la roue de la souris pour sélectionner les slots
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) // scroll up
        {
            selectedSlot = (selectedSlot - 1 + inventorySize) % inventorySize;
        }
        else if (scroll < 0f) // scroll down
        {
            selectedSlot = (selectedSlot + 1) % inventorySize;
        }

        // clique gauche pour utiliser l'objet sélectionné
        if (Input.GetMouseButtonDown(0)) UseItem();

        // Met à jour l'UI
        UpdateUI();
    }

    public void UpdateUI()
    {
        Image[] invImages = { inv1, inv2, inv3, inv4, inv5 };
        for (int i = 0; i < inventorySize; i++)
        {
            Slot slot = items[i];
            Image img = invImages[i];
            if (!slot.IsEmpty()) img.sprite = slot.item.icon;
            else img.sprite = null;
            if(i == selectedSlot)
            {
                img.color = Color.yellow;
                selectedInv.text = slot.item != null ? slot.item.itemName + " (x" + slot.quantity + ")" : "Vide";
            } else img.color = Color.white;
        }
    }
}