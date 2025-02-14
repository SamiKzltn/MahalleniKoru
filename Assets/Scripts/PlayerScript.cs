using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    public int playerMoney = 1000;

    public GameObject currentWeapon1;
    public GameObject currentWeapon2;

    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;       
    }

    public bool CanAfford(int weaponPrice)
    {
        return playerMoney >= weaponPrice;
    }
    public void SpendMoney(int weaponPrice) 
    {
        playerMoney -= weaponPrice;
    }
}
