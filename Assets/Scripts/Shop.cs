using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] protected Gun_Scriptable Gun_Scriptable;

    public PlayerScript player;
    public TextMeshProUGUI moneyText;
    public Button buyAKButton, buySniperButton;
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
        AkButton.onClick.AddListener(PurchaseWeapon);
        SniperButton.onClick.AddListener(PurchaseWeapon);
        UpdateButtonState();
        UpdateUI();
    }
    [SerializeField] private Button AkButton, SniperButton; // Satýn alma butonu
    [SerializeField] private int weaponPrice = 100; // Silahýn fiyatý
    private void PurchaseWeapon()
    {
        if (player.CanAfford(weaponPrice)) // Oyuncunun yeterli parasý var mý?
        {
            player.SpendMoney(weaponPrice); // Parayý düþ
            Gun_Scriptable.Is_Purchished = true; // Silahýn satýn alýndýðýný iþaretle
            //buyButton.interactable = false; // Butonu devre dýþý býrak

            Debug.Log($"{Gun_Scriptable.gunName} satýn alýndý!");
        }
        else
        {
            Debug.Log("Yeterli paranýz yok!");
        }
    }

    private void UpdateButtonState()
    {
        //buyButton.interactable = !Gun_Scriptable.Is_Purchished; // Satýn alýndýysa butonu kapat
    }
    void UpdateUI()
    {
        moneyText.text = "Para: " + PlayerScript.playerMoney + " $";
    }
}