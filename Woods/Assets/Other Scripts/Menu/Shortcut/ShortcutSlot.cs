using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortcutSlot : MonoBehaviour {

    private Button slotBtn;

    public GameObject inven;
    private InventoryManager invenManager;

    public GameObject shortcut;
    private ShortcutManager shortcutManager;

	// Use this for initialization
	void Start () {
        slotBtn = GetComponent<Button>();
        invenManager = inven.GetComponent<InventoryManager>();

        shortcutManager = shortcut.GetComponent<ShortcutManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetShortcut()
    {
        Debug.Log("Shortcut slot selected!");
        GameObject setGoShortcut = invenManager.goToSetShortcut;
        
        setGoShortcut.GetComponent<ManageItem>().inShortcut = true;

        setGoShortcut.transform.SetParent(transform);
        setGoShortcut.transform.position = transform.position;

        ManageItem manageItem = setGoShortcut.GetComponent<ManageItem>();
        Button itemBtn = setGoShortcut.GetComponent<Button>();
        itemBtn.onClick.AddListener(delegate () { shortcutManager.UseItemInShortcut(setGoShortcut); });

        invenManager.goToSetShortcut = null;
    }

}
