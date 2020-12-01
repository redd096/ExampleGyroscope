namespace redd096
{
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("redd096/MonoBehaviours/UI Manager")]
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu = default;
        [SerializeField] Button useAccel = default;
        [SerializeField] Button useGyro = default;

        void Start()
        {
            PauseMenu(false);

            useAccel.gameObject.SetActive(true);
            useGyro.gameObject.SetActive(false);
        }

        public void PauseMenu(bool active)
        {
            if (pauseMenu == null)
                return;

            //active or deactive pause menu
            pauseMenu.SetActive(active);
        }
    }
}