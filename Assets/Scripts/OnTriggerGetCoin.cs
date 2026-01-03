using UnityEngine;

public class OnTriggerGetCoin : MonoBehaviour
{
    // Appelé automatiquement lorsqu'un autre collider entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant a un composant KillableX
        CoinGet killable = other.GetComponent<CoinGet>();
        if (killable != null) killable.Kill(transform);
    }
}