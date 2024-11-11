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
        waveManager = WaveManager.Instance;
    }
    
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
