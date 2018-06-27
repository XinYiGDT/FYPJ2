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
            PhotonNetwork.CreateRoom(createRoomInput.text, new RoomOptions() { MaxPlayers = 2 }, null);
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
        PhotonNetwork.LoadLevel("Gameplay");
    }
}
