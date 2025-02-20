using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Save/Save.txt";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER)) Directory.CreateDirectory(SAVE_FOLDER);
    }

    public static void Save(string saveString)
    {
        Debug.Log(SAVE_FOLDER);
        File.WriteAllText(SAVE_FOLDER, saveString);
    }

    public static string Load()
    {
        if (File.Exists(SAVE_FOLDER))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER);
            return saveString;
        }
        else
        {
            return null;
        }
    }
}
