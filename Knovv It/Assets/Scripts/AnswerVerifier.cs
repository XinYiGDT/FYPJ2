using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum EVENT_CATEGORY
{
    CHECK_DATA = 1,
    SENDNRECEIVED_WORD
};

public class AnswerVerifier : MonoBehaviour
{

    [SerializeField]
    private InputField answerField;
    [SerializeField]
    private string answerText;

    [SerializeField]
    private GameObject text01;
    [SerializeField]
    private GameObject textMain;

    [SerializeField]
    private Text questionText;

    [SerializeField]
    private Text scoreText;
    private int score;

    [SerializeField]
    private Timer gameTimer;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.OnEventCall += this.OnEvent;

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

        if (GameObject.Find("ScoreText").GetComponent<Text>())
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }

        if (GameObject.Find("Text(1)").GetComponent<Text>())
        {
            text01 = GameObject.Find("Text(1)");
        }

        if (GameObject.Find("Text").GetComponent<Text>())
        {
            textMain = GameObject.Find("Text");
        }
    }

    private void Update()
    {

    }
    public void EnterAnswer(InputField input)
    {
        if (input.text.Length > 0)
        {
            Debug.Log("Text has been entered");
            //char firstLetter = GetFirstLetter(input.text);
            answerText = input.text;
            CheckAnswer(input.text);
        }
        else if (input.text.Length == 0)
        {
            Debug.Log("Main Input Empty");
        }

        //Clear the text form the text box.
        answerField.text = "";
    }

    char GetLastLetter(string word)
    {
        //TODO need disable spacebar input or the word need to remove all spaces.
        string temp = word.ToLower();
        char lastLetter = temp[temp.Length - 1];
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
        string temp = word.ToLower();
        char firstLetter = temp[0];
        Debug.Log(word + " First Letter" + " is " + firstLetter);

        //Verify is it a letter.
        if (firstLetter >= 'a' || firstLetter <= 'z' || firstLetter >= 'A' || firstLetter <= 'Z')
        {
            return firstLetter;
        }

        return '0'; //Return '0' if failed to get first char.
    }

    void CheckAnswer(string answer)
    {
        //First check is the last letter matches the first letter.
        if (GetFirstLetter(answer) == GetLastLetter(questionText.text))
        {
            Debug.Log(answer + " first letter is " + questionText.text + " last letter");

            //TODO check is there such a word exist.
            CallRequest(answer, EVENT_CATEGORY.CHECK_DATA);
            //TODO check did the word appear before.
        }
        else
            Debug.Log(answer + " first letter is not same as " + questionText.text);
    }

    public void CallRequest(string answer, EVENT_CATEGORY cat)
    {
        //PhotonNetwork.OnEventCall += this.OnEvent;

        byte evCode = (byte)cat;
        string contentMessage = answer;
        byte[] content = Encoding.UTF8.GetBytes(contentMessage);
        bool reliable = true;
        PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
    }

    private void OnEvent(byte eventCode, object content, int senderID)
    {
        if (eventCode == (byte)EVENT_CATEGORY.CHECK_DATA && (senderID <= 0))
        {
            string RecvdMessage = content.ToString();
            Debug.Log(RecvdMessage);
            //TODO IF ANSWER CORRECT DO ALL THE NESSCESSY STUFF
            IsAnswerExist(RecvdMessage);
        }
        else if (eventCode == (byte)EVENT_CATEGORY.SENDNRECEIVED_WORD)
        {
            string RecvdMessage = content.ToString();
            string[] subStrings = RecvdMessage.Split('=');
            Debug.Log(RecvdMessage);
            Debug.Log(PhotonNetwork.AuthValues.UserId);

            if (subStrings[0] != PhotonNetwork.AuthValues.UserId)
            {
                Debug.Log("subStrings1 " + subStrings[0]);
                Debug.Log("subStrings2 " + subStrings[1]);
                BeginTurn(subStrings[1]);
            }
        }

        //PhotonNetwork.OnEventCall -= OnEvent;
    }

    private void BeginTurn(string recvdMessage)
    {
        //TODO reset timer and etc.
        questionText.text = recvdMessage;
    }

    private void IsAnswerExist(string returnMsg)
    {
        if (returnMsg == "success")
        {
            //TODO pass the turn with the answer.
            //TODO end the player turn.
            
            CallRequest(answerText, EVENT_CATEGORY.SENDNRECEIVED_WORD);
            //End Timer all that blah blah.
            AddScore();
           // text01.GetComponent<FloatingText>().enabled = true;
            //textMain.enabled = false;
            gameTimer.ResetTimer();
        }
    }

    private void AddScore()
    {
        score += (100 * answerText.Length);
        scoreText.text = "Score: " + score.ToString();
    }
}