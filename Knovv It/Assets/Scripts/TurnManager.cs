using Photon;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : PunBehaviour
{
    public float turnDuration = 20f;

    PhotonPlayer[] playerList;

    // GUI
    Text playerIGNObj;
    Text enemyIGNObj;
    Text turnObj;
    Text timeLeftObj;

    // Use this for initialization
    void Start () {
        playerList = PhotonNetwork.playerList;
        SetupGUI();
    }

    // Update is called once per frame
    void Update () {
        if (IsMyTurn)
        {
            if (this.IsOver)
                SwitchTurn();

            timeLeftObj.text = RemainingSecondsInTurn.ToString();
        }

        turnObj.text = PhotonNetwork.room.GetWhoseTurn() + " Turn";
    }

    public bool IsMyTurn
    {
        get { return (PhotonNetwork.player.UserId == PhotonNetwork.room.GetWhoseTurn()); }
    }

    public float ElapsedTimeInTurn
    {
        get { return ((float)(PhotonNetwork.ServerTimestamp - PhotonNetwork.room.GetTurnStartTime())) / 1000.0f; }
    }

    public float RemainingSecondsInTurn
    {

        get { return Mathf.Max(0f, this.turnDuration - this.ElapsedTimeInTurn); }
    }

    public bool IsOver
    {
        get { return this.RemainingSecondsInTurn <= 0f; }
    }

    public void SwitchTurn()
    {
        for (int i = 0; i < playerList.Length; i++)
        {
            if (PhotonNetwork.room.GetWhoseTurn() != playerList[i].UserId)
                PhotonNetwork.room.SetWhoseTurn(playerList[i].UserId, true);
        }
    }

    void SetupGUI()
    {
        // Set GUI
        //if (GameObject.Find("IGN").GetComponent<Text>())
        //{
        //    playerIGNObj = GameObject.Find("IGN").GetComponent<Text>();
        //    playerIGNObj.text = PhotonNetwork.player.UserId;
        //}

        //if (GameObject.Find("EnemyIGN").GetComponent<Text>())
        //{
        //    enemyIGNObj = GameObject.Find("EnemyIGN").GetComponent<Text>();

        //    for (int i = 0; i < playerList.Length; i++)
        //    {
        //        if (playerList[i].UserId != PhotonNetwork.player.UserId)
        //            enemyIGNObj.text = playerList[i].UserId;
        //    }

        //}

        if (GameObject.Find("TurnIndicator").GetComponent<Text>())
        {
            turnObj = GameObject.Find("TurnIndicator").GetComponent<Text>();
            turnObj.text = PhotonNetwork.room.GetWhoseTurn() + " Turn";
        }

        if (GameObject.Find("TimeLeft").GetComponent<Text>())
        {
            timeLeftObj = GameObject.Find("TimeLeft").GetComponent<Text>();
            timeLeftObj.text = RemainingSecondsInTurn.ToString();
        }
    }

    //public override void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    //{
    //}
}

public static class TurnStatics
{
    public static readonly string TurnPropKey = "WhoseTurn";
    public static readonly string TurnStartPropKey = "TimeStart";

    public static void SetWhoseTurn(this Room room, string turn, bool setStartTime = false)
    {
        if (room == null || room.CustomProperties == null)
        {
            return;
        }

        ExitGames.Client.Photon.Hashtable turnProps = new ExitGames.Client.Photon.Hashtable();
        turnProps[TurnPropKey] = turn;
        if (setStartTime)
        {
            turnProps[TurnStartPropKey] = PhotonNetwork.ServerTimestamp;
        }

        room.SetCustomProperties(turnProps);
    }

    public static string GetWhoseTurn(this RoomInfo room)
    {
        if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnPropKey))
        {
            return "ERROR";
        }

        return room.CustomProperties[TurnPropKey].ToString();
    }

    public static int GetTurnStartTime(this RoomInfo room)
    {
        if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnStartPropKey))
        {
            return 0;
        }

        return (int)room.CustomProperties[TurnStartPropKey];
    }
}