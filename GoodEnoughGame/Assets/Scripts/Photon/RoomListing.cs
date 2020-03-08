using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviourPunCallbacks
{
    public Text listText;

    private void Start()
    {
        listText = GetComponent<Text>();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("ehl");
        string text = "Server List : \n \n";
        foreach(var info in roomList)
        {
            if (info.PlayerCount != 0)
                text += info.Name + "      " + info.PlayerCount + " / " + info.MaxPlayers + "\n";
        }
        listText.text = text;
    }
}
