using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input : MonoBehaviour {

    private TouchScreenKeyboard keyboard;
	// Use this for initialization
	void Start () {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
