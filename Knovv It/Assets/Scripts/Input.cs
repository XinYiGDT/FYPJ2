using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input : MonoBehaviour {

    private TouchScreenKeyboard keyboard;
    public InputField MainInput;
	// Use this for initialization
	void Start () {
        //// Disable keyboard input space.
        //TouchScreenKeyboard.hideInput = false;
        //keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
        ////keyboard.active = true;

        //TouchScreenKeyboard keyboard;
        MainInput.shouldHideMobileInput = true;
        TouchScreenKeyboard.hideInput = false;
        if (keyboard == null || !TouchScreenKeyboard.visible)
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "xyz");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
