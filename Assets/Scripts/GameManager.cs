using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playLogo;
    public GameObject winPanel;
    public GameObject losePanel;       // ← KÉO LOSE PANEL VÀO ĐÂY
    public GameObject scoreUI;
    public GameObject spawn;
    public bool IsRestarting = false;
    public TMP_Text winScoreText;
    public TMP_Text loseScoreText;     // ← KÉO LOSE SCORE TEXT VÀO ĐÂY
    public Image[] lifeHearts;         // ← KÉO 4 ICON HEART VÀO ĐÂY (size = 4)

    public int lives = 1;             // BẮT ĐẦU CHỈ 1 MẠNG

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        lives = 1;
        UpdateLifeUI();
        ResetToMenu();
    }

    public void StartGame()
    {
        playLogo.SetActive(false);
        scoreUI.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        spawn.SetActive(true);
        Time.timeScale = 1f;
    }

    public void WinGame(int finalScore)
    {
        winPanel.SetActive(true);
        winScoreText.text = "Score: " + finalScore;
        Time.timeScale = 0f;
    }

    public void LoseLife()
    {
        lives--;
        UpdateLifeUI();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void UpdateLifeUI()
    {
        for (int i = 0; i < lifeHearts.Length; i++)
        {
            lifeHearts[i].enabled = i < lives;
        }
    }

    public void AddLife()
    {
        if (lives < 4)
        {
            lives++;
            UpdateLifeUI();
        }
    }

    public void GameOver()
    {
        Debug.Log("=== GAME OVER ===");
        losePanel.SetActive(true);
        loseScoreText.text = "Score: " + ScoreController.Instance.GetScorePrint();
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        IsRestarting = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ResetToMenu()
    {
        Time.timeScale = 0f;
        playLogo.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        scoreUI.SetActive(false);
    }
}