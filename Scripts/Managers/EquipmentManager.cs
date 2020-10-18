using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public Equipment[] defaultItems;
    public GameObject[] equipmentSlots;
    Equipment[] currentEquipment;
    Sprite[] currentlyEquippedObjects;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;

        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numberOfSlots];
        currentlyEquippedObjects = new Sprite[numberOfSlots];

        EquipDefaultItems();
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex);

        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;

        SpriteRenderer targetSpriteRenderer = equipmentSlots[slotIndex].GetComponent<SpriteRenderer>();
        targetSpriteRenderer.sprite = newItem.spriteToDisplay;

        currentlyEquippedObjects[slotIndex] = newItem.spriteToDisplay;
    }

    public Equipment Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            if(currentlyEquippedObjects[slotIndex] != null)
            {
                currentlyEquippedObjects[slotIndex] = null;
                equipmentSlots[slotIndex].GetComponent<SpriteRenderer>().sprite = null;
            }

            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            return oldItem;
        }
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        EquipDefaultItems();
    }

    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

}
