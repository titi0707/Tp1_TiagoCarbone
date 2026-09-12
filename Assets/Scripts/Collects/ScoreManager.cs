using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text scoreText; 
    private int score = 0;
    private int totalCollectibles;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        totalCollectibles = FindObjectsOfType<Collectible>().Length;
        UpdateUI();
    }

    public void AddPoint()
    {
        score++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Joyas: " + score + " / " + totalCollectibles;
        }
    }
}