using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AnswerVerifier : MonoBehaviour
{
    public const byte B_Check_Data = 10;
    public const byte B_Send_Word = 11;

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

    [SerializeField]
    TurnManager m_turnManager;

    // Use this for initialization
    void Start()
    {
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


        if (GetComponent<TurnManager>())
        {
            m_turnManager = GetComponent<TurnManager>();
        }

        if (GameObject.Find("Text (1)").GetComponent<Text>())
        {
            text01 = GameObject.Find("Text (1)");
        }

        if (GameObject.Find("Text").GetComponent<Text>())
        {
            textMain = GameObject.Find("Text");
        }

        if (GameObject.Find("Inner").GetComponent<Timer>())
        {
            gameTimer = GameObject.Find("Inner").GetComponent<Timer>();
        }
    }

    private void Update()
    {

    }

    private void OnEnable()
    {
        PhotonNetwork.OnEventCall += OnEvent;
    }

    private void OnDisable()
    {
        PhotonNetwork.OnEventCall -= OnEvent;
    }

    public void EnterAnswer(InputField input)
    {
        if (PhotonNetwork.room.GetWhoseTurn() == PhotonNetwork.player.UserId)
        {
            if (input.text.Length > 0)
            {
                Debug.Log("Text has been entered");
                answerText = input.text;
                CheckAnswer(input.text);
            }
            else if (input.text.Length == 0)
            {
                Debug.Log("Main Input Empty");
            }
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
            CallRequest(answer, B_Check_Data);
            //TODO check did the word appear before.
        }
        else
            Debug.Log(answer + " first letter is not same as " + questionText.text);
    }

    public void CallRequest(string answer, byte cat)
    {
        if (PhotonNetwork.player.UserId != PhotonNetwork.room.GetWhoseTurn())
            return;

        Debug.Log("Calling " + cat.ToString());

        byte evCode = cat;
        string contentMessage = answer;
        byte[] content = Encoding.UTF8.GetBytes(contentMessage);
        bool reliable = true;
        PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
    }

    private void OnEvent(byte eventCode, object content, int senderID)
    {
        switch (eventCode)
        {
            case B_Check_Data:
                {
                    string RecvdMessage = content.ToString();
                    Debug.Log(RecvdMessage);
                    //TODO IF ANSWER CORRECT DO ALL THE NESSCESSY STUFF
                    IsAnswerExist(RecvdMessage);
                }
                break;
            case B_Send_Word:
                {
                    Debug.Log("Got called");
                    string RecvdMessage = content.ToString();
                    string[] subStrings = RecvdMessage.Split('=');
                    Debug.Log(RecvdMessage);
                    Debug.Log(PhotonNetwork.player.UserId);

                    if (subStrings[0] != PhotonNetwork.player.UserId)
                    {
                        Debug.Log("subStrings1 " + subStrings[0]);
                        Debug.Log("subStrings2 " + subStrings[1]);
                        BeginTurn(subStrings[1]);
                    }
                }
                break;
            default:
                break;
        }
    }

    public void BeginTurn(string recvdMessage)
    {
        questionText.text = recvdMessage;
    }

    public void EndTurn()
    {
        AddScore();
        gameTimer.ResetTimer();
        m_turnManager.SwitchTurn();
    }

    private void IsAnswerExist(string returnMsg)
    {
        if (returnMsg == "success")
        {
            CallRequest(answerText, B_Send_Word);

            text01.GetComponent<FloatingText>().enabled = true;
            textMain.SetActive(false);
            EndTurn();
        }
    }

    private void AddScore()
    {
        score += (100 * answerText.Length);
        scoreText.text = "Score: " + score.ToString();
    }
}