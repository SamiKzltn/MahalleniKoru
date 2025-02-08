using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey("OyunBasladi"))
        {
            PlayerPrefs.SetInt("Ak-47_Mermi", 980);
            PlayerPrefs.SetInt("Pompali_Mermi", 15);
            PlayerPrefs.SetInt("Magnum_Mermi", 50);
            PlayerPrefs.SetInt("Sniper_Mermi", 30);

            PlayerPrefs.SetInt("OyunBasladi",1);
        }
        
    }
    void Update()
    {
        
    }
}
