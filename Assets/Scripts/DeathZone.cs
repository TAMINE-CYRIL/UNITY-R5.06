using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public AudioClip deathSound; 
    public float volume = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (deathSound != null)
            {
                AudioSource.PlayClipAtPoint(deathSound, transform.position, volume);
                Invoke(nameof(ReloadScene), deathSound.length);
            }
            else
            {
                ReloadScene();
            }
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
