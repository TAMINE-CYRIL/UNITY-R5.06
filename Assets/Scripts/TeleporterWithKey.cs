using UnityEngine;

public class TeleporterWithKey : MonoBehaviour
{
    [Header("Destination")]
    public Transform destination;

    [Header("Clé requise (sera consommée)")]
    public Item requiredKey;

    private void OnTriggerEnter(Collider other)
    {


        if (!other.CompareTag("Player"))
        {
            Debug.Log("REFUS : ce n'est pas le Player");
            return;
        }


        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null)
        {
            Debug.Log("ERREUR : PlayerInventory introuvable sur le Player");
            return;
        }


        if (requiredKey == null)
        {
            Debug.LogError("Aucune clé requise assignée au téléporteur");
            return;
        }

        Debug.Log("Clé requise par le téléporteur : " + requiredKey.itemName);

        bool hasKey = inventory.HasItem(requiredKey);

        if (!hasKey)
        {
            Debug.Log("TELEPORTATION BLOQUÉE : clé manquante");
            return;
        }

        Debug.Log("CLÉ VALIDÉE : début de la téléportation");

        CharacterController controller = other.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }
        else
        {
            Debug.LogWarning("WARNING : aucun CharacterController trouvé");
        }


        other.transform.position = destination.position;


        if (controller != null)
        {
            controller.enabled = true;
        }

        Debug.Log("Consommation de la clé : " + requiredKey.itemName);
        inventory.RemoveItem(requiredKey);

        Debug.Log("Téléportation TERMINÉE avec succès");
    }
}
