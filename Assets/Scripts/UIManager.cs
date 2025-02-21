using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private PlayerScript playerScript => PlayerScript.Instance;

    [SerializeField] protected Gun_Scriptable[] Gun_Scriptable;
    [SerializeField] private Image[] weaponImages;
    [SerializeField] private Image HealthImage;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI ToplamMermiSayisiText;
    [SerializeField] TextMeshProUGUI KalanMermiSayisiText;
    [SerializeField] TextMeshProUGUI MoneyText;
    [SerializeField] TextMeshProUGUI HouseHealth;
    [SerializeField] TextMeshProUGUI PersonHealth;

    [Header("Buttons")]
    [SerializeField] protected KeyCode m_buyMenu = KeyCode.B;
    [SerializeField] protected KeyCode m_inventoryMenu = KeyCode.I;
    
    [Header("GameObjects")]
    public GameObject Menu;
    public GameObject Inventory;
    public GameObject targetObject;

    [Header("Scripts")]
    public MonoBehaviour playerController;
    public MonoBehaviour targetScript1;
    public MonoBehaviour targetScript2;
    public MonoBehaviour targetScript3;
    public MonoBehaviour targetScript4;

    private bool InventoryOpen;
    private bool ShopMenuOpen;

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
            MoneyText.text = playerScript.playerMoney.ToString();
        }
        else if (Input.GetKeyDown(m_inventoryMenu))
        {
            ToggleMenu(Inventory);
            FixingTheBuyButtons();
        }
    }

    void ToggleMenu(GameObject menu)
    {
        // E�er a�maya �al��t���m�z men� zaten a��k ise kapatabiliriz
        bool isActive = !menu.activeSelf;

        // E�er di�er men� a��ksa, yeni bir men� a��lmas�n� engelle
        if (isActive) // Yani yeni bir men� a��lacaksa
        {
            if (menu == Menu && Inventory.activeSelf) return;  // E�er Ma�aza a��lacaksa ama envanter a��ksa, engelle
            if (menu == Inventory && Menu.activeSelf) return;  // E�er Envanter a��lacaksa ama ma�aza a��ksa, engelle
        }

        menu.SetActive(isActive);

        // Men� a��ld���nda mouse'u serbest b�rak
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isActive;

        // Men� a��ld���nda belirli bir scripti kapat
        if (targetScript1 != null) targetScript1.enabled = !isActive;
        if (targetScript2 != null) targetScript2.enabled = !isActive;
        if (targetScript3 != null) targetScript3.enabled = !isActive;
        if (targetScript4 != null) targetScript4.enabled = !isActive;
        if (playerController != null) playerController.enabled = !isActive;
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

    public void HouseHealthUpdate(float house)
    {
        HealthImage.fillAmount = house/500;
    }
    public void FixingTheBuyButtons()
    {
        for (int i = 0; i < Gun_Scriptable.Length; i++)
        {
            if (Gun_Scriptable[i].Is_Purchished)
            {
                weaponImages[i].gameObject.SetActive(true); // Silah�n g�rselini aktif et
            }
            else
            {
                weaponImages[i].gameObject.SetActive(false); // Sat�n al�nmam��sa kapat
            }
        }
    }
}
