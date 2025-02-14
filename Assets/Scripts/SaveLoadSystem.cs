using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    private PlayerScript playerScripts;
    private List<Weapon> weapons;

    void Awake()
    {
        // `PlayerScript` bileþenini al, yoksa ekle
        playerScripts = FindObjectOfType<PlayerScript>();
        
        if (playerScripts == null)
        {
            playerScripts = gameObject.AddComponent<PlayerScript>();
        }

        if(weapons == null)
        {
            weapons = new List<Weapon>()
            {
                new Weapon(2020,"Taramali",120,30),
                new Weapon(2030,"Magnum",130,8),
                new Weapon(2040,"Pompali",140,2),
                new Weapon(2050,"Sniper",150,5)
            };
        }

        // Örnek veri oluþtur
        PlayerData playerData = new PlayerData()
        {
            playerMoney = 8000,
            position = playerScripts.transform.position,
            allweapons = weapons
        };
        string json = JsonUtility.ToJson(playerData);
        SaveSystem.Save(json);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K)) { SaveGame(); }
        if (Input.GetKeyUp(KeyCode.L)) { LoadGame(); }
    }

    public void SaveGame()
    {
        // Verileri al
        Vector3 playerPosition = playerScripts.transform.position;
        int money = playerScripts.playerMoney;
        List<Weapon> saveWeapon = weapons;

        // PlayerData nesnesini oluþtur
        PlayerData saveData = new PlayerData()
        {
            position = playerPosition,
            playerMoney = money,
            allweapons = saveWeapon
        };

        // JSON olarak kaydet
        string savePlayerData = JsonUtility.ToJson(saveData);
        SaveSystem.Save(savePlayerData);
        Debug.Log("Saved");
    }

    public void LoadGame()
    {
        string saveString = SaveSystem.Load();
        if (saveString != null) { 
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(saveString);

            playerScripts.transform.position = loadedData.position;
            playerScripts.playerMoney = loadedData.playerMoney;
            weapons = loadedData.allweapons;
            Debug.Log(loadedData.allweapons);
            Debug.Log("Loaded");
        }
        else
        {
            Debug.Log("Data Not Found");
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public Vector3 position;
    public int playerMoney;
    public List<Weapon> allweapons;
}

[System.Serializable]
public class Weapon
{
    public int ID;
    public string weaponName;
    public int totalAmmo;
    public int magazineAmmo;

    public Weapon(int Id, string weaponName, int totalAmmo, int magazineAmmo)
    {
        ID = Id;
        this.weaponName = weaponName;
        this.totalAmmo = totalAmmo;
        this.magazineAmmo = magazineAmmo;
    }
}
