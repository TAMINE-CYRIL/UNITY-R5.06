using UnityEngine;

public class Pickable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger avec : " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Ramassé par le joueur !");
            // plus tard : ajouter du score ici

            Destroy(gameObject);
        }
    }
}
