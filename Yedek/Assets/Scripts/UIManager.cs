using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI ToplamMermiSayisiText;
    [SerializeField] TextMeshProUGUI KalanMermiSayisiText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void ShowAllAmmo(int MaxMermiSayisi, int kalanMermi)
    {

        ToplamMermiSayisiText.text = MaxMermiSayisi.ToString();
        KalanMermiSayisiText.text = kalanMermi.ToString();
    }
    public void JustMagazine(int kalanMermi)
    {
        KalanMermiSayisiText.text = kalanMermi.ToString();
    }
}
