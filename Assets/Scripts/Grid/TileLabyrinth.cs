using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class TileLabyrinth : TileBase, IColorable
{
    [Header("DrawColor")]
    [SerializeField] bool canColor = true;

    bool isColored;
    public bool IsColored
    {
        //return true if can't color, or if is colored
        get
        {
            return canColor == false || isColored;
        }
        set
        {
            isColored = value;
        }
    }

    Dictionary<Renderer, Color> normalColors = new Dictionary<Renderer, Color>();

    void Awake()
    {
        //foreach renderer in child, add to dictionary the normal color
        foreach(Renderer r in GetComponentsInChildren<Renderer>())
        {
            normalColors.Add(r, r.material.color);
        }
    }

    public bool ColorElement(Color drawColor)
    {
        if (canColor)
        {
            //if already colored, reset to normal colors
            if (IsColored)
            {
                foreach (Renderer r in normalColors.Keys)
                {
                    r.material.color = normalColors[r];
                }
            }
            //else set draw color
            else
            {
                foreach (Renderer r in normalColors.Keys)
                {
                    r.material.color = drawColor;
                }
            }

            IsColored = !IsColored;
            return true;
        }

        return false;
    }
}
