namespace redd096
{
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

        [Header("Text")]
        [SerializeField] Text textToSet = default;

        [Header("Analog")]
        [SerializeField] RectTransform area = default;
        [SerializeField] RectTransform analog = default;
        [SerializeField] float smooth = 10;

        void Awake()
        {
            //remove menu
            PauseMenu(false);
            EndMenu(false);

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

        public void SetText(string text)
        {
            if (textToSet == null)
                return;

            //set text
            textToSet.text = text;
        }

        #region analog

        public void AnalogPosition(Vector2 position)
        {
            if (analog == null)
                return;

            //set analog position
            analog.position = Vector2.Lerp(analog.position, position, Time.deltaTime * smooth);

            //clamp in area
            Vector2 anchoredPosition = analog.anchoredPosition;
            anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, -area.sizeDelta.x / 2, area.sizeDelta.x / 2);
            anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, -area.sizeDelta.y / 2, area.sizeDelta.y / 2);

            analog.anchoredPosition = anchoredPosition;
        }

        public void ResetAnalogPosition()
        {
            if (analog == null)
                return;

            //reset analog position
            analog.anchoredPosition = Vector2.Lerp(analog.anchoredPosition, Vector2.zero, Time.deltaTime * smooth);
        }

        public Vector3 GetAnalogAnchoredPosition()
        {
            return analog.anchoredPosition;
        }

        #endregion

        #endregion
    }
}