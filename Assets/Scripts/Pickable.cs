using UnityEngine;

public class Pickable : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger avec : " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Ramassé par le joueur !");
            // plus tard : ajouter du score ici
            audioSource.Play();

            // delai de 200ms avant de setactive a false pour laisser le temps au son de jouer
            Invoke("DisableObject", 0.1f);

            void DisableObject()
            {
                gameObject.SetActive(false);
            }
        }
    }
}
