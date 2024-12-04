
// Developer(s): Tristan Hughes
// Last Updated: 11-21-24
// Player Controller Script (controls player movements)

// the intended purpose of this class is to manage player behavior.
// it also provides methods for

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public player controller  variables
    public float moveSpeed = 5;
    public SpriteRenderer itemHeldRender;

    // private player contorller variables
    private Vector3 playerPos;
    private WaveManager waveManager;
    private Items itemHeld;
    private bool isPlayerMoving = false;

    // player boundaries
    public float minBoundaryX = -10.0f;
    public float maxBoundaryX = 10.0f;
    public float minBoundaryY = -10.0f;
    public float maxBoundaryY = 10.0f;

    void Start()
    {
        // This initializes playerPos at the location it is currently at
        playerPos = transform.position;
        // retreive the reference to wave manager instance
        waveManager = WaveManager.Instance;

    }

    void Update()
    {
        // update functions that will be checked every frame
        checkMoving();
        checkInput();
    }

    // this function  is a private function to check if  the player is moving or not
    // if the player is moving then move the player towards the location
    // you can change the moveSpeed to change how fast the player moves
    // once the player reaches the the click position within a certain distance stop the player movement
    private void checkMoving()
    {
        if (isPlayerMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, playerPos) <= 0.1f)
            {
                isPlayerMoving = false;
            }
        }
    }

    // checking for if the player clicks
    // if the player clicks an there is no collider attached to any object clicked
    // then the player will just move to the mouse position
    //
    // if the player clicks and detects a collider then the item will connect to the player
    // and it will start the coroutine to pick up  the kitchen item
    private void checkInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // this is how the mouse position is  registered in the world space
            Vector2 mousePos;
            KitchenItems kitchenItem;
            RaycastHit2D hit;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //  this is how we check if there is an item to pick up
            // make sure to give the items a collider for this to  work 
            hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                kitchenItem = hit.collider.GetComponent<KitchenItems>();
                if (kitchenItem != null)
                {
                    setPlayerPos(hit.collider.transform.position);
                    StartCoroutine(playerInteraction(kitchenItem.gameObject));
                    //kitchenItem.itemRespawn();
                    return;
                }

                Customers customer;
                customer = hit.collider.GetComponent<Customers>();
                if (customer != null)
                {
                    setPlayerPos(hit.collider.transform.position);
                    StartCoroutine(playerInteraction(customer.gameObject));
                    return;
                }
            }

            setPlayerPos(mousePos);
        }
    
    }

    // this function is designed as a coroutine for player interactions
    // while the player is  moving then you need to continue to the next frame
    // we need to have a distance check for the item that the player clicks so if the player is not within a certain distance
    // then the player will no pick up the item
    // once the player gets close enough he will pick up the item
    // this is to wait before the player picks up an item
    // once the player stops moving we will check if the collider that was detected is a customer or an item
    // if it is an item then call the pickup function, destroy the item, then leave the coroutine
    // if it is a customer then call the deliverorder function  then leave  the coroutine
    private IEnumerator playerInteraction(GameObject targetObject)
    {
        KitchenItems item;
        Customers customer;

        while (isPlayerMoving)
        {
            yield return null;
        }

        if (Vector3.Distance(transform.position, targetObject.transform.position) <= 0.6f){

            item = targetObject.GetComponent<KitchenItems>();

            if (item != null)
            {
                if (itemHeld == null)
                {
                    pickupItem(item.getItem());
                    item.itemRespawn();
                }
                else
                {
                    itemHeld = item.getItem();
                    updateHeldItem();
                    item.itemRespawn();
                }

                yield break;
            }

            customer = targetObject.GetComponent<Customers>();
            if (customer != null)
            {
                deliverOrder(customer);
                yield break;
            }
        }


    }

    // this function delivers an item to a customer
    // if no item is held then do nothing
    // if an item is held then if the items are received by a customer
    // then update the item held variable and  call the update function
    private void deliverOrder(Customers activeCustomer)
    {
        if (itemHeld != null)
        {
            bool orderAccepted = activeCustomer.orderReceived(itemHeld);
            if (orderAccepted)
            {
                itemHeld = null;
                updateHeldItem();

                Debug.Log("item delivered");
            } else
            {
                Debug.Log("the customer did not like this item");
            }
        } else
        {
            Debug.Log("no item to deliver");
        }

    }

    //this function updates depending on if an item is held or not
    // if there is a item and it is being held display its sprite
    // if there is no item being held, hide the render
    private void updateHeldItem()
    {
        if (itemHeldRender != null)
        {
            if(itemHeld != null)
            {
                itemHeldRender.sprite = itemHeld.itemIcon;
                itemHeldRender.enabled = true;
            } else
            {
                itemHeldRender.sprite = null;
                itemHeldRender.enabled = false;
            }
        }
    }

    // this  function sets the held item as the given item then calls the update function 
    private void pickupItem(Items item)
    {
        itemHeld = item;
        updateHeldItem();
    }
    
    // this function simply updates the player postion to the given  position
    // then it sets the player movement to true
    private void setPlayerPos(Vector3 position)
    {
        if (position.x >= minBoundaryX && position.x <= maxBoundaryX && position.y >= minBoundaryY && position.y <= maxBoundaryY)
        {
        playerPos = new Vector3(
            Mathf.Clamp(position.x, minBoundaryX, maxBoundaryX),
            Mathf.Clamp(position.y, minBoundaryY, maxBoundaryY),
            position.z
            );
        }
       
        isPlayerMoving = true;
    }
}
