
// Developer(s): Tristan Hughes
// Last Updated: 11-16-24
// Item Manager Class

// the intended purpose of this class is to manager the list of available items
// it also provides a method for selecting a random item

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }
    private AudioSource audioSource;

    public List<Items> itemsAvailable;

    // this function is used to check if an instance exists or not
    // then it determines whether to set the instance or destroy the duplicate
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("ItemManager instance set.");
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    // this function is used to return a random item from the available items list
    // if the list is null then it should return null
    // same thing for the item, if the item is null it should return
    // if neither of those are null it returns a random item
    public Items getRandomItem()
    {
        int i;
        if(itemsAvailable == null)
        {
            Debug.LogError("itemsAvailable is null");
            return null;
        }

        i = Random.Range(0, itemsAvailable.Count);
        Items randomItem = itemsAvailable[i];
        if (randomItem == null)
        {
            Debug.LogError("Randomly selected item is null");
        } 
        return randomItem;

    }
    // this function is designed to play the audio for selling items
    public void playMoneySound()
    {
        audioSource.Play();
    }
}
