using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PointerManager : MonoBehaviour
{
    #region Singleton

    public static PointerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject pointer;
    public Image image;
    public bool checkingMousePosition;
    private Item currentItem;
    RaycastHit hitData;

    void Update()
    {

        //Can call this to get an object back from our thing
        if (checkingMousePosition)
        {
            Vector3 screenVector = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000f);

            if (Input.GetMouseButtonUp(1) && hit.transform != null)
            {
                Debug.Log(hit.transform.gameObject.name);
            }

            /*if (Physics.Raycast(hit, hitData, 1000) && Input.GetMouseButtonDown(1))
            {
                GameObject selectedObject = hitData.transform.gameObject;
                if(selectedObject != null)
                {
                    Debug.Log(selectedObject.name);
                }
            }*/
        }
    }

    

    public Item GetItem()
    {
        return currentItem;
    }

    public void AddPointerObject(Item itemToDisplay)
    {
        currentItem = itemToDisplay;

        image.sprite = currentItem.icon;
        pointer.gameObject.SetActive(true);
    }

    public void ClearPointObject()
    {
        image.sprite = null;
        pointer.gameObject.SetActive(false);
    }
}
