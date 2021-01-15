using UnityEngine;

[SelectionBase]
public class TileLabyrinth : TileBase, IColorable
{
    public void ColorElement(Color drawColor)
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = drawColor;
        }
    }
}
