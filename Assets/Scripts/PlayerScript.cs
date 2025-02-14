using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    public int playerMoney; // Oyuncunun para miktarý
    public Dictionary<int, int> AllAmmo; // Toplam mermi miktarý
    public Dictionary<int, int> MagazineAmmo; // Þarjördeki mermi miktarý
    public Vector3 position;

    // Silahlar için bir liste oluþturduk
    public List<Weapon> weapons;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Baþlangýç silahlarý ve mermileri
        if (weapons == null)
        {
            weapons = new List<Weapon>
            {
                new Weapon(2020,"Taramali",120,30),
                new Weapon(2030,"Magnum",130,8),
                new Weapon(2040,"Pompali",140,2),
                new Weapon(2050,"Sniper",150,5)
            };
        }

        // Ammo ve MagazineAmmo baþlangýç verilerini oluþturma
        AllAmmo = new Dictionary<int, int>();
        MagazineAmmo = new Dictionary<int, int>();

        for (int i = 0; i < weapons.Count; i++)
        {
            AllAmmo.Add(i, weapons[i].totalAmmo); // Silahlarýn toplam mermi miktarýný kaydet
            MagazineAmmo.Add(i, weapons[i].magazineAmmo); // Þarjördeki mermi miktarýný kaydet
        }
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
    }
}
