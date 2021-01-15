using UnityEngine;

public class ColorComponent : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //if collide with Icolorable, call function
        IColorable colorable = collision.GetContact(0).otherCollider.GetComponentInParent<IColorable>();
        if(colorable != null)
        {
            colorable.ColorElement(GetComponentInChildren<Renderer>().material.color);
        }
    }
}
