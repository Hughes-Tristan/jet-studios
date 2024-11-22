
// Developer(s): Tristan Hughes
// Last Updated: 11-21-24
// Customers Class

// the intended purpose of this class is to manage customer behavior.
// it also provides methods for customer receiving orders and assigning customer seats.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customers : MonoBehaviour
{
    private Items itemOrdered;
    private WaveManager waveManager;
    private Transform seatAssigned;
    private SpriteRenderer orderDisplay;

    // Start is called before the first frame update
    void Start()
    {
        // this calls for the the wavemanager instance so the player can earn money
        waveManager = WaveManager.Instance;
        if (waveManager == null)
        {
            Debug.LogError("WaveManager.Instance is null");
        }

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
                } else
                {
                    Debug.Log("error: itemicon is null");
                }

            }
            else
            {
                Debug.Log("error: orderDisplay sprite render not found");
            }
        } else
        {
            Debug.Log("orderDisplay not found!");
        }
    }

    // this function checks to see if an order is received by the customer or not
    // if the items order is the same as the item given to the customer the player will earn money
    // otherwise the order received is not correct
    public bool orderReceived(Items item)
    {
        if(item == itemOrdered)
        {
            waveManager.EarnMoney(10);
           
            CustomerManager.instance.seatAvailable(seatAssigned);
            Destroy(gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }


    // this is a form of a setter function that assigns the seat the the customer
    public void assignSeat(Transform seat)
    {
        seatAssigned = seat;
    }
}
