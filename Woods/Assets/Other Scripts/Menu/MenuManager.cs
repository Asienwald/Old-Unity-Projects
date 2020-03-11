using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    private GameObject playerMenu;

	// Use this for initialization
	void Start () {
        playerMenu = transform.GetChild(0).gameObject;
	}
	
	public void SwitchBetweenMenus(int index)
    { 
        int currentlyOpened = 0;
        foreach(Transform child in playerMenu.transform)
        {
            if (child.gameObject.activeSelf)
            {
                currentlyOpened = child.transform.GetSiblingIndex();
                //Debug.Log("current index="+ currentlyOpened);
            }
        }
        if(currentlyOpened + index >= 0 && currentlyOpened + index < playerMenu.transform.childCount)
        {
            //Debug.Log("Still in index!");
            //Debug.Log(currentlyOpened + index);
            playerMenu.transform.GetChild(currentlyOpened).gameObject.SetActive(false);
            playerMenu.transform.GetChild(currentlyOpened + index).gameObject.SetActive(true);
        }
        else if (currentlyOpened + index < 0)
        {
            //Debug.Log("Out of index!");
            //playerMenu.transform.GetChild(currentlyOpened).gameObject.SetActive(true);
            return;
        }
    }
}
