using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonConnect : MonoBehaviour {

    public string versionName = "0.1";

    public GameObject view1, view2, view3;

    void Awake()
    {
        Debug.Log("Connecting to photon...");
        PhotonNetwork.ConnectUsingSettings(versionName);
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected to Master");
    }

    private void OnJoinedLobby()
    {
        view1.SetActive(false);
        view2.SetActive(true);
        view3.SetActive(false);
        Debug.Log("On Joined Lobby");
    }

    private void OnDisconnectedFromPhoton()
    {
        if (view1.activeSelf)
            view1.SetActive(false);
        if (view2.activeSelf)
            view2.SetActive(false);
        view3.SetActive(true);
        Debug.Log("Disconnected from server");
    }

    private void OnFailedToConnectToPhoton()
    {

    }
}
