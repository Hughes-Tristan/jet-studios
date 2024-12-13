
// Developer(s): Tristan Hughes
// Last Updated: 11-21-24
// Customers Class

// the intended purpose of this class is to manage customer behavior.
// it also provides methods for customer receiving orders and assigning customer seats.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Customers : MonoBehaviour
{
    private Items itemOrdered;
    private WaveManager waveManager;
    private Transform seatAssigned;
    private SpriteRenderer orderDisplay;
    public bool isSitting = false;
    private bool isMoving = false;
    public float moveSpeed = 2f;
    private Vector3 targetPos;
    public Transform exitpoint;
    private ItemManager itemManager;

    // Start is called before the first frame update
    void Start()
    {
        // this calls for the the wavemanager instance so the player can earn money
        waveManager = WaveManager.Instance;
        itemManager = ItemManager.Instance;

        // this gets the random item that the customer ordered
        itemOrdered = ItemManager.Instance.getRandomItem();
        if (itemOrdered == null)
        {
            Debug.LogError("itemOrdered is null");
        }

        // this finds and initializes the item display then assigns the sprite to display
        Transform orderTransform = transform.Find("orderDisplay");
        if (orderTransform != null)
        {
            Debug.Log("orderDisplay found!");
            orderDisplay = orderTransform.GetComponent<SpriteRenderer>();
            if (orderDisplay != null )
            {
                if(itemOrdered.itemIcon != null)
                {
                    orderDisplay.sprite = itemOrdered.itemIcon;
                }
            }
            
        } 
    }

    // this function checks to see if an order is received by the customer or not
    // if the items order is the same as the item given to the customer the player will earn money
    // otherwise the order received is not correct
    public bool orderReceived(Items item)
    {
        if (isSitting)
        {

            if (item == itemOrdered)
            {
                waveManager.EarnMoney(20);

                //when customer recieves order, player earns money and sfx coin is triggered
                //SFXManager.Instance.PlayMoneySound();

                CustomerManager.instance.seatAvailable(seatAssigned);
                Vector3 exitPos = exitpoint.position;
                itemManager.playMoneySound();
                leave(exitPos);

                //Destroy(gameObject);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }



    // this is a form of a setter function that assigns the seat the the customer
    public void assignSeat(Transform seat)
    {
        isSitting = true;
        seatAssigned = seat;

    }

    /*private IEnumerator customerLeave()
    {
        Transform orderTransform = transform.Find("orderDisplay");

    }*/

    public void changePos(Vector3 pos)
    {
        targetPos = pos;
        isMoving = true;
    }

    public void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) <= 0.1f)
        {
            isMoving = false;
        }
    }

    void Update()
    {
        if (isMoving)
        {
            move();
        }    
    }

    public void leave(Vector3 exit)
    {
        targetPos = exit;
        isMoving = true;
        StartCoroutine(waitToLeave());
    }
    public IEnumerator waitToLeave()
    {
        while (isMoving)
        {
            yield return null;
        }
        //CustomerManager.instance.seatAvailable(seatAssigned);
        Destroy(gameObject);
    }
}
