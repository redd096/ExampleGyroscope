using UnityEngine;
using redd096;

[System.Serializable]
public struct TileStruct
{
    public TileBase tilePrefab;
    public Color color;
}

public class LabyrinthGrid : GridBase
{
    [Header("Roof")]
    [SerializeField] float yScale = 5;
    [SerializeField] float yPosition = 12.5f;

    [Header("Tile")]
    [SerializeField] TileStruct[] tiles = default;

    protected override void GenerateGrid()
    {
        base.GenerateGrid();

        //generate roof
        GameObject roof = GameObject.CreatePrimitive(PrimitiveType.Cube);
        roof.name = "Roof";

        //set size and position
        roof.transform.localScale = new Vector3(tileSize.x * gridImage.width, yScale, tileSize.z * gridImage.height);
        roof.transform.position = startPosition                                                                         //start position
            + new Vector3(tileSize.x * (gridImage.width - 1), 0, tileSize.z * (gridImage.height - 1)) / 2               //last tile position /2 to find center of the grid
            + Vector3.up * yPosition;                                                                                   //height position

        //set parent and hide renderer
        roof.transform.SetParent(transform);
        roof.GetComponent<Renderer>().enabled = false;
    }

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

    public bool CheckEndLevel()
    {
        //if a tile is not colored, return false
        foreach(TileLabyrinth tile in Grid.Values)
        {
            if (tile.IsColored == false)
                return false;
        }

        return true;
    }

    public TileBase GetCurrentTile(Vector3 position)
    {
        //return nearest tile
        return Utility.FindNearest(Grid, position, out Vector2Int key);
    }
}
