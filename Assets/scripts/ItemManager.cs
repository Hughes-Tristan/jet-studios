using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    private bool isHoldingItem = false;
    public WaveManager moneyManager;
    //public GameObject itemPrefab;
    [SerializeField] private GameObject activeItem;
    public float spawnDelayMin = 3, spawnDelayMax = 6;

    public void Start()
    {
        activeItem.SetActive(false);
        spawnItem();
        Debug.Log("dummy test");
    }

    // Function for picking up an item
    public void pickupItem(Vector2 position)
    {
        Debug.Log("pickup called");
        Debug.Log($"isHoldingItem: {isHoldingItem}, item.activeSelf: {activeItem.activeSelf}");

        if (!isHoldingItem && activeItem != null)
        {
            Debug.Log("pickup debug");
            isHoldingItem = true;
            Destroy(activeItem);
            activeItem = null;
            Debug.Log("Item Received!");

            StartCoroutine(respawnItem());
        }
    }
    // Function for placing an item
    public void placeItem(Vector2 position)
    {
        if (isHoldingItem)
        {
            isHoldingItem = false;
            moneyManager.EarnMoney(10);
            Debug.Log("Item placed... you earned $10");
        }
    }

    private void spawnItem()
    {
        if(!activeItem.activeSelf && !isHoldingItem)
        {
            activeItem.SetActive(true);
            Debug.Log("Item respawned.");
        }
    }

    private IEnumerator respawnItem()
    {
        float spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
        yield return new WaitForSeconds(spawnDelay);
        spawnItem();
    }
}
