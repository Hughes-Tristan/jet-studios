using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }
    private AudioSource extraAudioSource;

    [SerializeField] private Transform mouseVisualTransform;
    [SerializeField] private GridManager gridManager; // Reference to GridManager
    private WeaponTypeListSO weaponTypeList;
    private WeaponTypeSO weaponType;
    private Camera mainCamera;
    WaveManager waveManager;

    private bool isWeaponSelected = false; // Flag to track if a weapon is selected

    private void Awake()
    {
        weaponTypeList = Resources.Load<WeaponTypeListSO>(typeof(WeaponTypeListSO).Name);
        weaponType = null; // Default: No weapon selected

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
        mainCamera = Camera.main;
        waveManager = WaveManager.Instance;
        extraAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        mouseVisualTransform.position = GetMouseWorldPosition();

        if (Input.GetMouseButtonDown(0) && isWeaponSelected)
        {
            TryPlaceWeapon();
        }
    }

    // this function is designed to play the audio for when player doesn't have enough money
    public void notEnoughMoneySound()
    {
        extraAudioSource.Play();
    }

    private void TryPlaceWeapon()
    {
        Tile highlightedTile = Tile.GetHighlightedTile(); // Get the tile currently highlighted by the mouse
        int moneyCounter;
        moneyCounter = waveManager.getMoney();

        if (highlightedTile != null)
        {
            if (!highlightedTile.CanBuild())
            {
                Debug.Log("Cannot place here: Tile is already occupied!");
                return;
            }

            // Check if the player has enough money
            StatsTypeSO moneyStat = Resources.Load<StatsTypeListSO>(typeof(StatsTypeListSO).Name).list[0]; // Assume money is the first stat
            int currentMoney = StatsManager.Instance.GetStatsAmount(moneyStat);



            if (moneyCounter >= weaponType.price)
            {
                // Place weapon at the center of the highlighted tile
                Transform builtTransform = Instantiate(weaponType.prefab, highlightedTile.transform.position, Quaternion.identity);
                highlightedTile.SetTransform(builtTransform); // Mark the tile as occupied

                temporaryDefense defenseWeapon = builtTransform.GetComponent<temporaryDefense>();
                if (defenseWeapon != null)
                {
                    defenseWeapon.initializeHealth(weaponType.health);
                }

                waveManager.spendMoney(weaponType.price);

                // Deduct the price
                StatsManager.Instance.SubtractStatsAmount(moneyStat, weaponType.price);



                // Reset weapon selection after placement
                isWeaponSelected = false;
                weaponType = null; // Clear the weapon selection
            }
            else
            {
                notEnoughMoneySound();
                Debug.Log($"Not enough money! {weaponType.nameString} costs {weaponType.price}.");
            }
        }
        else
        {
            Debug.Log("No valid tile is highlighted!");
        }
    }

    public void ChooseWeapon(int weaponIndex)
    {
        weaponType = weaponTypeList.list[weaponIndex];
        isWeaponSelected = true; // Mark that a weapon is selected
        Debug.Log($"Selected {weaponType.nameString} (Price: {weaponType.price}, Health: {weaponType.health})");
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Ensure it's 2D
        return mouseWorldPosition;
    }
}