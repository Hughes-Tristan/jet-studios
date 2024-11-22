using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField] private Vector3 gridOrigin = Vector3.zero; // Default to (0, 0, 0)

    [SerializeField] private int gridWidth, gridHeight;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private float cellSize = 1f; // Default size

    private Tile[,] gridTiles; // Store all tiles in a 2D array

    void Start() {
        GenerateGrid();
    }

    void GenerateGrid() {
        gridTiles = new Tile[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                // Base position starts from GridManager's transform position
                Vector3 position = transform.position + new Vector3(x * cellSize, y * cellSize);
                var spawnedTile = Instantiate(_tilePrefab, position, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                // Checkerboard pattern for visibility
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset, cellSize);

                gridTiles[x, y] = spawnedTile; // Add tile to the grid
            }
        }
    }


    public Tile GetTileAtWorldPosition(Vector3 worldPosition) {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / cellSize);

        if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight) {
            return gridTiles[x, y];
        }
        return null;
    }
}
