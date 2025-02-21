using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int clockSandTime, luckRate, diamondValue, rockValue;
    public int itemHookingIndex;
    public float powerValue;

    private int score;
    public bool IsGameOver { get; private set; } // Kiểm tra trạng thái game over

    //UIManager uiManager; // Tham chiếu đến UIManager
    LevelManager lvManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Đảm bảo GameManager không bị hủy khi cảnh thay đổi
        }
        else
        {
            Destroy(gameObject); // Nếu có một instance khác, hủy object này
        }

        //uiManager = FindObjectOfType<UIManager>();
        lvManager = FindObjectOfType<LevelManager>();

        SetScore();
        UIManager.updateScore = true;
        UIManager.scoreValue = score;
    }

    public void AddScore(int points)
    {
        if (!IsGameOver) // Chỉ cộng điểm nếu chưa game over
        {
            /*if (itemHookingIndex == 2 && PlayerPrefs.GetInt("BuyRockValue") == 1)
            {
                //rock x2
                score += points * 2;
                itemHookingIndex = 0;
            }
            else if (itemHookingIndex == 1 && PlayerPrefs.GetInt("BuyDiamondValue") == 1)
            {
                //diamond x2
                score += points * 2;
                itemHookingIndex = 0;
            }
            else
            {
                score += points;
            }*/

            score += points;

            //uiManager.UpdateScoreText(score); // Cập nhật UI sau khi cộng điểm
            UIManager.updateScore = true;
            UIManager.scoreValue = score;
            CheckLevelTargetPass();
        }
    }

    public void SetScore()
    {
        score = PlayerPrefs.GetInt("Score");
    }

    public void UpdateScore()
    {
        PlayerPrefs.SetInt("Score", GetScore());
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        IsGameOver = true; // Đánh dấu trò chơi kết thúc
        Debug.Log("Game Over!");
    }

    public void CheckLevelTargetPass()
    {
        if (GetScore() >= lvManager.levelTarget[PlayerPrefs.GetInt("Level")])
        {
            UIManager.levelTargetDone = true;
        }
    }

    public void LevelPass()
    {
        if (PlayerPrefs.GetInt("Level") < LevelManager.levelEndGame)
        {
            PlayerPrefs.SetInt("Score", GetScore());
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            PlayerPrefs.SetInt("BuySandClock", 0);
            PlayerPrefs.SetInt("BuyPower", 0);
            PlayerPrefs.SetInt("BuyDiamondValue", 0);
            PlayerPrefs.SetInt("BuyLuckyUpValue", 0);
            PlayerPrefs.SetInt("BuyRockValue", 0);
            SceneManager.LoadScene("ShopScene");
        }
    }
}