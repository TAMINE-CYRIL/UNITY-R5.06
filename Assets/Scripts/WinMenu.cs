using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinMenu : MonoBehaviour
{
    public GameObject victoryPanel;

    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;
    public PlayerScore playerScore;

    public TextMeshProUGUI victoryScoreText;

    // Au démarrage : on cache le panel
    void Start()
    {
        victoryPanel.SetActive(false);
    }

    // Affichage du panel en cas de victoire
    public void ShowVictory()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;

        playerMovement.enabled = false;
        playerCombat.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Affichage du score final
        victoryScoreText.text = "Score final : " + playerScore.getScore();
    }

    // Relancer le jeu, on recharge la scène
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quitter le jeu
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitter le jeu");
    }
}
