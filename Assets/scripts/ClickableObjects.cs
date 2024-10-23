using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjects : MonoBehaviour
{

    public PlayerController movementAction;
    public ItemManager itemAction;
    public bool isPickupObject, isPlaceObject;
    public GameObject itemPrefab;
    private GameObject activeItem;

    // Start is called before the first frame update
    void OnMouseDown()
    {
        Debug.Log("User Clicked");
        if (isPickupObject) {
            itemAction.pickupItem(transform.position);
            movementAction.setTarget(transform.position);
        } else if (isPlaceObject) {
            movementAction.setTarget(transform.position);
            itemAction.placeItem(transform.position);
        }
    }
}
