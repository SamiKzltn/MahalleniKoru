using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    public int playerMoney; // Oyuncunun para miktar�
    public Dictionary<int, int> AllAmmo; // Toplam mermi miktar�
    public Dictionary<int, int> MagazineAmmo; // �arj�rdeki mermi miktar�
    public Vector3 position;

    // Silahlar i�in bir liste olu�turduk
    public List<Weapon> weapons;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Ba�lang�� silahlar� ve mermileri
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

        // Ammo ve MagazineAmmo ba�lang�� verilerini olu�turma
        AllAmmo = new Dictionary<int, int>();
        MagazineAmmo = new Dictionary<int, int>();

        for (int i = 0; i < weapons.Count; i++)
        {
            AllAmmo.Add(i, weapons[i].totalAmmo); // Silahlar�n toplam mermi miktar�n� kaydet
            MagazineAmmo.Add(i, weapons[i].magazineAmmo); // �arj�rdeki mermi miktar�n� kaydet
        }
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
    }
}
