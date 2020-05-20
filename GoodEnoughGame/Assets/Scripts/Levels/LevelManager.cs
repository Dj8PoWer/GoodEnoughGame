using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelManager : MonoBehaviour
{
    public int level = 0;
    PhotonView PV;

    public GameObject[] levels;
    
    public GameObject[] spawners1;

    public AudioSource homeMusic;
    public AudioSource levelMusic;

    void Start()
    {
        homeMusic.Play();
        
        PV = GetComponent<PhotonView>();
        //spawn1_1 = GetComponent<Level1Spawner1>();
        //spawn1_2 = GetComponent<Level1Spawner2>();
        //spawn1_3 = GetComponent<Level1Spawner3>();
        //spawn1 = GetComponent<Level1SpawnPoint>();
    }

    void Update()
    {
        //empty
    }

    public void LoadLevel(int level)
    {
        homeMusic.Stop();
        levelMusic.Play();
        
        PV.RPC("StartLvl", RpcTarget.All, levels[level-1].transform.position);

        switch (level)
        {
            case 1:
                foreach (var spawner in spawners1)
                {
                    spawner.GetComponent<LevelSpawner>().spawning = true;
                }
                break;
            case 2:
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
