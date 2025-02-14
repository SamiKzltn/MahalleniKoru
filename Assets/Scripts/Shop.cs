using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] protected Gun_Scriptable Gun_Scriptable;
    [SerializeField] protected Gun_Scriptable[] allguns;

    public PlayerScript player;
    public Button buyButton;

    [SerializeField] private UnityEngine.Color purchasedColor = UnityEngine.Color.green; // Sat�n al�nd���nda olacak renk
    [SerializeField] private UnityEngine.Color defaultColor = UnityEngine.Color.white; // Varsay�lan renk

    [Header("Texts")]
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
        buyButton.onClick.AddListener(PurchaseWeapon);
        UpdateButtonState();
    }
    private void PurchaseWeapon()
    {
        if(Gun_Scriptable.Is_Purchished == true)
        {
            Debug.Log("Zaten Satin Alindi");
        }
        else
        {
            if (player.CanAfford(Gun_Scriptable.Price)) // Oyuncunun yeterli paras� var m�?
            {
                player.SpendMoney(Gun_Scriptable.Price); // Paray� d��
                Gun_Scriptable.Is_Purchished = true; // Silah�n sat�n al�nd���n� i�aretle
                UpdateButtonState();
                Debug.Log($"{Gun_Scriptable.gunName} sat�n al�nd�!");
            }
            else
            {
                Debug.Log("Yeterli paran�z yok!");
            }
        }
    }
    private void UpdateButtonState()
    {
        buyButton.interactable = !Gun_Scriptable.Is_Purchished; // Sat�n al�nd�ysa butonu kapat
    }
}