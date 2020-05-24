using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int level = 0;
    public int difficulty = 1;
    PhotonView PV;

    public GameObject HubPoint;

    public GameObject[] levels;

    public GameObject[] leavers;
    
    public GameObject[] spawners1;
    public GameObject[] spawners2;

    //public AudioSource homeMusic;
    //public AudioSource levelMusic;
    public AudioSource[] musics;

    public Timer timer;

    void Start()
    {
        foreach (var leaver in leavers)
        {
            leaver.SetActive(false);
        }
        //homeMusic.Play();
        musics[0].Play();

        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        //empty
    }

    public void LoadLevel(int level)
    {
        //homeMusic.Stop();
        //levelMusic.Play();
        musics[0].Stop();
        musics[level].Play();
        
        PV.RPC("StartLvl", RpcTarget.All, levels[level-1].transform.position);
        StartCoroutine(SpawnBoss(level));
        StartTimer();

        switch (level)
        {
            case 1:
                foreach (var spawner in spawners1)
                {
                    spawner.GetComponent<LevelSpawner>().spawning = true;
                }
                break;
            case 2:
                foreach (var spawner in spawners2)
                {
                    spawner.GetComponent<LevelSpawner>().spawning = true;
                }
                break;
            default:
                break;
        }
    }

    public void BackSpawn()
    {
        //homeMusic.Play();
        //levelMusic.Stop();
        musics[0].Play();
        musics[level].Stop();
        level = 0;
        
        PV.RPC("StartLvl", RpcTarget.All, HubPoint.transform.position);
        foreach (var leaver in leavers)
        {
            leaver.SetActive(false);
        }
    }

    IEnumerator SpawnBoss(int selectedlvl)
    {
        yield return new WaitForSeconds(185f);
        KillAll();
        switch (selectedlvl)
        {
            case 1:
                foreach (var spawner in spawners1)
                {
                    spawner.GetComponent<LevelSpawner>().spawning = false;
                }
                PhotonNetwork.Instantiate(System.IO.Path.Combine("PhotonPrefabs", "GhostBoss"), levels[level - 1].transform.position, Quaternion.identity, 0);
                break;
            case 2:
                break;
            case 3:
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

    void KillAll()
    {
        foreach (var mob in FindObjectsOfType<Zombie>())
            mob.TakeDamage(int.MaxValue, false);
        foreach (var mob in FindObjectsOfType<Skeleton>())
            mob.TakeDamage(int.MaxValue, false);
        foreach (var mob in FindObjectsOfType<Witch>())
            mob.TakeDamage(int.MaxValue, false);
    }

    [PunRPC]
    void StartTimer()
    {
        timer.StartTimer();
    }
}
