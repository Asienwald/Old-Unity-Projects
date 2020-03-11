using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutManager : MonoBehaviour {

    private bool itemInCooldown;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UseItemInShortcut(GameObject item)
    {
        if (itemInCooldown)
        {
            Debug.Log("Item still in cooldown!");
            return;
        }
        else
        {
            item.GetComponent<ManageItem>().UseItem();
            StartCoroutine(StartItemCooldown(item.GetComponent<DisplayItem>().coolDown));
        }
    }

    private IEnumerator StartItemCooldown(float itemCd)
    {
        itemInCooldown = true;
        yield return new WaitForSeconds(itemCd);
        itemInCooldown = false;
    }
}
