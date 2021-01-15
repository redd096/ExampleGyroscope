using UnityEngine;

[SelectionBase]
public class TileBase : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] protected Color pixel;
    [SerializeField] Vector2Int positionInGrid;

    public void Init(Color pixel, Vector2Int positionInGrid)
    {
        this.pixel = pixel;
        this.positionInGrid = positionInGrid;
    }
}
