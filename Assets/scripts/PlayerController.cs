using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Controller Variables and References
    public float moveSpeed = 5;
    public SpriteRenderer itemHeldRender;
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
        checkMoving();
        checkInput();
    }

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

    private void checkInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

    private void updateHeldItem()
    {
        // if there is a item and it is being held display its sprite
        // if there is no item being held, hide the the render
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

    private void pickupItem(Items item)
    {
        if(itemHeld == null)
        {
            itemHeld = item;
            updateHeldItem();
        }
    }
    
    private void setPlayerPos(Vector3 position)
    {
        playerPos = position;
        isPlayerMoving = true;
    }
}
