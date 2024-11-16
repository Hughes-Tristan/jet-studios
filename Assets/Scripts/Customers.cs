
// Developer(s): Tristan Hughes
// Last Updated: 11-16-24
// Customers Class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customers : MonoBehaviour
{
    [SerializeField] private Items itemOrdered;
    private WaveManager waveManager;

    // Start is called before the first frame update
    void Start()
    {
        // this calls for the the wavemanager instance so the player can earn money
        waveManager = WaveManager.Instance;
    }
    
    // this  class checks to see if and order is received by the customer  or not
    // if the items order is the same as the item given to the customer the player will earn money
    //  otherwise the order received is not correct
    public bool orderReceived(Items item)
    {
        if(item = itemOrdered)
        {
            waveManager.EarnMoney(10);
            Destroy(gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }
}
