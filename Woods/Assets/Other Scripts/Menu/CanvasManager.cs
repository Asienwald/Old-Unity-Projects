using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

    public GameObject menu;
    private bool menuOpened = false;

	// Use this for initialization
	void Start () {
		if(menu.activeSelf == true)
        {
            menu.SetActive(false);
        }
	}
	
	public void OpenClosePlayerMenu()
    {
        if (menuOpened)
        {
            menu.SetActive(false);
            menuOpened = false;
        }
        else
        {
            menu.SetActive(true);
            menuOpened = true;
        }
    }
}
