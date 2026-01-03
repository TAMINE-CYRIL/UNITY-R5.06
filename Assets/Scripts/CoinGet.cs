using UnityEngine;

public class CoinGet : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Méthode pour "tuer" l'objet
    public void Kill(Transform collector)
    {
        Debug.Log(name + " est récolté");

        PlayerScore playerScore = collector.GetComponent<PlayerScore>();
        playerScore.AddScore(1); // Ajoute 1 point au score du joueur

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
