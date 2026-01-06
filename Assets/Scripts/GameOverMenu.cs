using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverPanel;
    public MonoBehaviour playerLook;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        if (playerLook != null)
            playerLook.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
