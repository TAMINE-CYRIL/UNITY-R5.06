using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie que c'est bien le joueur
        if (!other.CompareTag("Player"))
            return;

        // Téléportation
        other.transform.position = destination.position;
    }
}
