using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private Canvas playerCanvas;
    public RectTransform healthBarFill;  // Assigne ta barre du joueur (HUD)
    public TextMeshProUGUI healthText;              // Assigne ton texte de santé (HUD)

    void Start()
    {
        currentHealth = maxHealth;

        playerCanvas = GameObject.Find("PlayerCanvas").GetComponent<Canvas>();

        healthBarFill = FindChildByName(playerCanvas.transform, "HealthBar_Fill").GetComponent<RectTransform>();
        healthText = FindChildByName(playerCanvas.transform, "Health_Text_Max").GetComponent<TextMeshProUGUI>();

        UpdateUI();
    }

    Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name) return child;

            Transform result = FindChildByName(child, name);
            if (result != null) return result;
        }
        return null;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Empêche la santé de descendre en dessous de 0

        Debug.Log("Santé du joueur : " + currentHealth + "(" + amount + " dégâts reçus)");

        UpdateUI();

        if (currentHealth <= 0) Die();
    }

    void UpdateUI()
    {
        healthText.text = currentHealth + " / " + maxHealth;
        float percentLife = currentHealth / maxHealth;
        healthBarFill.anchorMin = new Vector2(0f, healthBarFill.anchorMin.y);
        healthBarFill.anchorMax = new Vector2(percentLife, healthBarFill.anchorMax.y);
    }

    void Die()
    {
        Debug.Log("PLAYER MORT");
        // Pour l'instant on desactive le joueur
        gameObject.SetActive(false);
    }
}
