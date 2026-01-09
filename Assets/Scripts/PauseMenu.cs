using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;

    public GameOverMenu gameOverMenu;
    public WinMenu winMenu;
    public StartMenu startMenu;

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (IsAnyBlockingMenuActive())
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    private bool IsAnyBlockingMenuActive()
    {
        if (startMenu != null && startMenu.gameObject.activeInHierarchy)
            return true;

        if (gameOverMenu != null && gameOverMenu.gameOverPanel.activeSelf)
            return true;

        if (winMenu != null && winMenu.victoryPanel.activeSelf)
            return true;

        return false;
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;

        DisableGameplay();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        EnableGameplay();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisableGameplay()
    {
        if (playerMovement != null) playerMovement.enabled = false;
        if (playerAttack != null) playerAttack.enabled = false;
    }

    private void EnableGameplay()
    {
        if (playerMovement != null) playerMovement.enabled = true;
        if (playerAttack != null) playerAttack.enabled = true;
    }
}
