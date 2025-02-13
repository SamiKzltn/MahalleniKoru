using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoBoxSpawn : MonoBehaviour
{

    //Kutularýn Oluþma Noktalarý
    public List<GameObject> AmmoBoxPoint = new List<GameObject>();
    public GameObject Ammo_Box;
    public static bool IsAmmoExists;
    public int HowMuchExits;
    
    //Kutularýn çýkma süreleri farký
    public float BoxExitTime;

    int Rand;

    List<int> Point = new List<int>();

    

    private void Start()
    {
        HowMuchExits = 1;
        IsAmmoExists = false;
        //StartCoroutine(AmmoCreater());
        InvokeRepeating(nameof(AmmoCreater), 2f, 2f);
    }
    void AmmoCreater()
    {
        Rand = Random.Range(0,5);

        if (HowMuchExits <= 2)
        {
            if (!Point.Contains(Rand))
            {
                Point.Add(Rand);
            }
            else
            {
                Rand = Random.Range(0, 5);
                //continue;
                return;
            }
            GameObject obj = Instantiate(Ammo_Box, AmmoBoxPoint[Rand].transform.position, AmmoBoxPoint[Rand].transform.rotation);
            HowMuchExits++;

            obj.transform.gameObject.GetComponent<AmmoScripts>().Pointi = Rand;
        }

        
}
    public void DestroyPoints(int value)
    {
        Point.Remove(value);
    }
}
