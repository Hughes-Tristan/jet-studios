
// Developer(s): Tristan Hughes
// Last Updated: 11-16-24
// Player Controller Script (controls player movements)

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
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
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //  this is how we check if there is an item to pick up
            // make sure to give the items a collider for this to  work
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                KitchenItems kitchenItem = hit.collider.GetComponent<KitchenItems>();
                if (kitchenItem != null)
                {
                    setPlayerPos(hit.collider.transform.position);
                    StartCoroutine(playerInteraction(kitchenItem.gameObject));
                    return;
                }

                Customers customer = hit.collider.GetComponent<Customers>();
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
    // this is to wait before the player picks up an item
    // once the player stops moving we will check if the collider that was detected is a customer or an item
    // if it is an item then call the pickup function, destroy the item, then leave the coroutine
    // if it is a customer then call the deliverorder function  then leave  the coroutine
    private System.Collections.IEnumerator playerInteraction(GameObject player)
    {
        while (isPlayerMoving)
        {
            yield return null;
        }

        KitchenItems item = player.GetComponent<KitchenItems>();
        if (item != null)
        {
            pickupItem(item.getItem());
            item.destroy();
            yield break;
        }

        Customers customer = player.GetComponent<Customers>();
        if (customer != null)
        {
            deliverOrder(customer);
            yield break;
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
            if (activeCustomer.orderReceived(itemHeld))
            {
                itemHeld = null;
                updateHeldItem();

                Debug.Log("item delivered");
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
        if(itemHeld == null)
        {
            itemHeld = item;
            updateHeldItem();
        }
    }
    
    // this function simply updates the player postion to the given  position
    // then it sets the player movement to true
    private void setPlayerPos(Vector3 position)
    {
        playerPos = position;
        isPlayerMoving = true;
    }
}
