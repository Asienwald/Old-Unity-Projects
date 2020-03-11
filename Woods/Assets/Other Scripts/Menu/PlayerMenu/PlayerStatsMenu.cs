using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStatsMenu : MonoBehaviour {

    private Text luaText;
    public Text hpText;
    private Text dmgText;
    private Text mdmgText;
    private Text defText;
    private Text mdefText;
    private Text statPtsText;

    private GameObject playerStatsMenu;
    private Text strText;
    private Text vitText;
    private Text wisText;
    private int statPtsToBeUsed;
    public int statPtsUsed;
    private int currentStr;
    private int currentVit;
    private int currentWis;
    public int strToBeAdded;
    private int vitToBeAdded;
    private int wisToBeAdded;

    private bool cantAdd;
    private bool cantMinus;

    public GameObject gameData;
    Player player;

    private int hpToBeAdded;


	// Use this for initialization
	void Start () {
        player = gameData.GetComponent<Player>();;

        luaText = transform.Find("LuaText").GetComponent<Text>();
        dmgText = transform.Find("DmgText").GetComponent<Text>();
        mdmgText = transform.Find("MdmgText").GetComponent<Text>();
        defText = transform.Find("DefText").GetComponent<Text>();
        mdefText = transform.Find("MdefText").GetComponent<Text>();
        statPtsText = transform.Find("StatPtsText").GetComponent<Text>();

        strText = transform.Find("strText").GetComponent<Text>();
        vitText = transform.Find("vitText").GetComponent<Text>();
        wisText = transform.Find("wisText").GetComponent<Text>();

        currentStr = int.Parse(strText.text);
        currentVit = int.Parse(vitText.text);
        currentWis = int.Parse(wisText.text);


        statPtsToBeUsed = player.statPts;

        UpdateStatText();
    }

    private void UpdateStatText()
    {
        luaText.text = player.lua.ToString();
        dmgText.text = player.dmg.ToString();
        mdmgText.text = player.mdmg.ToString();
        defText.text = player.def.ToString();
        mdefText.text = player.mdef.ToString();
        statPtsText.text = player.statPts.ToString();

        strText.text = player.str.ToString();
        vitText.text = player.vit.ToString();
        wisText.text = player.wis.ToString();
    }


    public void AddStr()
    {
        if(statPtsToBeUsed == 0)
        {
            return;
        }
        else
        {
            statPtsToBeUsed -= 1;
            statPtsText.text = statPtsToBeUsed.ToString();

            strToBeAdded += 1;
            strText.text = (player.str + strToBeAdded).ToString();
            strText.color = Color.red;

            dmgText.color = Color.red;
            dmgText.text = (player.dmg + (strToBeAdded * player.lvl)).ToString();

            Debug.Log(strToBeAdded);
            statPtsUsed = strToBeAdded + vitToBeAdded + wisToBeAdded;
        }
    }
    public void MinusStr()
    {
        if(statPtsToBeUsed == player.statPts || (player.str + strToBeAdded) <= 1)
        {
            return;
        }
        else
        {
            statPtsToBeUsed += 1;
            statPtsText.text = statPtsToBeUsed.ToString();

            strToBeAdded -= 1;
            strText.text = (player.str + strToBeAdded).ToString();
            strText.color = Color.red;

            dmgText.color = Color.red;
            dmgText.text = (player.dmg + (strToBeAdded * player.lvl)).ToString();
            if(strToBeAdded == 0)
            {
                strText.color = Color.white;
                dmgText.color = Color.white;
            }
            statPtsUsed = strToBeAdded + vitToBeAdded + wisToBeAdded;
        }
    }

    public void AddVit()
    {
        if (statPtsToBeUsed == 0)
        {
            return;
        }
        else
        {
            statPtsToBeUsed -= 1;
            statPtsUsed += 1;
            statPtsText.text = statPtsToBeUsed.ToString();

            vitToBeAdded += 1;
            vitText.text = (player.vit + vitToBeAdded).ToString();
            vitText.color = Color.red;

            defText.color = Color.red;
            defText.text = (player.def + (vitToBeAdded * player.lvl)).ToString();

            hpText.color = Color.red;
            player.maxHp -= hpToBeAdded;
            hpToBeAdded = player.lvl * 10 * vitToBeAdded;
            player.maxHp += hpToBeAdded;
            statPtsUsed = strToBeAdded + vitToBeAdded + wisToBeAdded;
        }
    }
    public void MinusVit()
    {
        if (statPtsToBeUsed == player.statPts || (player.vit + vitToBeAdded) <= 1)
        {
            return;
        }
        else
        {
            statPtsToBeUsed += 1;
            statPtsText.text = statPtsToBeUsed.ToString();

            vitToBeAdded -= 1;
            vitText.text = (player.vit + vitToBeAdded).ToString();
            vitText.color = Color.red;

            defText.color = Color.red;
            defText.text = (player.def + (vitToBeAdded * player.lvl)).ToString();

            hpText.color = Color.red;
            player.maxHp -= hpToBeAdded;
            hpToBeAdded = player.lvl * 10 * vitToBeAdded;
            player.maxHp += hpToBeAdded;
            Debug.Log(player.maxHp + player.lvl * 10 * vitToBeAdded);
            if (vitToBeAdded == 0)
            {
                vitText.color = Color.white;
                defText.color = Color.white;
                hpText.color = Color.white;
            }

            statPtsUsed = strToBeAdded + vitToBeAdded + wisToBeAdded;
        }
    }

    public void AddWis()
    {
        if (statPtsToBeUsed <= 0)
        {
            return;
        }
        else
        {
            statPtsToBeUsed -= 1;
            statPtsText.text = statPtsToBeUsed.ToString();

            wisToBeAdded += 1;
            wisText.text = (player.wis + wisToBeAdded).ToString();
            wisText.color = Color.red;

            mdmgText.color = Color.red;
            mdmgText.text = (player.mdmg + (wisToBeAdded * player.lvl)).ToString();

            mdefText.color = Color.red;
            mdefText.text = (player.mdef + (wisToBeAdded * player.lvl)).ToString();

            statPtsUsed = strToBeAdded + vitToBeAdded + wisToBeAdded;
        }
    }
    public void MinusWis()
    {
        if (statPtsToBeUsed == player.statPts || (player.wis + wisToBeAdded) <= 1)
        {
            return;
        }
        else
        {
            statPtsToBeUsed += 1;
            statPtsText.text = statPtsToBeUsed.ToString();

            wisToBeAdded -= 1;
            wisText.text = (player.wis + wisToBeAdded).ToString();
            wisText.color = Color.red;

            mdmgText.color = Color.red;
            mdmgText.text = (player.mdmg + (wisToBeAdded * player.lvl)).ToString();

            mdefText.color = Color.red;
            mdefText.text = (player.mdef + (wisToBeAdded * player.lvl)).ToString();
            if (wisToBeAdded == 0)
            {
                wisText.color = Color.white;
                mdmgText.color = Color.white;
                mdefText.color = Color.white;
            }

            statPtsUsed = strToBeAdded + vitToBeAdded + wisToBeAdded;
        }
    }

    public void ConfirmStats()
    {
        player.AddStr(strToBeAdded);
        player.AddVit(vitToBeAdded);
        player.AddWis(wisToBeAdded);
        
        foreach(Transform child in transform)
        {
            if(child.GetComponent<Text>() != null)
            {
                Text text = child.GetComponent<Text>();
                text.color = Color.white;
            }
        }
        hpText.color = Color.white;
        UpdateStatText();

        strToBeAdded = 0;
        vitToBeAdded = 0;
        wisToBeAdded = 0;
    }
    
}
