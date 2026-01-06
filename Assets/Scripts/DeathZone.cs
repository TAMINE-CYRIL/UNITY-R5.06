using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public AudioClip deathSound; 
    public float volume = 1f;
    public GameOverMenu gameOverMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (deathSound != null)
            {
                AudioSource.PlayClipAtPoint(deathSound, transform.position, volume);
                Invoke(nameof(gameOverMenu.ShowGameOver), deathSound.length);
            }
            else gameOverMenu.ShowGameOver();
        }
    }
}
