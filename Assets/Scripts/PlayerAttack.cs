using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int meleeDamage = 25;
    public float meleeRange = 2f;
    public float meleeCooldown = 0.5f;

    public int gunDamage = 15;
    public float gunRange = 50f;
    public new Transform camera;
    private Camera fpsCamera;

    private float lastMeleeTime = 0f;

    void Start()
    {
        fpsCamera = camera.GetComponent<Camera>();
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
        if(Time.time < lastMeleeTime + meleeCooldown) return;
        lastMeleeTime = Time.time;
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
