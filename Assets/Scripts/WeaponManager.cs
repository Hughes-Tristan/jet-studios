// Author: Jazzel Radaza
// Created: 2024-11-15
// Last Modified: 2024-12-13 by Jazzel (added notEnoughMoney sound)
// Description: Manages weapon selection, placement on the grid, and interactions such as audio feedback for insufficient funds.

using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }
    private AudioSource extraAudioSource; // Plays sound effects (e.g., insufficient funds)

    [SerializeField] private Transform mouseVisualTransform; // Visual indicator for the mouse position
    [SerializeField] private GridManager gridManager; // Reference to the grid system
    private WeaponTypeListSO weaponTypeList; // List of available weapon types
    private WeaponTypeSO weaponType; // Currently selected weapon type
    private Camera mainCamera; // Main camera for converting mouse position to world position
    private WaveManager waveManager; // Reference to WaveManager for managing money and waves

    private bool isWeaponSelected = false; // Tracks if a weapon is currently selected

    private void Awake()
    {
        weaponTypeList = Resources.Load<WeaponTypeListSO>(typeof(WeaponTypeListSO).Name);
        weaponType = null; // Default to no weapon selected

        if (Instance == null)
        {
            Instance = this;
            Debug.Log("WeaponManager instance set.");
        }
        else
        {
            Destroy(gameObject); // Enforce singleton pattern
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
        waveManager = WaveManager.Instance; // Get the WaveManager instance
        extraAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Update the position of the mouse visual indicator
        mouseVisualTransform.position = GetMouseWorldPosition();

        // Attempt to place the selected weapon when the left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && isWeaponSelected)
        {
            TryPlaceWeapon();
        }
    }

    // Plays a sound when the player doesn't have enough money
    public void notEnoughMoneySound()
    {
        extraAudioSource.Play();
    }

    // Attempts to place the selected weapon on the currently highlighted tile
    private void TryPlaceWeapon()
    {
        Tile highlightedTile = Tile.GetHighlightedTile(); // Get the currently highlighted tile
        int moneyCounter = waveManager.getMoney(); // Current money available to the player

        if (highlightedTile != null)
        {
            if (!highlightedTile.CanBuild())
            {
                Debug.Log("Cannot place here: Tile is already occupied!");
                return;
            }

            // Check if the player has enough money for the selected weapon
            StatsTypeSO moneyStat = Resources.Load<StatsTypeListSO>(typeof(StatsTypeListSO).Name).list[0]; // Assume money is the first stat
            int currentMoney = StatsManager.Instance.GetStatsAmount(moneyStat);

            if (moneyCounter >= weaponType.price)
            {
                // Place the weapon at the highlighted tile's center
                Transform builtTransform = Instantiate(weaponType.prefab, highlightedTile.transform.position, Quaternion.identity);
                highlightedTile.SetTransform(builtTransform); // Mark the tile as occupied

                // Initialize the weapon's health
                temporaryDefense defenseWeapon = builtTransform.GetComponent<temporaryDefense>();
                if (defenseWeapon != null)
                {
                    defenseWeapon.initializeHealth(weaponType.health);
                }

                waveManager.spendMoney(weaponType.price); // Deduct money from WaveManager
                StatsManager.Instance.SubtractStatsAmount(moneyStat, weaponType.price); // Deduct money from stats

                // Reset selection after placing the weapon
                isWeaponSelected = false;
                weaponType = null;
            }
            else
            {
                notEnoughMoneySound(); // Play sound for insufficient funds
                Debug.Log($"Not enough money! {weaponType.nameString} costs {weaponType.price}.");
            }
        }
        else
        {
            Debug.Log("No valid tile is highlighted!");
        }
    }

    // Selects a weapon from the weapon list
    public void ChooseWeapon(int weaponIndex)
    {
        weaponType = weaponTypeList.list[weaponIndex];
        isWeaponSelected = true;
        Debug.Log($"Selected {weaponType.nameString} (Price: {weaponType.price}, Health: {weaponType.health})");
    }

    // Converts the mouse position from screen space to world space
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Keep the position in 2D
        return mouseWorldPosition;
    }
}
