using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public PlayerScript player;
    public TextMeshProUGUI moneyText;
    public Button buyAKButton, buyShotgunButton, buySniperButton;

    public TextMeshProUGUI shotgunPriceTag;
    public TextMeshProUGUI magnumPriceTag;
    public TextMeshProUGUI sniperPriceTag;
    public TextMeshProUGUI akPriceTag;

    public void Begining()
    {
        shotgunPriceTag.text = "2000";
        magnumPriceTag.text = "1000";
        sniperPriceTag.text = "3000";
        akPriceTag.text = "4000";
    }
    void Start()
    {
        Begining();
        UpdateUI();

        // Butonlara týklanýnca silah satýn alma iþlemi gerçekleþsin
    }

    void BuyWeapon(string weaponName, int cost)
    {
    }

    void UpdateUI()
    {
        moneyText.text = "Para: " + PlayerScript.playerMoney + " $";
    }
}