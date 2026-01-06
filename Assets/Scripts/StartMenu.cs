using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenuPanel;
    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;

    private void Start()
    {
        ShowStartMenu();
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("SampleScene");
        ShowStartMenu();
    }

    public void ShowStartMenu()
    {
        startMenuPanel.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playerMovement.enabled = false;
        playerCombat.enabled = false;
    }

    public void PlayGame()
    {
        // Reprendre le jeu
        startMenuPanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerMovement.enabled = true;
        playerCombat.enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitter le jeu");
    }
}