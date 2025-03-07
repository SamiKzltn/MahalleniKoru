using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoScripts : MonoBehaviour
{
    int[] GunID =
    {
        2030,
        2040,
        2050,
        2020
    };

    int[] AmmoTypes =
    {
        5,
        10,
        25,
        50,
    };

    public List<Sprite> Gun_Sprites = new List<Sprite>();

    public Image Gun_Image;

    public int gunId;
    public int Ammo_Type;
    public int Pointi;
    
    void Start()
    {
        int NumberKey = Random.Range(0, 4);
        gunId = GunID[NumberKey];
        Ammo_Type = AmmoTypes[Random.Range(0, AmmoTypes.Length)];
        Gun_Image.sprite = Gun_Sprites[NumberKey];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
