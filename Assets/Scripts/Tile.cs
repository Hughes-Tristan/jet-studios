// Author: Jazzel Radaza
// Created: 2024-11-21
// Last Modified: 2024-11-22 by Jazzel (added tile highlighting logic)
// Description: Represents a single grid tile, handling appearance, highlighting, and occupancy status.

using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor; // Colors for alternating tile patterns
    [SerializeField] private SpriteRenderer _renderer; // Renderer to change tile appearance
    [SerializeField] private GameObject _highlight; // Highlight object for visual feedback

    private static Tile _currentlyHighlightedTile; // Tracks the tile currently highlighted by the mouse
    private Transform placedTransform; // The object occupying this tile

    // Checks if the tile is available for placement
    public bool CanBuild()
    {
        return placedTransform == null;
    }

    // Marks the tile as occupied by an object
    public void SetTransform(Transform transform)
    {
        placedTransform = transform;
    }

    // Clears the tile, making it available for placement
    public void ClearTransform()
    {
        placedTransform = null;
    }

    // Returns the currently highlighted tile
    public static Tile GetHighlightedTile()
    {
        return _currentlyHighlightedTile;
    }

    // Initializes the tile's appearance and size
    public void Init(bool isOffset, float tileSize)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor; // Apply the appropriate color
        transform.localScale = Vector3.one * tileSize; // Set the tile size
    }

    // Highlights the tile when the mouse hovers over it
    void OnMouseEnter()
    {
        _highlight.SetActive(true);
        _currentlyHighlightedTile = this;
    }

    // Removes the highlight when the mouse exits the tile
    void OnMouseExit()
    {
        _highlight.SetActive(false);
        if (_currentlyHighlightedTile == this)
        {
            _currentlyHighlightedTile = null;
        }
    }
}
