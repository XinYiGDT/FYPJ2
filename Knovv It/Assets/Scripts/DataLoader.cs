using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {
    string URL = "http://localhost/Knovv_IT/Data.php";
    public string[] wordList;
	// Use this for initialization
    void Start()
    {
        Debug.Log("Connection to [" + URL + "]");
        StartCoroutine(LoadData());
    }

    IEnumerator LoadData () {
        WWW wordData = new WWW(URL);
        yield return wordData;

        Debug.Log(wordData.text);
        if (!string.IsNullOrEmpty(wordData.error))
            Debug.Log(wordData.error);

        string wordString = wordData.text;
        //print(wordString);
        //wordList = wordString.Split(';');
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
