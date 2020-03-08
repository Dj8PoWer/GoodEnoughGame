using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonLobby : MonoBehaviourPunCallbacks
{

    public static PhotonLobby lobby;

    public GameObject joinButton;
    public GameObject cancelButton;

    public Text serverName;

    private void Awake()
    {
        lobby = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Connects to the photon server
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player connected to the photon server");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
        joinButton.SetActive(true);
    }

    public void OnJoinButtonClick()
    {
        joinButton.SetActive(false);
        cancelButton.SetActive(true);
        JoinRoom();
    }

    public void JoinRoom()
    {
        if (serverName.text != "")
        {
            Debug.Log("Trying to join " + serverName.text);
            PhotonNetwork.JoinRoom(serverName.text);
        }
        else
        {
            Debug.Log("Trying to join a RandomRoom");
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Player failed to join a room");
        CreateRoom(serverName.text);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Player failed to join a room");
        CreateRoom(serverName.text);
    }

    void CreateRoom(string name = "")
    {
        Debug.Log("Trying to create a new room");
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSettings.multiplayerSettings.maxPlayer };
        if (serverName.text != "")
        {
            PhotonNetwork.CreateRoom(serverName.text, roomOps);
        }
        else
        {
            int randomRoomName = Random.Range(0, 10000);
            PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room, there must be a room with the same name");
        CreateRoom();
    }

    public void OnCancelButtonClick()
    {
        Debug.Log("Cancelled connection to a room");
        cancelButton.SetActive(false);
        joinButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
