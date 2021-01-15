using UnityEngine;
using redd096;

public class EndLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //if hit player, check win game
        if(other.GetComponentInParent<Player>())
        {
            CheckWinGame();
        }
    }

    void CheckWinGame()
    {
        //check end level, then call end game
        if (GameManager.instance.labyrinthGrid.CheckEndLevel())
        {
            GameManager.instance.levelManager.EndGame(true);
        }
        //else show hint
        else
        {
            GameManager.instance.uiManager.ActivateHintEndGame();
        }
    }
}
