namespace redd096
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class UI_MenuBase : MonoBehaviour
    {
        /// <summary>
        /// Reference to UI_Manager
        /// </summary>
        protected UI_ManagerBase UI_managerBase;

        /// <summary>
        /// Is this menu active?
        /// </summary>
        bool isActive;

        public bool IsActive => isActive;

        /// <summary>
        /// Active or deactive menu
        /// </summary>
        public virtual void ToggleMenu(bool value)
        {
            //do only if state is changed 
            if (isActive == value)
                return;

            //active or deactive menu
            isActive = value;
            gameObject.SetActive(isActive);
        }

        /// <summary>
        /// Menu setup
        /// </summary>
        public void Setup(UI_ManagerBase UI_managerBase)
        {
            this.UI_managerBase = UI_managerBase;
            isActive = true;

            //call function
            OnSetup();
        }

        protected virtual void OnSetup()
        {

        }
    }
}