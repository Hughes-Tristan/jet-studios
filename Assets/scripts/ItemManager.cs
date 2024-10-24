using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    //private bool isHoldingItem = false;
    public WaveManager moneyManager;
    public GameObject spawnObject;
    public GameObject itemPrefab;
    public GameObject activeItem;
    public float spawnDelayMin = 3;
    public float spawnDelayMax = 6;


    // Function for picking up an item
    public void pickupItem(GameObject item)
    {
        Debug.Log("pickup called");
        //Debug.Log($"isHoldingItem: {isHoldingItem}, item.activeSelf: {activeItem.activeSelf}");

        if (item != null)
        {
            item.SetActive(false);
            activeItem = item;
            Debug.Log("Item picked up!");
        }
        else
        {
            Debug.LogWarning("Item is not set or not found!");
        }
    }
    // Function for placing an item
    public void placeItem(Vector2 position)
    {
        if (activeItem != null)
        {
            activeItem.transform.position = position;
            activeItem.SetActive(true);
            moneyManager.EarnMoney(10);
            Debug.Log("Item placed... you earned $10");

            activeItem = null;
            StartCoroutine(respawnItem());
        } 
        else
        {
            Debug.LogWarning("No item to place");
        }
    }

    private IEnumerator respawnItem()
    {
        float spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
        Debug.Log($"Item will respawn in {spawnDelay} seconds");
        yield return new WaitForSeconds(spawnDelay);
        
        Vector2 spawnPosition = spawnObject.transform.position;
        GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        activeItem = newItem;
        Debug.Log("Respawned after {spawnDelay} seconds.");

        ClickableObjects.resetItemAvailability();
    }
}
