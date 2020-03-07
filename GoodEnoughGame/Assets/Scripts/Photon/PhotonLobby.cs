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
        joinButton.SetActive(true);
    }

    public void OnJoinButtonClick()
    {
        joinButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRoom()
    {
        if (serverName.text != "")
        {
            RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSettings.multiplayerSettings.maxPlayer }; ;
        }
        else
            CreateRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Player failed to join random room");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Trying to create a new room");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte) MultiplayerSettings.multiplayerSettings.maxPlayer };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
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
