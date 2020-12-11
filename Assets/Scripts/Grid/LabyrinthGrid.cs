using UnityEngine;

[System.Serializable]
public struct TileStruct
{
    public TileBase tilePrefab;
    public Color color;
}

public class LabyrinthGrid : GridBase
{
    [Header("Tile")]
    [SerializeField] TileStruct[] tiles = default;

    protected override TileBase SelectTile(Color pixel)
    {

        //find in the array using color, then return prefab
        foreach(TileStruct tile in tiles)
        {
            if (tile.color == pixel)
            {
                return tile.tilePrefab;
            }
        }

        return null;
    }
}
