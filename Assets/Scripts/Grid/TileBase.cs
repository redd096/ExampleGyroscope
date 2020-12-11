using UnityEngine;

[SelectionBase]
public class TileBase : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] protected Color pixel;

    public void Init(Color pixel)
    {
        this.pixel = pixel;
    }
}
