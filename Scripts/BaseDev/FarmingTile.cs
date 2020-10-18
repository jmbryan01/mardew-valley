using UnityEngine;

public class FarmingTile : Interactable
{
    public SpriteRenderer spriteRenderer;

    public override void Interact()
    {
        base.Interact();

        PlaceObject();
    }

    public void PlaceObject()
    {
        Item itemToPlace = PointerManager.instance.GetItem();
        spriteRenderer.sprite = itemToPlace.icon;
    }
}
