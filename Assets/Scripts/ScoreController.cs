using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    private int score;

    public static ScoreController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        score = 0;
        UpdateUI();
    }


    public void GetScore(int amount)
    {
        score += amount;
        UpdateUI();
    }


    public int GetScorePrint()
    {
        return score;
    }

    void UpdateUI()
    {
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "0";
    }
}