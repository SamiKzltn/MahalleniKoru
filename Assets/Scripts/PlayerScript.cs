using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static int playerMoney = 2000;

    public GameObject currentWeapon1;
    public GameObject currentWeapon2;

    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;

    public bool CanAfford(int weaponPrice)
    {
        return playerMoney >= weaponPrice;
    }
    public void SpendMoney(int weaponPrice) 
    {
        playerMoney -= weaponPrice;
    }
}
