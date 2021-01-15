using UnityEngine;
using redd096;

public class ColorComponent : MonoBehaviour
{
    [Header("Important")]
    [SerializeField] float contactDistance = 3f;

    TileBase lastTile;

    private void Update()
    {
        //get current tile, and be sure is in contact
        TileBase currentTile = GameManager.instance.labyrinthGrid.GetCurrentTile(transform.position);
        if (Vector3.Distance(transform.position, currentTile.transform.position) > contactDistance)
            return;

        //if different tile from last one
        if (currentTile != lastTile)
        {
            lastTile = currentTile;

            //if there is interface, color tile
            IColorable colorable = currentTile.GetComponent<IColorable>();
            if(colorable != null)
                colorable.ColorElement(GetComponentInChildren<Renderer>().material.color);
        }
    }

    /*
    IColorable lastHit;

    private void OnCollisionEnter(Collision collision)
    {      
        //if collide with Icolorable and is not last hit
        IColorable colorable = collision.GetContact(0).otherCollider.GetComponentInParent<IColorable>();
        if(colorable != null)
        {
            //color element
            if (colorable.ColorElement(GetComponentInChildren<Renderer>().material.color))
            {
                //set last hit 
                lastHit = colorable;
            }
        }
    }
    */
}
