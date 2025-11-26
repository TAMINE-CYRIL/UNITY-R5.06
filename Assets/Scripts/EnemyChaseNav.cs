using UnityEngine;
using UnityEngine.AI;

public class EnemyChaserNav : MonoBehaviour
{
    public Transform target;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // On cherche le joueur s'il n'est pas assigné
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
                Debug.Log("EnemyChaserNav : target = " + target.name);
            }
            else
            {
                Debug.LogError("EnemyChaserNav : aucun objet avec le tag Player trouvé !");
            }
        }
    }

    void Update()
    {
        if (target == null) return;

        // Toujours poursuivre le joueur
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }
}
