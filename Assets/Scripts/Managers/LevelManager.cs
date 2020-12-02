using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using redd096;

public class LevelManager : MonoBehaviour
{
    [Header("Important")]
    [SerializeField] float timeBeforeStart = 3;

    Coroutine startGame_coroutine;

    void Start()
    {
        //stop player
        GameManager.instance.player.enabled = false;

        //be sure to run only one coroutine
        if (startGame_coroutine != null)
            StopCoroutine(startGame_coroutine);

        //start coroutine
        startGame_coroutine = StartCoroutine(StartGame_Coroutine());
    }

    IEnumerator StartGame_Coroutine()
    {
        //wait timer (show on UI)
        float timer = Time.time + timeBeforeStart;
        while(Time.time < timer)
        {
            GameManager.instance.uiManager.SetText( (timer - Time.time).ToString("F0") );
            yield return null;
        }

        //start game
        StartGame();
    }

    public void StartGame()
    {
        //enable player
        GameManager.instance.player.enabled = true;
    }

    public void EndGame(bool win)
    {
        //stop time and show end menu
        Time.timeScale = 0;
        GameManager.instance.uiManager.EndMenu(true, win);
    }
}
