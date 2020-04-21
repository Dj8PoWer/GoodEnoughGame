using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelManager : MonoBehaviour
{
    public int level = 0;
    PhotonView PV;

    public Level1Spawner1 spawn1_1;
    public Level1Spawner2 spawn1_2;
    public Level1Spawner3 spawn1_3;

    public Level1SpawnPoint spawn1;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        //spawn1_1 = GetComponent<Level1Spawner1>();
        //spawn1_2 = GetComponent<Level1Spawner2>();
        //spawn1_3 = GetComponent<Level1Spawner3>();
        //spawn1 = GetComponent<Level1SpawnPoint>();
    }

    void Update()
    {
        //
    }

    public void LoadLevel(int level)
    {

        
        switch (level)
        {
            case 1:
                //SendTo(players, spawn1.gameObject);
                PV.RPC("StartLvl", RpcTarget.All, spawn1.transform.position);
                spawn1_1.spawning = true;
                spawn1_2.spawning = true;
                spawn1_3.spawning = true;
                break;

                default:
                    break;
        }
    }

    public void SendTo(GameObject[] players, GameObject dest)
    {
        foreach (var ele in players)
        {
            ele.transform.position = dest.transform.position;
        }
    }

    [PunRPC]
    private void StartLvl(Vector3 position)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            if (player.GetPhotonView().IsMine)
            {
                player.GetComponent<PlayerManager>().Teleport(position);
            }
        }
            
    }
}
