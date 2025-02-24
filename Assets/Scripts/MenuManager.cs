using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public GameObject continueButton, settingPanel, onBGMButton, offBGMButton, onFXButton, offFXButton;
    public AudioSource menuBGM, levelBGM, clickFX, pullFX, bombFX;
    public bool canPlayBGM, canPlayFX;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("BGM"))
        {
            PlayerPrefs.SetInt("BGM", 1);
        }

        if (!PlayerPrefs.HasKey("FX"))
        {
            PlayerPrefs.SetInt("FX", 1);
        }

        if (PlayerPrefs.GetInt("BGM") == 1)
        {
            canPlayBGM = true;
            onBGMButton.SetActive(true);
            offBGMButton.SetActive(false);
        }
        else
        {
            canPlayBGM = false;
            onBGMButton.SetActive(false);
            offBGMButton.SetActive(true);
        }

        if (PlayerPrefs.GetInt("FX") == 1)
        {
            canPlayFX = true;
            onFXButton.SetActive(true);
            offFXButton.SetActive(false);
        }
        else
        {
            canPlayFX = false;
            onFXButton.SetActive(false);
            offFXButton.SetActive(true);
        }

        PlaySound(menuBGM, canPlayBGM);

        if (PlayerPrefs.GetInt("Level") > 0)
        {
            continueButton.SetActive(true);
        }
    }

    public void NewGame()
    {
        PlaySound(menuBGM, false);
        PlaySound(clickFX, canPlayFX);
        PlaySound(levelBGM, canPlayBGM);
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("BuySandClock", 0);
        PlayerPrefs.SetInt("BuyPower", 0);
        PlayerPrefs.SetInt("BuyBomb", 0);
        PlayerPrefs.SetInt("BuyLuckyUpValue", 0);
        PlayerPrefs.SetInt("BuyDiamondValue", 0);
        PlayerPrefs.SetInt("BuyRockValue", 0);
        SceneManager.LoadScene("LevelScene");
    }

    public void Continue()
    {
        PlaySound(menuBGM, false);
        PlaySound(clickFX, canPlayFX);
        PlaySound(levelBGM, canPlayBGM);
        SceneManager.LoadScene("LevelScene");
    }

    public void OpenSetting()
    {
        PlaySound(clickFX, canPlayFX);
        settingPanel?.SetActive(true);
    }

    public void CloseSetting()
    {
        PlaySound(clickFX, canPlayFX);
        settingPanel?.SetActive(false);
    }

    public void OffBGM()
    {
        PlayerPrefs.SetInt("BGM", 1);
        canPlayBGM = true;
        PlaySound(clickFX, canPlayFX);
        PlaySound(menuBGM, canPlayBGM);
        onBGMButton?.SetActive(true);
        offBGMButton?.SetActive(false);
    }

    public void OnBGM()
    {
        PlayerPrefs.SetInt("BGM", 0);
        canPlayBGM = false;
        PlaySound(clickFX, canPlayFX);
        PlaySound(menuBGM, canPlayBGM);
        onBGMButton?.SetActive(false);
        offBGMButton?.SetActive(true);
    }

    public void OffFX()
    {
        PlayerPrefs.SetInt("FX", 1);
        canPlayFX = true;
        PlaySound(clickFX, canPlayFX);
        onFXButton?.SetActive(true);
        offFXButton?.SetActive(false);
    }

    public void OnFX()
    {
        PlayerPrefs.SetInt("FX", 0);
        canPlayFX = false;
        PlaySound(clickFX, canPlayFX);
        onFXButton?.SetActive(false);
        offFXButton?.SetActive(true);
    }

    public void PlaySound(AudioSource audio, bool canPlay)
    {
        if (canPlay)
        {
            audio.Play();
        }
        else
        {
            audio.Stop();
        }
    }
}