using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour {

    string Word = null;
    int WordIndex = -1;
    string Alpha = null;
    string Alpha2 = null;
    char[] nameLetter = new char[30];
    public InputField myInput = null;

	// Use this for initialization
	public void AlphabetFunction (string Alphabet)
    {
        WordIndex++;
        char[] keepLetter;
        keepLetter = Alphabet.ToCharArray();
        nameLetter[WordIndex] = keepLetter[0];
        Alpha = nameLetter[WordIndex].ToString();
        Word = Word + Alpha;
        myInput.text = UppercaseFirst(Word);
	}

	
    public void DelelteFunction ()
    {
        if (WordIndex >= 0)
        {
            WordIndex--;

            Alpha2 = null;
            for(int i=0; i<WordIndex +1; ++i)
            {
                Alpha2 = Alpha2 + nameLetter[i].ToString();
            }
            Word = Alpha2;
            myInput.text = UppercaseFirst(Word);
        }

    }

    public void SendFunction()
    {
        Word = ""; 
    }

    static string UppercaseFirst(string s)
    {
        //check for empty string
        if(string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}
