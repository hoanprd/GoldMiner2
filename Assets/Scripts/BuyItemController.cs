using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemController : MonoBehaviour
{
    public int costValue;
    public GameObject buyButton;

    public void BuySandClock()
    {
        if (GameManager.Instance.GetScore() >= costValue)
        {
            ShopManager.buyItemFromShop = true;
            PlayerPrefs.SetInt("BuySandClock", 1);
            BuyInteract();
        }
        else
        {
            ShopManager.buyFail = true;
        }
    }

    public void BuyBomb()
    {
        if (GameManager.Instance.GetScore() >= costValue)
        {
            ShopManager.buyItemFromShop = true;
            PlayerPrefs.SetInt("BuyBomb", PlayerPrefs.GetInt("BuyBomb") + 1);
            BuyInteract();
        }
        else
        {
            ShopManager.buyFail = true;
        }
    }

    public void BuyPower()
    {
        if (GameManager.Instance.GetScore() >= costValue)
        {
            ShopManager.buyItemFromShop = true;
            PlayerPrefs.SetInt("BuyPower", 1);
            BuyInteract();
        }
        else
        {
            ShopManager.buyFail = true;
        }
    }

    public void BuyDiamondValue()
    {
        if (GameManager.Instance.GetScore() >= costValue)
        {
            ShopManager.buyItemFromShop = true;
            PlayerPrefs.SetInt("BuyDiamondValue", 1);
            BuyInteract();
        }
        else
        {
            ShopManager.buyFail = true;
        }
    }

    public void BuyLuckyUp()
    {
        if (GameManager.Instance.GetScore() >= costValue)
        {
            ShopManager.buyItemFromShop = true;
            PlayerPrefs.SetInt("BuyLuckyUpValue", 1);
            BuyInteract();
        }
        else
        {
            ShopManager.buyFail = true;
        }
    }

    public void BuyRockValue()
    {
        if (GameManager.Instance.GetScore() >= costValue)
        {
            ShopManager.buyItemFromShop = true;
            PlayerPrefs.SetInt("BuyRockValue", 1);
            BuyInteract();
        }
        else
        {
            ShopManager.buyFail = true;
        }
    }

    public void BuyInteract()
    {
        GameManager.Instance.AddScore(-costValue);
        buyButton.SetActive(false);
    }
}