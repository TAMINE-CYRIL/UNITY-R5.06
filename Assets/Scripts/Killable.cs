using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillableX : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Méthode publique pour "tuer" l'objet
    public void Kill()
    {
        Debug.Log(name + " est mort");

        GameObject temp = new GameObject("TempAudio");
        AudioSource tempS = temp.AddComponent<AudioSource>();

        tempS.clip = audioSource.clip;
        tempS.volume = audioSource.volume;
        tempS.spatialBlend = audioSource.spatialBlend;

        tempS.Play();
        Destroy(temp, tempS.clip.length);

        Destroy(gameObject);
    }
}