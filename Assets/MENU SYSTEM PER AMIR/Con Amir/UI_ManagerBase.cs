namespace redd096
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class UI_ManagerBase : MonoBehaviour
    {
        /// <summary>
        /// list of menus
        /// </summary>
        [SerializeField] protected List<UI_MenuBase> menus = new List<UI_MenuBase>();

        /// <summary>
        /// Selected menu
        /// </summary>
        protected UI_MenuBase currentMenu;

        public UI_MenuBase CurrentMenu => currentMenu;

        /// <summary>
        /// Setup every menu
        /// </summary>
        public void Setup()
        {
            foreach(UI_MenuBase menu in menus)
            {
                menu.Setup(this);
            }

            //call function
            OnSetup();
        }

        protected virtual void OnSetup()
        {

        }
    }
}