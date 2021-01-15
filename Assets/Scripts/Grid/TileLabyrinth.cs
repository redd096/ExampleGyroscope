using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
