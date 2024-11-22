
// Developer(s): Tristan Hughes
// Last Updated: 11-16-24
// Kitchen Items Class

// the intended purpose of this class is to manage items that the player can interact with
// it also provides a method for handling item respawns

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenItems : MonoBehaviour
{
    [SerializeField] private Items item;

    private SpriteRenderer spriteRenderer;
    private Collider2D collider2DRef;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2DRef = GetComponent<Collider2D>();
    }

    // this is a public getter function to return the item
    public Items getItem()
    {
        return item;
    }

    // this function hides the item then initiates a respawn
    public void itemRespawn()
    {
        Debug.Log("item respawned");
        collider2DRef.enabled = false;
        spriteRenderer.enabled = false;

        StartCoroutine(respawn());
    }

    // public function to destroy the item
    //public void destroy()
    //{
    //    Destroy(gameObject);
    //}


    // this is a function that waits to set the item to visible for a random period of time
    // this random time occur in an interval
    private IEnumerator respawn()
    {
        float randomizedTime;
        randomizedTime = Random.Range(3, 6);
        yield return new WaitForSeconds(randomizedTime);
        collider2DRef.enabled = true;
        spriteRenderer.enabled = true;
        Debug.Log("item visible");
    }
}
