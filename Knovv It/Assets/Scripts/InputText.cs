using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour {

    string Word = null;
    int WordIndex = 0;
    string Alpha;
    public Text myInput = null;

	// Use this for initialization
	public void alphabetFunction (string Alphabet)
    {
        WordIndex++;
        Word = Word + Alphabet;
        myInput.text = Word;
	}

	
    public void delelteFunction ()
    {

    }
}
