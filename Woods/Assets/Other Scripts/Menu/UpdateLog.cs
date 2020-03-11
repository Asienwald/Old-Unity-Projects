using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLog : MonoBehaviour {

    private Text log;
    private string logText;

	// Use this for initialization
	void Start () {
        log = transform.GetChild(0).GetComponent<Text>();
	}
	
	public void AddActionInLog(string text)
    {
        logText = text + "\n" + logText;
        log.text = logText;
    }
}
