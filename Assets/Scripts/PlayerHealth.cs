using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    private bool isDead = false;

    [Header("UI")]
    private Canvas playerCanvas;
    public RectTransform healthBarFill;
    public TextMeshProUGUI healthText;

    [Header("Game Over")]
    public GameOverMenu gameOverMenu;

    void Start()
    {
        currentHealth = maxHealth;

        playerCanvas = GameObject.Find("PlayerCanvas").GetComponent<Canvas>();

        healthBarFill = FindChildByName(playerCanvas.transform, "HealthBar_Fill")
            .GetComponent<RectTransform>();

        healthText = FindChildByName(playerCanvas.transform, "Health_Text_Max")
            .GetComponent<TextMeshProUGUI>();

        UpdateUI();
    }

    public void TakeDamage(float amount)
    {
        // 🔒 Sécurité : pas de dégâts si déjà mort
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        Debug.Log($"Santé du joueur : {currentHealth} (-{amount})");

        UpdateUI();

        if (currentHealth <= 0f)
            Die();
    }

    void Die()
    {
        if (isDead) return; // double sécurité

        isDead = true;
        Debug.Log("PLAYER MORT");

        // Affichage Game Over
        if (gameOverMenu != null)
            gameOverMenu.ShowGameOver();

        // Bloquer le joueur (sans tout désactiver)
        DisablePlayerControls();
    }

    void DisablePlayerControls()
    {
        // Exemple : bloquer le CharacterController
        var controller = GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = false;
    }

    void UpdateUI()
    {
        healthText.text = $"{currentHealth} / {maxHealth}";

        float percentLife = currentHealth / maxHealth;
        healthBarFill.anchorMin = new Vector2(0f, healthBarFill.anchorMin.y);
        healthBarFill.anchorMax = new Vector2(percentLife, healthBarFill.anchorMax.y);
    }

    Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform result = FindChildByName(child, name);
            if (result != null)
                return result;
        }
        return null;
    }
}
