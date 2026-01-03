using UnityEngine;

public class EnemyAttack : MonoBehaviour 
{
    private Transform player;

    // Distance d'attaque : 2
    public float attackDistance = 2f;
    // Délai entre les attaques : 1.5 secondes
    public float attackCooldown = 1.5f;
    // Dégâts infligés par l'ennemi : 10
    public float damage = 10f;

    // Dernier temps d'attaque
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackDistance && Time.time >= lastAttackTime + attackCooldown)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }
}