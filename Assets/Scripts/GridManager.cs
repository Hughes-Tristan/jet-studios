// Author: Jazzel Radaza
// Created: 2024-11-10
// Last Modified: 2024-12-05 by Jazzel (refactored movement logic)
// Description: Manages the grid system for placing and organizing weapons (soldiers) on the grid.

using UnityEngine;

public class GridManager : MonoBehaviour{
    [SerializeField] private Vector3 gridOrigin = Vector3.zero; // Starting position of the grid
    [SerializeField] private int gridWidth, gridHeight; // Grid dimensions
    [SerializeField] private Tile _tilePrefab; // Prefab used for creating grid tiles
    [SerializeField] private float cellSize = 1f; // Size of each cell in the grid

    private Tile[,] gridTiles; // 2D array to store all tiles

    void Start()
    {
        GenerateGrid(); // Initialize and populate the grid at the start
    }

    // Generates the grid by instantiating tiles in a 2D arrangement
    void GenerateGrid()
    {
        gridTiles = new Tile[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Calculate the world position of the tile
                Vector3 position = transform.position + new Vector3(x * cellSize, y * cellSize);

                // Spawn a tile and assign it to the grid
                var spawnedTile = Instantiate(_tilePrefab, position, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                // Alternate tile appearance for a checkerboard pattern
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset, cellSize);

                // Store the tile in the grid array
                gridTiles[x, y] = spawnedTile;
            }
        }
    }

    // Retrieves the tile at a given world position, if valid
    public Tile GetTileAtWorldPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / cellSize);

        if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)
        {
            return gridTiles[x, y];
        }
        return null; // Return null if the position is outside the grid bounds
    }
}
