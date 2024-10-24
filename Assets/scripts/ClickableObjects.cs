using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjects : MonoBehaviour
{

    public PlayerController movementAction;
    public ItemManager itemAction;
    public bool isPickupObject, isPlaceObject;
    public GameObject itemReference;
    private static GameObject activeItem;
    private static bool isItemAvailable = true;

    // Start is called before the first frame update
    public void OnMouseDown()
    {
        Debug.Log("User Clicked");
        Debug.Log($"Clicked on: {gameObject.name}, Tag: {gameObject.tag}");
        if (isPickupObject && activeItem == null && CompareTag("Pickup") && isItemAvailable) {
            Debug.Log("Valid Pickup Item clicked!");
            movementAction.MoveToTarget(transform.position, () =>
            {
                if(itemReference != null)
                {
                    itemAction.pickupItem(itemReference);
                    activeItem = itemAction.activeItem;
                    isItemAvailable = false;
                    Debug.Log("Item received.");
                }
                else
                {
                    Debug.LogWarning("No item to pick up!");
                }

            });
            
        } else if (isPlaceObject && activeItem != null && CompareTag("Place")) {
            Debug.Log("Valid place!");
            movementAction.MoveToTarget(transform.position, () =>
            {
                itemAction.placeItem(transform.position);
                Debug.Log($"Placed item: {activeItem.name}");
                activeItem = null;
            });
        }
        else
        {
            Debug.Log("Invalid click or wrong state!");
        }
    }
    public static void resetItemAvailability()
    {
        isItemAvailable = true;
        Debug.Log("Item is now available");
    }
}
