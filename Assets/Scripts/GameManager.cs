using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playLogo;
    public GameObject winPanel;
    public GameObject scoreUI;
    public GameObject spawn;
    public bool IsRestarting = false;
    public TMP_Text winScoreText;

    public int lives = 3;
    public TMP_Text livesText;
    public GameObject losePanel;
    public TMP_Text loseScoreText;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ResetToMenu();
    }

    
    public void StartGame()
    {
        lives = 3;
        UpdateLivesUI();
        playLogo.SetActive(false);
        scoreUI.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        spawn.SetActive(true);

        Time.timeScale = 1f;
    }

    
    public void WinGame(int finalScore)
    {
        Debug.Log("WinGame Called");

        winPanel.SetActive(true);
        winScoreText.text = "Score: " + finalScore;

        Time.timeScale = 0f;
    }

    public void LoseGame()
    {
        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
        else
        {
            // Fallback to winPanel if losePanel is not set
            winPanel.SetActive(true);
        }

        if (loseScoreText != null)
        {
            loseScoreText.text = "Score: " + ScoreController.Instance.GetScorePrint();
        }
        else if (winScoreText != null)
        {
            winScoreText.text = "Score: " + ScoreController.Instance.GetScorePrint();
        }

        Time.timeScale = 0f;
    }

    public void DecrementLife()
    {
        lives--;
        UpdateLivesUI();
        if (lives > 0)
        {
            ShipControllerScript.Instance.SpawnShip();
        }
        else
        {
            LoseGame();
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
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
        scoreUI.SetActive(false);
    }
}