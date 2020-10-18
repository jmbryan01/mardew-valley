using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerObject : MonoBehaviour
{
    private Item currentItem;
    private Vector2 screenVector;

    void LateUpdate()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            //TODO: turn all of this into a public method for converting inventory back into world objects
            //TODO: need to do a check for where something can be placed - make it specific areas? Or close enough

            screenVector = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

            currentItem = PointerManager.instance.GetItem();
            int numberOfItems = Inventory.instance.items[currentItem];

            if (currentItem.prefab != null)
            {
                if(numberOfItems > 1)
                {
                    PlaceObject(currentItem);
                }
                else if (numberOfItems == 1)
                {
                    PlaceObject(currentItem);

                    PointerManager.instance.ClearPointObject();
                    currentItem.RemoveFromInventory();
                }
            }
            else
            {
                Debug.Log("No prefab assigned to item " + currentItem.name);
            }           
        }

        if (Input.GetMouseButtonDown(1))
        {
            PointerManager.instance.ClearPointObject();
        }
    }

    private void PlaceObject(Item itemToCreate)
    {
        Instantiate(itemToCreate.prefab, screenVector, Quaternion.identity);
        Inventory.instance.items[itemToCreate] -= 1;

        if (Inventory.instance.onItemChangedCallback != null)
        {
            Inventory.instance.onItemChangedCallback.Invoke();
        }
    }
}
