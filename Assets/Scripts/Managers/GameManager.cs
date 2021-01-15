namespace redd096
{
    using UnityEngine;

    [AddComponentMenu("redd096/Singletons/Game Manager")]
    public class GameManager : Singleton<GameManager>
    {
        public UIManager uiManager { get; private set; }
        public LevelManager levelManager { get; private set; }
        public LabyrinthGrid labyrinthGrid { get; private set; }

        protected override void SetDefaults()
        {
            //get references
            uiManager = FindObjectOfType<UIManager>();
            levelManager = FindObjectOfType<LevelManager>();
            labyrinthGrid = FindObjectOfType<LabyrinthGrid>();
        }
    }
}