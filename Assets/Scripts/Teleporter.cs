using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        // V�rifie que c'est bien le joueur
        if (!other.CompareTag("Player"))
            return;
        Debug.Log("Teleporting player...");

        // Téléportation selon le type de composant
        CharacterController controller = other.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            other.transform.position = destination.position;
            other.transform.rotation = destination.rotation;
            controller.enabled = true;
        }
    }
}
