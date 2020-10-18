using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public GameObject countPanel;
    public Text numItems;
    Item item;

    public void AddItem(Item newItem, int itemCount)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;

        if (itemCount > 1)
        {
            numItems.text = itemCount.ToString();
            countPanel.SetActive(true);
        }
        else
        {
            numItems.text = null;
            countPanel.SetActive(false);
        }
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;

        countPanel.SetActive(false);
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        item.Use();
    }

    public void SelectItem()
    {
        item.Select();
    }
}
