using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerKillX : MonoBehaviour
{
    // Appelé automatiquement lorsqu'un autre collider entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant a un composant KillableX
        KillableX killable = other.GetComponent<KillableX>();

        if (killable != null)
        {
            killable.Kill();
        }
        else
        {
            Debug.Log(other.name + " n'est pas un KillableX");
        }
    }
}