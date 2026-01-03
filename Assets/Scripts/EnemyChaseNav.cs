using UnityEngine;
using UnityEngine.AI;

public class EnemyChaserNav : MonoBehaviour
{
    public Transform target;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (target == null) return;

        // Toujours poursuivre le joueur
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }
}