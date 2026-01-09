using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinMenu : MonoBehaviour
{
    public GameObject victoryPanel;

    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    public PlayerScore playerScore;

    public TextMeshProUGUI victoryScoreText;

    void Start()
    {
        victoryPanel.SetActive(false);
    }

    public void ShowVictory()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;

        DisableGameplay();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerScore != null)
            victoryScoreText.text = "Score final : " + playerScore.getScore();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitter le jeu");
    }

    private void DisableGameplay()
    {
        if (playerMovement != null) playerMovement.enabled = false;
        if (playerAttack != null) playerAttack.enabled = false;
    }
}
