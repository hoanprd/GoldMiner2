using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    LevelManager lvManager;

    public Text scoreText;  // Hiển thị điểm số
    public Text timeText;   // Hiển thị thời gian còn lại
    public Text levelText;
    public Text targetText;
    public GameObject bombUI, skipButton, endGamePanel, loseGamePanel;
    public Text bombAmongText;

    //fix singeton kh khởi tạo UI
    public static bool updateScore, levelTargetDone;
    public static int scoreValue;

    public float timeLimit;  // Thời gian tối đa (giây)
    private float remainingTime;   // Thời gian còn lại

    private void Start()
    {
        lvManager = FindObjectOfType<LevelManager>();
        updateScore = false;
        levelTargetDone = false;

        if (PlayerPrefs.GetInt("BuySandClock") == 1)
        {
            timeLimit += GameManager.Instance.clockSandTime;
        }
        remainingTime = timeLimit;  // Đặt thời gian ban đầu
        levelText.text = "Level " + (PlayerPrefs.GetInt("Level") + 1).ToString();
        targetText.text = "$" + lvManager.levelTarget[PlayerPrefs.GetInt("Level")] + "/";
        UpdateScoreText(GameManager.Instance.GetScore()); // Cập nhật điểm khi bắt đầu
    }

    private void Update()
    {
        // Kiểm tra nếu thời gian còn lại > 0
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime; // Giảm thời gian còn lại mỗi frame
            UpdateTimeText(); // Cập nhật hiển thị thời gian
            if (PlayerPrefs.GetInt("BuyBomb") > 0)
            {
                bombUI.SetActive(true);
                bombAmongText.text = PlayerPrefs.GetInt("BuyBomb").ToString();
            }
            else
            {
                bombUI.SetActive(false);
            }

            // Nhận điểm từ GameManager sẽ update score
            if (updateScore)
            {
                updateScore = false;
                UpdateScoreText(scoreValue);
            }

            if (levelTargetDone)
            {
                skipButton.SetActive(true);
            }
        }
        else if (remainingTime <= 0 && !GameManager.Instance.IsGameOver)
        {
            // Khi hết thời gian, gọi GameOver trong GameManager
            if (!GameManager.Instance.IsGameOver && !levelTargetDone)
            {
                GameManager.Instance.GameOver(); // Kết thúc trò chơi
                loseGamePanel.SetActive(true);
                StartCoroutine(delayGoToMenu());
            }
            else
            {
                if (PlayerPrefs.GetInt("Level") < LevelManager.levelEndGame - 1)
                {
                    GameManager.Instance.LevelPass();
                }
                else
                {
                    GameManager.Instance.GameOver();
                    endGamePanel.SetActive(true);
                    StartCoroutine(delayGoToMenu());
                }
            }
        }
    }

    // Cập nhật thời gian hiển thị trên UI
    private void UpdateTimeText()
    {
        timeText.text = Mathf.Max(0, Mathf.FloorToInt(remainingTime)).ToString();
    }

    // Cập nhật điểm số hiển thị trên UI
    public void UpdateScoreText(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = "$ " + newScore.ToString();
        }
    }

    public void SkipLevel()
    {
        if (PlayerPrefs.GetInt("Level") < LevelManager.levelEndGame - 1)
        {
            GameManager.Instance.LevelPass();
        }
        else
        {
            endGamePanel.SetActive(true);
            GameManager.Instance.GameOver();
            StartCoroutine(delayGoToMenu());
        }
    }

    IEnumerator delayGoToMenu()
    {
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("Level", 0);
        //MenuManager.Instance.PlaySound(MenuManager.Instance.levelBGM, false);
        Destroy(GameManager.Instance);
        //Destroy(MenuManager.Instance);
        SceneManager.LoadScene("MenuScene");
    }
}