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
    [SerializeField] private int maxWeaponsPerPanel = 1;
    [SerializeField] private bool isWeaponAvailable;

    void Start()
    {
        originalPosition = transform.position;

        if (!isWeaponAvailable)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (weaponPanel1 == null)
            weaponPanel1 = GameObject.Find("WeaponPanel1").GetComponent<RectTransform>();

        if (weaponPanel2 == null)
            weaponPanel2 = GameObject.Find("WeaponPanel2").GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root); // Root'a al, böylece üstte kalýr
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
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
            transform.SetParent(weaponPanel1);
            transform.localPosition = Vector3.zero; // Panelin içine sýfýr pozisyonunda yerleþtir
            Debug.Log("Silah WeaponPanel1'e eklendi.");
        }
        else if (insidePanel2)
        {
            transform.SetParent(weaponPanel2);
            transform.localPosition = Vector3.zero; // Panelin içine sýfýr pozisyonunda yerleþtir
            Debug.Log("Silah WeaponPanel2'ye eklendi.");
        }
        else
        {
            transform.position = originalPosition; // Panel dýþýna býrakýldýysa geri dön
            Debug.Log("Panel dýþýnda! Silah eski konumuna geri döndü.");
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