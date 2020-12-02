﻿namespace redd096
{
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("redd096/MonoBehaviours/UI Manager")]
    public class UIManager : MonoBehaviour
    {
        [Header("Pause")]
        [SerializeField] GameObject pauseMenu = default;
        [SerializeField] Button useAccel = default;
        [SerializeField] Button useGyro = default;

        [Header("End Game")]
        [SerializeField] GameObject endMenu = default;
        [SerializeField] Text endGameText = default;
        [SerializeField] string winString = "You Won!";
        [SerializeField] string loseString = "You Lost!";

        [Header("Text")]
        [SerializeField] Text textToSet = default;

        void Awake()
        {
            //remove menu
            PauseMenu(false);
            EndMenu(false);

            //active first button
            useAccel.gameObject.SetActive(true);
            useGyro.gameObject.SetActive(false);
        }

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

        public void SetText(string text)
        {
            if (textToSet == null)
                return;

            //set text
            textToSet.text = text;
        }

        #endregion
    }
}