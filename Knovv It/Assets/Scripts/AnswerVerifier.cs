using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerVerifier : MonoBehaviour {

    [SerializeField]
    private InputField answerField;
    [SerializeField]
    private string answerText;

    [SerializeField]
    private Text questionText;

    // Use this for initialization
    void Start () {
        if (GameObject.Find("AnswerField").GetComponent<InputField>())
        {
            answerField = GameObject.Find("AnswerField").GetComponent<InputField>();
            answerText = answerField.text;
            answerField.onEndEdit.AddListener(delegate { EnterAnswer(answerField); });
        }

        if (GameObject.Find("Question").GetComponent<Text>())
        {
            questionText = GameObject.Find("Question").GetComponent<Text>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void EnterAnswer(InputField input)
    {
        if (input.text.Length > 0)
        {
            Debug.Log("Text has been entered");
            char firstLetter = GetFirstLetter(input.text);

            if (CheckAnswer(firstLetter))
            {
                //TODO pass the turn with the answer.
                //TODO end the player turn.
                //TODO rest timer and etc.
            }
            else
            {
                Debug.Log("Answer Failed");
            }
        }
        else if (input.text.Length == 0)
        {
            Debug.Log("Main Input Empty");
        }

        //Clear the text form the text box.
        input.text = "";
    }

    char GetLastLetter(string word)
    {
        //TODO need disable spacebar input or the word need to remove all spaces.
        char lastLetter = word[word.Length - 1];
        Debug.Log(word + " Last Letter" + " is " + lastLetter);

        //Verify is it a letter.
        if (lastLetter >= 'a' || lastLetter <= 'z' || lastLetter >= 'A' || lastLetter <= 'Z')
        {
            return lastLetter;
        }

        return '0'; //Return '0' if failed to get last char.
    }

    char GetFirstLetter(string word)
    {
        //TODO need disable spacebar input or the word need to remove all spaces.
        char firstLetter = word[0];
        Debug.Log(word + " First Letter" + " is " + firstLetter);

        //Verify is it a letter.
        if (firstLetter >= 'a' || firstLetter <= 'z' || firstLetter >= 'A' || firstLetter <= 'Z')
        {
            return firstLetter;
        }

        return '0'; //Return '0' if failed to get first char.
    }

    bool CheckAnswer(char answer)
    {
        //First check is the last letter matches the first letter.
        if (answer == GetLastLetter(questionText.text))
        {
            Debug.Log(answer + " first letter is " + questionText.text + " last letter");

            //TODO check is there such a word exist.
            //TODO check did the word appear before.
            return true;
        }

        Debug.Log(answer + " first letter is not same as " + questionText.text);
        return false;
    }
}
