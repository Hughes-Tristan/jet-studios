using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenItems : MonoBehaviour
{
    [SerializeField] private Items item;

    public Items getItem()
    {
        return item;
    }
    public void destroy()
    {
        Destroy(gameObject);
    }
}
