using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWeapon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform parentBeforeDrag;

    private Vector3 originalPosition;

    [SerializeField] private RectTransform weaponPanel1;
    [SerializeField] private RectTransform weaponPanel2;
    [SerializeField] private RectTransform gunImages;
    [SerializeField] private int maxWeaponsPerPanel = 1;
    [SerializeField] private bool isWeaponAvailable;

    public GameObject weapon;
    public GameObject weapon2;

    [SerializeField] protected Gun_Scriptable Gun_Scriptable;
    private GameController _gameController => GameController.Instance;
    private bool _a = false;
    void Start()
    {
        originalPosition = transform.position;
        gameObject.SetActive(Gun_Scriptable.Is_Purchished);
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (weaponPanel1 == null)
            weaponPanel1 = GameObject.Find("WeaponPanel1").GetComponent<RectTransform>();

        if (weaponPanel2 == null)
            weaponPanel2 = GameObject.Find("WeaponPanel2").GetComponent<RectTransform>();
        if (gunImages == null)
            gunImages = GameObject.Find("GunsImages").GetComponent<RectTransform>();
    }

    private void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root); // Root'a al, böylece üstte kalýr
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        if(_a)
        {
            _gameController.RemoveWeapon(weapon);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta; // Sürükleme hareketi
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool insidePanel1 = IsInsidePanel(weaponPanel1) && CanAddWeaponToPanel(weaponPanel1);
        bool insidePanel2 = IsInsidePanel(weaponPanel2) && CanAddWeaponToPanel(weaponPanel2);
        if (insidePanel1)
        {
            _a = true;
            transform.SetParent(weaponPanel1);
            transform.localPosition = Vector3.zero; // Panelin içine sýfýr pozisyonunda yerleþtir
            _gameController.AddWeapon(weapon,0);
        }
        else if (insidePanel2)
        {
            _a = true;
            transform.SetParent(weaponPanel2);
            transform.localPosition = Vector3.zero; // Panelin içine sýfýr pozisyonunda yerleþtir
            _gameController.AddWeapon(weapon,1);
        }else
        {
            _a = false;
            
            transform.position = originalPosition;
            transform.SetParent(gunImages);
        }
            
      
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    private bool IsInsidePanel(RectTransform panel)
    {
        return panel != null && RectTransformUtility.RectangleContainsScreenPoint(panel, Input.mousePosition, null);
    }

    private bool CanAddWeaponToPanel(RectTransform panel)
    {
        return panel.childCount < maxWeaponsPerPanel; // Panelde maksimum silah sýnýrýný kontrol et
    }
}