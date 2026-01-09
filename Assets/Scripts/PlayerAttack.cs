using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Références")]
    public Camera fpsCamera;

    [Header("Dégâts de base")]
    public int baseGunDamage = 15;
    public int baseMeleeDamage = 25;

    [Header("Portées")]
    public float gunRange = 50f;
    public float meleeRange = 2f;

    [Header("Cooldowns")]
    public float shootCooldown = 0.25f;
    public float meleeCooldown = 0.6f;

    private float lastShootTime;
    private float lastMeleeTime;

    private PlayerInventory inventory;
    private WeaponItem equippedWeapon;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();

        if (fpsCamera == null)
            fpsCamera = Camera.main;
    }

    void Update()
    {
        UpdateEquippedWeapon();

        if (Input.GetMouseButtonDown(0))
            TryShoot();

        if (Input.GetMouseButtonDown(1))
            TryMelee();
    }



    void UpdateEquippedWeapon()
    {
        inventory.GetItemInfo(out Item item, out int quantity);

        if (item != null && item is WeaponItem weapon)
        {
            if (equippedWeapon != weapon)
            {
                equippedWeapon = weapon;
                Debug.Log("Arme équipée : " + weapon.itemName);
            }
        }
        else
        {
            equippedWeapon = null;
        }
    }

    int GetGunDamage()
    {
        if (equippedWeapon != null)
            return baseGunDamage + equippedWeapon.bonusDamage;

        return baseGunDamage;
    }


    void TryShoot()
    {
        if (Time.time < lastShootTime + shootCooldown)
            return;

        lastShootTime = Time.time;

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, gunRange))
        {
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                int damage = GetGunDamage();
                enemy.TakeDamage(damage);

                Debug.Log("Tir sur " + hit.transform.name + " | dégâts : " + damage);
            }
        }
    }



    void TryMelee()
    {
        if (Time.time < lastMeleeTime + meleeCooldown)
            return;

        lastMeleeTime = Time.time;

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, meleeRange))
        {
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(baseMeleeDamage);
                Debug.Log("Coup de mêlée sur " + hit.transform.name + " | dégâts : " + baseMeleeDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (fpsCamera == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(fpsCamera.transform.position,
                        fpsCamera.transform.position + fpsCamera.transform.forward * meleeRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(fpsCamera.transform.position,
                        fpsCamera.transform.position + fpsCamera.transform.forward * gunRange);
    }
}
