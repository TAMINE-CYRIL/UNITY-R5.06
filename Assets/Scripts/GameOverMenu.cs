using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverPanel;
    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;
    public StartMenu startMenu;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        playerMovement.enabled = false;
        playerCombat.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Reset()
    {
        Time.timeScale = 1f;

        playerMovement.enabled = true;
        playerCombat.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("SampleScene");
        startMenu.PlayGame();
    } 
}
