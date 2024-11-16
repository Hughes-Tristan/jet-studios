
// Developer(s): Tristan Hughes
// Last Updated: 11-16-24
// Kitchen Items Class

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenItems : MonoBehaviour
{
    [SerializeField] private Items item;

    // this is a public getter to return the item
    public Items getItem()
    {
        return item;
    }

    // public function to destroy the item
    public void destroy()
    {
        Destroy(gameObject);
    }
}
