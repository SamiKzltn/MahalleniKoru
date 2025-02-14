using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewGun", menuName = "Gun")]
public class Gun_Scriptable : ScriptableObject
{
    public string gunName;
    
    public int Range;
    public int Price;
    public int magazineCapacity;
    public float shotFrequency;

    public Sprite Image;
    public bool Is_Purchished;

    public AudioClip GunSound;

    
}
