using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag; // Sürüklenen objeyi al
        if (dropped != null && dropped.GetComponent<DraggableWeapon>())
        {
            dropped.transform.SetParent(transform); // Yeni parent olarak Active Guns alaný belirleniyor
            dropped.transform.localPosition = Vector3.zero; // Pozisyonu sýfýrla
        }
        if (transform.childCount >= 2)
        {
            Debug.Log("Sadece 2 silah eklenebilir!");
            return;
        }
    }
}

