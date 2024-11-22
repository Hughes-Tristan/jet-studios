using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    private static Tile _currentlyHighlightedTile; // Tracks the tile currently highlighted by the mouse
    private Transform placedTransform; // The object occupying this tile

    public bool CanBuild() {
        return placedTransform == null; // Check if the tile is unoccupied
    }

    public void SetTransform(Transform transform) {
        placedTransform = transform; // Mark the tile as occupied
    }

    public void ClearTransform() {
        placedTransform = null; // Clear the tile
    }

    public static Tile GetHighlightedTile() {
        return _currentlyHighlightedTile; // Return the currently highlighted tile
    }

    public void Init(bool isOffset, float tileSize) {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        transform.localScale = Vector3.one * tileSize; // Adjust tile scale
    }

    void OnMouseEnter() {
        _highlight.SetActive(true);
        _currentlyHighlightedTile = this; // Set this tile as the highlighted tile
    }

    void OnMouseExit() {
        _highlight.SetActive(false);
        if (_currentlyHighlightedTile == this) {
            _currentlyHighlightedTile = null; // Clear the highlighted tile if the mouse exits
        }
    }
}
