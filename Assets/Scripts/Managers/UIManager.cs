namespace redd096
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("redd096/MonoBehaviours/UI Manager")]
    public class UIManager : MonoBehaviour
    {
        [Header("Pause")]
        [SerializeField] GameObject pauseMenu = default;

        [Header("End Game")]
        [SerializeField] GameObject endMenu = default;
        [SerializeField] Text endGameText = default;
        [SerializeField] string winString = "You Won!";
        [SerializeField] string loseString = "You Lost!";

        [Header("Hint End Game")]
        [SerializeField] GameObject hintEndGame = default;
        [SerializeField] float timeBeforeRemove = 2;

        [Header("Debug Text")]
        [SerializeField] Text debugTextToSet = default;

        Coroutine deactiveHintEndGame;

        private void Start()
        {
            //remove menu
            PauseMenu(false);
            EndMenu(false);

            //hide hint
            hintEndGame.SetActive(false);

            AddEvents();
        }

        void OnDestroy()
        {
            RemoveEvents();
        }

        #region events

        void AddEvents()
        {
            GameManager.instance.levelManager.onEndGame += OnEndGame;
        }

        void RemoveEvents()
        {
            GameManager.instance.levelManager.onEndGame -= OnEndGame;
        }

        void OnEndGame(bool win)
        {
            //show end menu
            EndMenu(true, win);
        }

        #endregion

        #region public API

        public void PauseMenu(bool active)
        {
            if (pauseMenu == null)
                return;

            //active or deactive pause menu
            pauseMenu.SetActive(active);
        }

        public void EndMenu(bool active, bool win = false)
        {
            //be sure to remove pause menu when active end menu
            if (active)
            {
                PauseMenu(false);

                //set end game text
                endGameText.text = win ? winString : loseString;
            }

            //active or deactive end menu
            endMenu.SetActive(active);
        }

        public void SetDebugText(string text)
        {
            if (debugTextToSet == null)
                return;

            //set text
            debugTextToSet.text = text;
        }

        public void ActivateHintEndGame()
        {
            //active
            hintEndGame.SetActive(true);

            //start timer to deactivate
            if (deactiveHintEndGame != null)
                StopCoroutine(deactiveHintEndGame);

            deactiveHintEndGame = StartCoroutine(DeactiveHintEndGame());
        }

        #endregion

        IEnumerator DeactiveHintEndGame()
        {
            //wait, then deactivate
            yield return new WaitForSeconds(timeBeforeRemove);

            hintEndGame.SetActive(false);
        }
    }
}