using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Important")]
    [SerializeField] float timeBeforeStart = 3;

    Coroutine startGame_coroutine;

    public System.Action onStartGame;
    public System.Action<bool> onEndGame;

    void Start()
    {
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
            redd096.GameManager.instance.uiManager.SetText( (timer - Time.time).ToString("F0") );
            yield return null;
        }

        //start game
        StartGame();
    }

    public void StartGame()
    {
        //call event
        onStartGame?.Invoke();
    }

    public void EndGame(bool win)
    {
        //stop time and call event
        Time.timeScale = 0;
        onEndGame?.Invoke(win);
    }
}
