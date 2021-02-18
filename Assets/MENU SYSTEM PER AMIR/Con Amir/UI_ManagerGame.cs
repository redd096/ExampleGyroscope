using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using redd096;

public class UI_ManagerGame : UI_ManagerBase
{
    List<UI_MenuBase> previousMenu = new List<UI_MenuBase>();

    private void Start()
    {
        //start from first menu
        currentMenu = menus[0];

        //setup every menu
        Setup();
    }

    protected override void OnSetup()
    {
        base.OnSetup();

        //deactive every other menu 
        foreach(UI_MenuBase menu in menus)
        {
            if (menu != currentMenu)
                menu.ToggleMenu(false);
        }
    }

    public void ChangeMenu(UI_MenuBase newMenu)
    {
        //add current menu to previous and deactive
        previousMenu.Add(currentMenu);
        currentMenu.ToggleMenu(false);

        //change current menu and active new one
        currentMenu = newMenu;
        currentMenu.ToggleMenu(true);
    }
}
