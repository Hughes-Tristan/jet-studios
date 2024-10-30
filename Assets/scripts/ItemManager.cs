using System.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public WaveManager moneyManager;
    public GameObject spawnObject;  // Reference to where the item should respawn
    public GameObject itemPrefab;   // Prefab for the item
    public GameObject activeItem;   // Currently active item in the scene
    public float spawnDelayMin = 3f;
    public float spawnDelayMax = 6f;
    public float destroyDelay = 3f;

    private bool isAvailable = true;  // True if an item is available to be picked up
    private bool isItemPlaced = false;  // True if an item is currently placed on the table
    private static ItemManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        isAvailable = true;
        isItemPlaced = false;
        Debug.Log("ItemManager Start: Initializing first item.");

        // Call this method to instantiate the first item at the start of the game
        InitializeFirstItem();
    }

    private void InitializeFirstItem()
    {
        Debug.Log("Attempting to initialize the first item...");

        if (activeItem == null)
        {
            if (spawnObject == null || itemPrefab == null)
            {
                Debug.LogError("SpawnObject or ItemPrefab is not assigned in the Inspector.");
                return;
            }

            Vector2 spawnPosition = spawnObject.transform.position;
            activeItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
            activeItem.SetActive(true);  // Ensure it's visible
            Debug.Log("First item successfully instantiated at spawn position.");

            isAvailable = true;  // Mark item as available for pickup
        }
    }

    public static bool isItemAvailable()
    {
        if (instance == null)
        {
            Debug.LogError("ItemManager instance is missing!");
            return false;
        }

        Debug.Log($"Checking item availability: isAvailable={instance.isAvailable}, isItemPlaced={instance.isItemPlaced}, activeItem={(instance.activeItem != null ? "Exists" : "Null")}");
        return instance.isAvailable && !instance.isItemPlaced && instance.activeItem != null;
    }

    public static bool hasActiveItem()
    {
        return instance != null && instance.activeItem != null;
    }

    // Function for picking up an item
    public void pickupItem(GameObject item)
    {
        Debug.Log($"Attempting to pick up item. isAvailable: {isAvailable}, isItemPlaced: {isItemPlaced}");

        if (item != null && isAvailable && !isItemPlaced)
        {
            item.SetActive(false);  // Hide the item on the pickup table
            activeItem = item;
            isAvailable = false;  // Item is no longer available for pickup until respawn
            Debug.Log("Item picked up successfully!");
        }
        else
        {
            Debug.LogWarning("Item is not available for pickup!");
        }
    }

    // Function for placing an item
    public void placeItem(Vector2 position)
    {
        Debug.Log($"Attempting to place item. isAvailable: {isAvailable}, isItemPlaced: {isItemPlaced}");

        if (activeItem != null)
        {
            activeItem.transform.position = position;
            activeItem.SetActive(true);  // Show the item on the place table
            moneyManager.EarnMoney(10);  // Reward the player
            Debug.Log("Item placed successfully, earned $10.");

            isItemPlaced = true;  // Mark item as placed
            StartCoroutine(DestroyPlacedItem());  // Begin the deactivation and respawn process
            activeItem = null;
        }
        else
        {
            Debug.LogWarning("No active item to place.");
        }
    }

    private IEnumerator DestroyPlacedItem()
    {
        yield return new WaitForSeconds(destroyDelay);  // Wait for destroy delay
        if (activeItem != null)
        {
            activeItem.SetActive(false);  // Deactivate the item instead of destroying it
            Debug.Log("Placed item deactivated.");
        }
        isItemPlaced = false;  // Reset item placed status
        StartCoroutine(respawnItem());  // Trigger respawn
    }

    private IEnumerator respawnItem()
    {
        float spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
        Debug.Log($"Item will respawn in {spawnDelay} seconds.");
        yield return new WaitForSeconds(spawnDelay);  // Wait for respawn delay

        Vector2 spawnPosition = spawnObject.transform.position;

        // Reuse or create a new item if none exists
        if (activeItem == null)
        {
            activeItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Respawned new activeItem instance.");
        }
        else
        {
            activeItem.transform.position = spawnPosition;
            activeItem.SetActive(true);
            Debug.Log("Reactivated existing activeItem instance.");
        }

        isAvailable = true;  // Mark item as available for pickup
        Debug.Log("Item respawned and is available for pickup.");
    }
}
