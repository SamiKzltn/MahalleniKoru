using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    public int Health;
    public int playerMoney; // Oyuncunun para miktar�
    public Dictionary<int, int> AllAmmo;
    public Dictionary<int, int> MagazineAmmo;
    public Vector3 position;
    

    // Silahlar i�in bir liste olu�turduk

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        AllAmmo = new Dictionary<int, int>();
        MagazineAmmo = new Dictionary<int, int>();

        Health = 100;
    }

    // Para kontrol�
    public bool CanAfford(int weaponPrice)
    {
        return playerMoney >= weaponPrice;
    }

    // Paray� harcama
    public void SpendMoney(int weaponPrice)
    {
        playerMoney -= weaponPrice;
    }

    // Silah�n mermi miktar�n� g�ncelleme
    public void UpdateWeaponAmmo(int weaponIndex, int ammoCount)
    {
        if (AllAmmo.ContainsKey(weaponIndex))
        {
            AllAmmo[weaponIndex] = ammoCount;
        }
    }

    // �arj�rdeki mermi miktar�n� g�ncelleme
    public void UpdateMagazineAmmo(int weaponIndex, int ammoCount)
    {
        if (MagazineAmmo.ContainsKey(weaponIndex))
        {
            MagazineAmmo[weaponIndex] = ammoCount;
        }
        else
        {
            Debug.LogWarning("�arj�r i�in Silah ID'si bulunamad�: " + weaponIndex);
        }
    }
}
