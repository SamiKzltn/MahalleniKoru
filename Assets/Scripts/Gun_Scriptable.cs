using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "Gun")]
public class Gun_Scriptable : ScriptableObject
{
    public string gunName;
    
    public int Range;
    public int magazineCapacity;
    public float shotFrequency;

    public AudioClip GunSound;

    
}
