using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    float score;

    public TextMeshProUGUI scoreText;

    void Start()
    {
        score = 0;
        UpdateUI();
    }

    // Update is called once per frame
    internal void AddScore(int amount)
    {
        Debug.Log("Score augmenté de " + amount);
        score += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score : " + score;
    }
}
