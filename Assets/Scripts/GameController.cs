using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] Weapons;

    public GameObject currentWeapon1;
    public GameObject currentWeapon2;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("OyunBasladi"))
        {
            PlayerPrefs.SetInt("Taramali_Mermi", 980);
            PlayerPrefs.SetInt("Pompali_Mermi", 15);
            PlayerPrefs.SetInt("Magnum_Mermi", 50);
            PlayerPrefs.SetInt("Sniper_Mermi", 30);

            PlayerPrefs.SetInt("OyunBasladi",1);
        }
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon2.SetActive(false);
            currentWeapon1.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon1.SetActive(false);
            currentWeapon2.SetActive(true);
        }
    }
}
