using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    float score;

    public TextMeshProUGUI scoreText;

    void Start()
    {
        score = 0; // On définit à 0 par défaut
        UpdateUI();
    }

    // Update is called once per frame
    internal void AddScore(int amount)
    {
        Debug.Log("Score augmenté de " + amount);
        score += amount;
        UpdateUI();
    }

    // Met à jour l'UI du score
    void UpdateUI()
    {
        scoreText.text = "Score : " + score;
    }
   
    // Renvoie le score
    public float getScore()
    {
        return score;
    }
}
