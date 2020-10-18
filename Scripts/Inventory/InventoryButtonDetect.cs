using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButtonDetect : MonoBehaviour, IPointerClickHandler
{
    public InventorySlot inventorySlot;

    public void OnPointerClick(PointerEventData data)
    {
        switch (data.button)
        {
            case PointerEventData.InputButton.Left:
                inventorySlot.SelectItem();
                break;
            case PointerEventData.InputButton.Right:
                inventorySlot.UseItem();
                break;
        }
    }
}
