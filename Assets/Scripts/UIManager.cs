using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI ToplamMermiSayisiText;
    [SerializeField] TextMeshProUGUI KalanMermiSayisiText;


    [SerializeField] protected KeyCode m_buyMenu = KeyCode.B;
    [SerializeField] protected KeyCode m_inventoryMenu = KeyCode.I;
    public GameObject Menu;
    public GameObject Inventory;
    public MonoBehaviour playerController;

    public GameObject targetObject;
    public MonoBehaviour targetScript1;
    public MonoBehaviour targetScript2;
    public MonoBehaviour targetScript3;
    public MonoBehaviour targetScript4;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(m_buyMenu))
        {
            ToggleMenu(Menu);
        }
        else if (Input.GetKeyDown(m_inventoryMenu))
        {
            ToggleMenu(Inventory);
        }
    }

    void ToggleMenu(GameObject menu)
    {
        bool isActive = !menu.activeSelf;
        menu.SetActive(isActive);

        // Menü açýldýðýnda mouse serbest býrak
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isActive;

        // Menü açýldýðýnda belirli bir scripti kapat
        if (targetScript1 != null)
        {
            targetScript1.enabled = !isActive; // Menü açýksa script kapanýr, menü kapalýysa açýlýr
        }
        if (targetScript2 != null)
        {
            targetScript2.enabled = !isActive; // Menü açýksa script kapanýr, menü kapalýysa açýlýr
        }
        if (targetScript3 != null)
        {
            targetScript3.enabled = !isActive; // Menü açýksa script kapanýr, menü kapalýysa açýlýr
        }
        if (targetScript4 != null)
        {
            targetScript4.enabled = !isActive; // Menü açýksa script kapanýr, menü kapalýysa açýlýr
        }
        if (playerController != null)
        {
            playerController.enabled = !isActive; // Menü açýksa script kapanýr, menü kapalýysa açýlýr
        }
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
    public void MenuScreen()
    {
        
    }
}
