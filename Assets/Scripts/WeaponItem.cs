using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon")]
public class WeaponItem : Item
{
    [Header("Stats de l'arme")]
    public int bonusDamage = 25;

    private void OnEnable()
    {
        itemType = ItemType.Weapon;
        maxStack = 1;
    }

    public override void Use(GameObject user)
    {
        Debug.Log("Weapon selected: " + itemName);
    }

}
