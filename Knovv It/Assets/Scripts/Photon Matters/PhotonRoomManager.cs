using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonRoomManager : MonoBehaviour {

    public InputField createRoomInput, joinRoomInput, nameInput;

    public void OnClickCreateRoom()
    {
        if (createRoomInput.text.Length >= 1)
        {
            PhotonNetwork.AuthValues = new AuthenticationValues(nameInput.text);
            PhotonNetwork.CreateRoom(createRoomInput.text, new RoomOptions() { MaxPlayers = 2, PublishUserId = true }, null);
        }
    }

    public void OnClickJoinRoom()
    {
        PhotonNetwork.AuthValues = new AuthenticationValues(nameInput.text);
        PhotonNetwork.JoinRoom(joinRoomInput.text);
    }

    private void OnJoinedRoom()
    {
        Debug.Log("Connected to the room");

        if (PhotonNetwork.playerList.Length == 2)
        {
            PhotonNetwork.LoadLevel("Gameplay");
        }
        else
        {
            Debug.Log("Not Enough PLayers");
        }
        
    }

    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        //Doesn't get called on the local player, just remote players, so you would still need something to handle on the second player
        if (PhotonNetwork.playerList.Length == 2)
        {
            ExitGames.Client.Photon.Hashtable timeProps = new ExitGames.Client.Photon.Hashtable();
            timeProps["TotalTime"] = PhotonNetwork.ServerTimestamp;
            PhotonNetwork.room.SetCustomProperties(timeProps);

            PhotonNetwork.room.SetWhoseTurn(newPlayer.UserId, false);
            PhotonNetwork.LoadLevel("Gameplay");
        }
        else
        {
            Debug.Log("Not Enough PLayers");
        }

    }
}
