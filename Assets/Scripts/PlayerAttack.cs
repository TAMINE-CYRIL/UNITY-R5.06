using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int meleeDamage = 25;
    public float meleeRange = 2f;
    public float meleeCooldown = 0.5f;

    public int gunDamage = 15;
    public float gunRange = 50f;
    public Transform camera;
    private Camera fpsCamera;

    private float lastMeleeTime = 0f;

    void Start()
    {
        fpsCamera = camera.GetComponent<Camera>();
    }

    void Update()
    {
        // Tir au clic gauche
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        // Attaque à l'épée au clic droit
        if (Input.GetButtonDown("Fire2") && Time.time >= lastMeleeTime + meleeCooldown)
        {
            MeleeAttack();
            lastMeleeTime = Time.time;
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, gunRange))
        {
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(gunDamage);
                Debug.Log("Tir sur : " + hit.transform.name);
            }
        }
    }

    void MeleeAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, meleeRange))
        {
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(meleeDamage);
                Debug.Log("Frappe sur : " + hit.transform.name);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (fpsCamera == null) return;
        // Visualiser la portée melee
        Gizmos.color = Color.red;
        Gizmos.DrawLine(fpsCamera.transform.position, fpsCamera.transform.position + fpsCamera.transform.forward * meleeRange);
        // Visualiser la portée gun
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(fpsCamera.transform.position, fpsCamera.transform.position + fpsCamera.transform.forward * gunRange);
    }
}
