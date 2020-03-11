using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemDetails : MonoBehaviour {

    //public GameObject equipGo;
    //private GameObject weapSlot;
    //private GameObject bodySlot;
    //private GameObject headSlot;
    //private GameObject accSlot;

    //public GameObject itemDescription;
    //private Text selectedItemDescrip;
    //private Text equippedItemDescrip;

    private DisplayItem itemDetails;

    private List<string> equipTypes = new List<string> {"weapon", "body", "head", "add"};
    private List<string> itemTypes;

    private string wholeText;

    //for all items
    private string nameText;
    private string descripText;
    private string sellPriceText;

    //for equipments
    private string lvlText;
    private string itemTypeText;
    private string abiText;

    //for weapons
    private string atkText;

    //for armor (body/head)
    private string defText;

    //for healing items
    private string cdText;
    private string targetHealText;
    

	// Use this for initialization
	void Start () { 
        itemDetails = GetComponent<DisplayItem>();

        //weapSlot = equipGo.transform.GetChild(3).gameObject;
        //bodySlot = equipGo.transform.GetChild(2).gameObject;
        //headSlot = equipGo.transform.GetChild(1).gameObject;
        //accSlot = equipGo.transform.GetChild(0).gameObject;

        //selectedItemDescrip = itemDescription.transform.GetChild(0).GetComponent<Text>();
        //equippedItemDescrip = itemDescription.transform.GetChild(1).GetComponent<Text>();
    }

    public string ShowDetails()
    {
        wholeText = "";
        nameText = "Name: " + itemDetails.name;
        descripText = itemDetails.description;
        wholeText += nameText + "\n" + descripText + "\n";

        if (equipTypes.Contains(itemDetails.itemType))
        { //item is an equipment
            ShowEquipDetails();
        }else if(itemDetails.itemType == "healing")
        {
            ShowHealingItemDetails();
        }

        sellPriceText = "SellPrice: " + itemDetails.sellPrice;
        wholeText += sellPriceText;

        return wholeText;
    }

    private void ShowHealingItemDetails()
    {
        targetHealText = itemDetails.targetHeal.ToUpper() + " + " + itemDetails.healAmt + "\n";
        cdText = "Cooldown: " + itemDetails.coolDown + "\n";
        wholeText += targetHealText + cdText;
    }


    private void ShowEquipDetails()
    {
        lvlText = "Lvl: " + itemDetails.lvl;
        itemTypeText = "Item Type: " + itemDetails.itemType;
        wholeText += lvlText + "\n" + itemTypeText + "\n";

        if (itemDetails.itemType == "weapon")
        {
            atkText = "Atk :" + itemDetails.atk;
            wholeText += atkText + "\n";
        }
        else if (itemDetails.itemType == "body" || itemDetails.itemType == "head")
        {
            defText = "Def: " + itemDetails.def;
            wholeText += defText + "\n";
        }

        ShowAbi();
    }

    private void ShowAbi()
    {
        if(itemDetails.abiType != null)
        {
            abiText = "Ability: " + itemDetails.abiType + " + " + itemDetails.abiAmt;
            wholeText += abiText + "\n";
        }
    }
}
