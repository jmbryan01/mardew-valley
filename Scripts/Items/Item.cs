using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName= "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public ItemType itemType;
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public GameObject prefab;

    public virtual void Use()
    {
        Debug.Log("Using this item " + name);
    }

    public void Select()
    {
        PointerManager.instance.AddPointerObject(this);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}

public enum ItemType { Equippable, Useable }
