using UnityEngine;

public enum ItemType
{
    Key,
    Consumable,
    Weapon,
}

public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public int maxStack = 5;
    public int ID;

    // A override
    public virtual void Use(GameObject user)
    {
        Debug.Log($"Usage de {itemName}");
    }
}