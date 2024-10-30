using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjects : MonoBehaviour
{
    public PlayerController movementAction;
    public ItemManager itemAction;
    public bool isPickupObject, isPlaceObject;

    private void OnMouseDown()
    {
        if (movementAction == null) Debug.LogError("movementAction is not assigned!");
        if (itemAction == null) Debug.LogError("itemAction is not assigned!");

        Debug.Log("User Clicked");
        Debug.Log($"Clicked on: {gameObject.name}, Tag: {gameObject.tag}");

        if (isPickupObject && ItemManager.isItemAvailable() && CompareTag("Pickup"))
        {
            Debug.Log("Valid Pickup Item clicked!");
            movementAction.MoveToTarget(transform.position, () =>
            {
                GameObject itemToPickUp = itemAction.activeItem;

                if (itemToPickUp != null)
                {
                    itemAction.pickupItem(itemToPickUp);
                    Debug.Log("Item received.");
                }
                else
                {
                    Debug.LogWarning("No item to pick up!");
                }
            });
    }
        else if (isPlaceObject && ItemManager.hasActiveItem() && CompareTag("Place"))
        {
            Debug.Log("Valid place!");
            movementAction.MoveToTarget(transform.position, () =>
            {
                itemAction.placeItem(transform.position);
                Debug.Log("Item Placed");
            });
        }
        else
        {
            Debug.Log("Invalid click or wrong state!");
        }
    }
}
