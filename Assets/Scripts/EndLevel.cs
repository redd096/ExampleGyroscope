using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //if hit player, win game
        if(other.GetComponentInParent<Player>())
        {
            redd096.GameManager.instance.levelManager.EndGame(true);
        }
    }
}
