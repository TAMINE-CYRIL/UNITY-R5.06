using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenuPanel;
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;

    void Start()
    {
        ShowStartMenu();
    }

    public void ShowStartMenu()
    {
        DisableGameplay();

        startMenuPanel.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        startMenuPanel.SetActive(false);

        EnableGameplay();

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

    private void EnableGameplay()
    {
        if (playerMovement != null) playerMovement.enabled = true;
        if (playerAttack != null) playerAttack.enabled = true;
    }
}
