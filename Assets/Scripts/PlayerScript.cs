using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    public int Health;
    public int playerMoney; // Oyuncunun para miktarý
    public Dictionary<int, int> AllAmmo;
    public Dictionary<int, int> MagazineAmmo;
    public Vector3 position;
    

    // Silahlar için bir liste oluþturduk

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

    // Para kontrolü
    public bool CanAfford(int weaponPrice)
    {
        return playerMoney >= weaponPrice;
    }

    // Parayý harcama
    public void SpendMoney(int weaponPrice)
    {
        playerMoney -= weaponPrice;
    }

    // Silahýn mermi miktarýný güncelleme
    public void UpdateWeaponAmmo(int weaponIndex, int ammoCount)
    {
        if (AllAmmo.ContainsKey(weaponIndex))
        {
            AllAmmo[weaponIndex] = ammoCount;
        }
    }

    // Þarjördeki mermi miktarýný güncelleme
    public void UpdateMagazineAmmo(int weaponIndex, int ammoCount)
    {
        if (MagazineAmmo.ContainsKey(weaponIndex))
        {
            MagazineAmmo[weaponIndex] = ammoCount;
        }
        else
        {
            Debug.LogWarning("Þarjör için Silah ID'si bulunamadý: " + weaponIndex);
        }
    }
}
