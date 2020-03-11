using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour {

    public Text nameText;

    public Scrollbar hpScrollbar;
    private Text hpScrollText;
    public Scrollbar manaScrollbar;
    private Text manaScrollText;

    public Text lvlText;
    public Scrollbar expScrollbar;
    private Text expScrollText;

    public GameObject gameData;
    private Player player;

	// Use this for initialization
	void Start () {
        player = gameData.GetComponent<Player>();

        hpScrollText = hpScrollbar.transform.GetChild(1).GetComponent<Text>();
        manaScrollText = manaScrollbar.transform.GetChild(1).GetComponent<Text>();
        expScrollText = expScrollbar.transform.GetChild(1).GetComponent<Text>();

        nameText.text = player.name;
	}

    private void UpdateStatsPanel()
    {
        float hpRatio = player.currentHp / player.maxHp;
        hpScrollbar.size = hpRatio;
        hpScrollText.text = player.currentHp + " / " + player.maxHp;

        float manaRatio = player.currentMana / player.maxMana;
        manaScrollbar.size = manaRatio;
        manaScrollText.text = player.currentMana + " / " + player.maxMana;

        lvlText.text = "Lv. " + player.lvl;

        float expRatio = player.surplusExp / (player.surplusExp + player.expToLvl);
        expScrollbar.size = expRatio;
        expScrollText.text = player.surplusExp + " / " + (player.surplusExp + player.expToLvl);
    }
	
	// Update is called once per frame
	void Update () {
        UpdateStatsPanel();
	}
}
