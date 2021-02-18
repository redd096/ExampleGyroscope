namespace redd096
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    [AddComponentMenu("redd096/UI Control/Old Event System redd096")]
    public class OldEventSystemRedd096 : EventSystem
    {
        #region variables

        [Header("For every menu, from what object start?")]
        [SerializeField] GameObject[] firstSelectedGameObjects = default;

        [Header("When one of these objects is active, can navigate only in its menu")]
        [SerializeField] GameObject[] overrideObjects = default;

        [Header("Can't navigate to these objects")]
        [SerializeField] System.Collections.Generic.List<GameObject> notNavigables = new System.Collections.Generic.List<GameObject>();

        GameObject selected;
        GameObject lastSelected;

        #endregion

        protected override void Update()
        {
            base.Update();

            //set current selected
            selected = current.currentSelectedGameObject;

            //if there is something selected and active
            if (selected && selected.activeInHierarchy)
            {
                //check if there is an override object active (can navigate only in its menu)
                CheckOverride();

                //check if selected a not navigable object
                CheckNotNavigables();

                //if != from last selected, set last selected
                if (lastSelected != selected)
                    lastSelected = selected;
            }
            //if selected nothing or is not active
            else
            {
                //if is active an override object, select it
                if (SetOverride())
                    return;

                //else, if last selected is active, select it
                //else check which firstSelectedGameObject is active, and select it
                SetFirstObject();
            }

            //if selected something not active, select null
            if (selected && selected.activeInHierarchy == false)
                current.SetSelectedGameObject(null);
        }

        #region selected and active

        void CheckOverride()
        {
            if (overrideObjects == null || overrideObjects.Length <= 0)
                return;

            foreach (GameObject overrideObj in overrideObjects)
            {
                //if an override is active, if selected something out of its menu
                if (overrideObj && overrideObj.activeInHierarchy && 
                    (selected == null || selected.transform.parent != overrideObj.transform.parent))
                {
                    //if last selected was in override menu, select it - otherwise select override object
                    if (lastSelected && lastSelected.activeInHierarchy && lastSelected.transform.parent == overrideObj.transform.parent)
                        current.SetSelectedGameObject(lastSelected);
                    else
                        current.SetSelectedGameObject(overrideObj);

                    break;
                }
            }
        }

        void CheckNotNavigables()
        {
            if (notNavigables == null || notNavigables.Count <= 0)
                return;

            //if selected a not navigable object
            if (notNavigables.Contains(selected))
            {
                //back to last selected or select null
                if (lastSelected && lastSelected.activeInHierarchy)
                    current.SetSelectedGameObject(lastSelected);
                else
                    current.SetSelectedGameObject(null);
            }
        }

        #endregion

        #region selected nothing or not active

        bool SetOverride()
        {
            if (overrideObjects == null || overrideObjects.Length <= 0)
                return false;

            //if is active an override object, select it
            foreach (GameObject overrideObj in overrideObjects)
            {
                if (overrideObj && overrideObj.activeInHierarchy)
                {
                    current.SetSelectedGameObject(overrideObj);
                    selected = overrideObj;
                    return true;
                }
            }

            return false;
        }

        void SetFirstObject()
        {
            //if last selected is active, select it
            if (lastSelected && lastSelected.activeInHierarchy)
            {
                current.SetSelectedGameObject(lastSelected);
                selected = lastSelected;
            }
            //else check which firstSelectedGameObject is active, and select it
            else
            {
                if (firstSelectedGameObjects == null || firstSelectedGameObjects.Length <= 0)
                    return;

                foreach (GameObject firstSelect in firstSelectedGameObjects)
                {
                    if (firstSelect && firstSelect.activeInHierarchy)
                    {
                        current.SetSelectedGameObject(firstSelect);
                        selected = firstSelect;
                        break;
                    }
                }
            }
        }

        #endregion

    }
}