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
    [SerializeField] private Button AkButton, SniperButton; // Sat�n alma butonu
    [SerializeField] private int weaponPrice = 100; // Silah�n fiyat�
    private void PurchaseWeapon()
    {
        if (player.CanAfford(weaponPrice)) // Oyuncunun yeterli paras� var m�?
        {
            player.SpendMoney(weaponPrice); // Paray� d��
            Gun_Scriptable.Is_Purchished = true; // Silah�n sat�n al�nd���n� i�aretle
            //buyButton.interactable = false; // Butonu devre d��� b�rak

            Debug.Log($"{Gun_Scriptable.gunName} sat�n al�nd�!");
        }
        else
        {
            Debug.Log("Yeterli paran�z yok!");
        }
    }

    private void UpdateButtonState()
    {
        //buyButton.interactable = !Gun_Scriptable.Is_Purchished; // Sat�n al�nd�ysa butonu kapat
    }
    void UpdateUI()
    {
        moneyText.text = "Para: " + PlayerScript.playerMoney + " $";
    }
}