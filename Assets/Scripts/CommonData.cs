using UnityEngine;

public static class CommonData
{
   
    public static void AmmoSave(string gunName, int maxAmmo)
    {
        PlayerPrefs.SetInt(gunName, maxAmmo);
    }

    
    public static int GetAmmo(string gunName)
    {
        return PlayerPrefs.GetInt(gunName);
    }

    public static int GetAmmo(string gunName, int count)
    {
        return PlayerPrefs.GetInt(gunName, count);
    }
}
