using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public RectTransform healthBarFill;
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Empêche la santé de descendre en dessous de 0

        Debug.Log("Santé de l'ennemi : " + currentHealth + "(" + amount + " dégâts reçus)");

        UpdateUI();

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Debug.Log("ENNEMI MORT");
        Destroy(gameObject);
    }

    void UpdateUI()
    {
        healthText.text = currentHealth + " / " + maxHealth;
        float percentLife = (float)currentHealth / (float)maxHealth;
        healthBarFill.anchorMin = new Vector2(0f, healthBarFill.anchorMin.y);
        healthBarFill.anchorMax = new Vector2(percentLife, healthBarFill.anchorMax.y);
    }
}
