using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        //destroy
        Destroy(collision.gameObject);

        //if was player, end game
        if (collision.transform.GetComponentInParent<Player>())
        {
            redd096.GameManager.instance.levelManager.EndGame(false);
        }
    }
}
