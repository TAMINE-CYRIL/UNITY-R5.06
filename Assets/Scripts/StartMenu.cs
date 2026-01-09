using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenuPanel;
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;

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
        playerAttack.enabled = false;
    }

    public void PlayGame()
    {
        // Reprendre le jeu
        startMenuPanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerMovement.enabled = true;
        playerAttack.enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitter le jeu");
    }
}