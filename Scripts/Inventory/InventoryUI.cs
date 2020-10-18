using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public Transform itemsParent;
    InventorySlot[] slots;

    public GameObject inventoryUI;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        List<Item> keys = new List<Item>(inventory.items.Keys);

        for (int i = 0; i < slots.Length; i++)
        {
            if(i < keys.Count)
            {
                int itemCount = inventory.items[keys[i]];
                slots[i].AddItem(keys[i], itemCount);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
