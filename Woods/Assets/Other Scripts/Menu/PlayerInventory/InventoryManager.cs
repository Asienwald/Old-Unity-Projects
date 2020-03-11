using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public GameObject equipGo;
    private GameObject weapSlot;
    private GameObject bodySlot;
    private GameObject headSlot;
    private GameObject accSlot;

    public Canvas descripCanvas;
    public Text itemDescription;
    private Text selectedItemDescrip;
    private Text equippedItemDescrip;

    public GameObject player;
    public GameObject equipInven;
    public GameObject item;

    public GameObject gameData;
    private AllItems allItems;

    private GameObject selected;

    private List<int> itemCount;

    private bool OnCooldown;

    public GameObject shortcut;
    public GameObject goToSetShortcut;
    //public Button shortcutBtn;

    public GameObject log;
    private UpdateLog updateLog;

	// Use this for initialization
	void Start () {
        weapSlot = equipGo.transform.GetChild(2).gameObject;
        bodySlot = equipGo.transform.GetChild(1).gameObject;
        headSlot = equipGo.transform.GetChild(0).gameObject;
        //accSlot = equipGo.transform.GetChild(0).gameObject;

        //selectedItemDescrip = itemDescription.transform.GetChild(0).GetComponent<Text>();
        //equippedItemDescrip = itemDescription.transform.GetChild(1).GetComponent<Text>();

        allItems = gameData.GetComponent<AllItems>();

        updateLog = log.GetComponent<UpdateLog>();

        //shortcutBtn.onClick.AddListener(delegate () { SetShortcut(); });

        AddItem("Dagger");
        AddItem("Splint Mail");
        AddItem("Robes");
        AddItem("Battle Axe");
        AddItem("Fishing Cap");
        AddItem("Apple");
        AddItem("Apple");
    }

    public void SetShortcut()
    {
        Debug.Log("Setting shortcut!");
        //selected.GetComponent<ManageItem>().inShortcut = true;
        if(selected.GetComponent<DisplayItem>().itemType == "healing")
        {
            goToSetShortcut = selected;
            updateLog.AddActionInLog("Select shortcut slot to add shortcut.");
        }
        else
        {
            updateLog.AddActionInLog("You can only add shortcuts for healing items.");
        }
    }
	
	public void SelectItem(GameObject itemGo)
    {
        ShowItemDetails showDetails = itemGo.GetComponent<ShowItemDetails>();

        itemDescription.text = "";
        itemDescription.text = showDetails.ShowDetails();

        selected = itemGo;
    }

    public void ThrowItem()
    {
        ManageItem manageItem = selected.GetComponent<ManageItem>();
        manageItem.DecreaseItemCount();
        DisplayItem dispItem = selected.GetComponent<DisplayItem>();
        updateLog.AddActionInLog("You threw " + dispItem.name);
    }

    public void UseItem()
    {
       // Debug.Log("Used an item!");
        ManageItem manageItem = selected.GetComponent<ManageItem>();
        DisplayItem dispItem = selected.GetComponent<DisplayItem>();

        if(dispItem.isEquipment == true)
        {
            bool equipped = manageItem.Equip();
            //Debug.Log("Equipped the equipment!");
            if (equipped)
            {
                updateLog.AddActionInLog("Equipped " + dispItem.name);
            }
            else
            {
                updateLog.AddActionInLog("Your level is too low!");
            }
            
        }
        else if(dispItem.itemType == "healing")
        {
            if (OnCooldown)
            {
                Debug.Log("Item still on cooldown!");
                updateLog.AddActionInLog("Item still on cooldown!");
            }
            else
            {
                manageItem.UseItem();
                updateLog.AddActionInLog("You used " + dispItem.name);
                StartCoroutine(StartItemCooldown(dispItem.coolDown));
            }
        }
        
    }

    private IEnumerator StartItemCooldown(float coolDownTime)
    {
        OnCooldown = true;
        yield return new WaitForSeconds(coolDownTime);
        OnCooldown = false;
    }

    public void AddItem(string itemName)
    {
        bool repeatedItem = false;
        updateLog.AddActionInLog("You got <" + itemName + ">");

        foreach (Item i in allItems.allItems)
        {
            if (itemName == i.name)
            {
                if(i.GetType() == typeof(HealingItem))
                {
                    
                    foreach(Transform invenSlot in equipInven.transform)
                    {
                        if(invenSlot.childCount != 0)
                        {
                            DisplayItem itemDetails = invenSlot.GetChild(0).GetComponent<DisplayItem>();
                            if(itemDetails.name == i.name)  //player already got that item in his inven
                            {
                                itemDetails.AddItemCount();
                                repeatedItem = true;
                            }
                        }
                    }

                    if (repeatedItem == false)
                    {
                        InstantiateItem(i);
                    }
                }
                else
                {
                    InstantiateItem(i);
                }

                
            }
        }
        
    }

    private void InstantiateItem(Item addedItem)
    {
        GameObject emptySlot = null;
        foreach (Transform child in equipInven.transform)
        {
            if (child.childCount == 0 && emptySlot == null)
            {
                //emptyIndex = child.transform.GetSiblingIndex();
                emptySlot = child.gameObject;
            }
        }

        GameObject itemGo = Instantiate(item);
        itemGo.transform.SetParent(emptySlot.transform);
        itemGo.transform.position = emptySlot.transform.position;
        //RectTransform rt = itemGo.GetComponent<RectTransform>();
        //itemGo.GetComponent<RectTransform>().sizeDelta = emptySlot.GetComponent<RectTransform>().sizeDelta;
        DisplayItem disItem = itemGo.GetComponent<DisplayItem>();
        disItem.itemCount += 1; //num of items at hand
        disItem.DisplayItemGui(addedItem);
        ManageItem manItem = itemGo.GetComponent<ManageItem>();
        manItem.player = gameData.GetComponent<Player>();
        manItem.equipmentGo = equipGo;
        Button itemBtn = itemGo.GetComponent<Button>();
        itemBtn.onClick.AddListener(delegate () { SelectItem(itemGo); });
    }

}
