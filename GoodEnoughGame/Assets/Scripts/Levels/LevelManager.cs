﻿using System.Collections;
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
    public GameObject[] spawners3;

    //public AudioSource homeMusic;
    //public AudioSource levelMusic;
    public AudioSource[] musics;

    public Timer timer;
    public GameObject diff;
    public Text difficultyDisplay;

    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            diff.SetActive(false);

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
        PV.RPC("StartTimer", RpcTarget.All);

        switch (level)
        {
            case 1:
                foreach (var spawner in spawners1)
                {
                    spawner.GetComponent<LevelSpawner>().spawning = true;
                    spawner.GetComponent<LevelSpawner>().level = difficulty;
                }
                break;
            case 2:
                foreach (var spawner in spawners2)
                {
                    spawner.GetComponent<LevelSpawner>().spawning = true;
                    spawner.GetComponent<LevelSpawner>().level = difficulty;
                }
                break;
            default:
                foreach (var spawner in spawners3)
                {
                    spawner.GetComponent<LevelSpawner>().spawning = true;
                    spawner.GetComponent<LevelSpawner>().level = difficulty;
                }
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
                var O = PhotonNetwork.Instantiate(System.IO.Path.Combine("PhotonPrefabs", "GhostBoss"), levels[level - 1].transform.position, Quaternion.identity, 0);
                O.GetComponent<GhostBoss>().Level = difficulty;
                break;
            case 2:
                foreach (var spawner in spawners2)
                {
                    spawner.GetComponent<LevelSpawner>().spawning = false;
                }
                O = PhotonNetwork.Instantiate(System.IO.Path.Combine("PhotonPrefabs", "Sarcophagus"), levels[level - 1].transform.position, Quaternion.identity, 0);
                O.GetComponent<Sarcophagus>().Level = difficulty;
                break;
            case 3:
                foreach (var spawner in spawners3)
                {
                    spawner.GetComponent<LevelSpawner>().spawning = false;
                }
                O = PhotonNetwork.Instantiate(System.IO.Path.Combine("PhotonPrefabs", "KnightSuperBoss"), levels[level - 1].transform.position, Quaternion.identity, 0);
                O.GetComponent<KnightSuperBoss>().Level = difficulty;
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
        foreach (var mob in FindObjectsOfType<Snake>())
            mob.TakeDamage(int.MaxValue, false);
        foreach (var mob in FindObjectsOfType<Scorpion>())
            mob.TakeDamage(int.MaxValue, false);
        foreach (var mob in FindObjectsOfType<Haunted>())
            mob.TakeDamage(int.MaxValue, false);
        foreach (var mob in FindObjectsOfType<Chest>())
            mob.TakeDamage(int.MaxValue, false);
        foreach (var mob in FindObjectsOfType<Phantom>())
            mob.TakeDamage(int.MaxValue, false);

    }

    [PunRPC]
    void StartTimer()
    {
        timer.StartTimer();
    }

    public void ChangeDifficulty(int d)
    {
        difficulty += d;
        if (difficulty <= 0)
            difficulty = 1;
        difficultyDisplay.text = "Mob level = " + difficulty.ToString();
    }
}
