namespace redd096
{
    using UnityEngine;

    [AddComponentMenu("redd096/Singletons/Game Manager")]
    public class GameManager : Singleton<GameManager>
    {
        public UIManager uiManager { get; private set; }
        public Player player { get; private set; }
        public LevelManager levelManager { get; private set; }

        protected override void SetDefaults()
        {
            //get references
            uiManager = FindObjectOfType<UIManager>();
            player = FindObjectOfType<Player>();
            levelManager = FindObjectOfType<LevelManager>();

            //if there is a player, lock mouse
            //if (player)
            //{
            //    FindObjectOfType<SceneLoader>().ResumeGame();
            //}
        }
    }
}