﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class LevelSpawner : MonoBehaviour
{
	public float spawningFrequency = 10;
	public float time;

    public string mob;
    public int level;

    public bool spawning = false;

	public LevelManager manager;


    void Update()
    {
        if (PhotonNetwork.IsMasterClient && spawning)
        {
            if (time <= 0)
            {
                var monster = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", mob), transform.position, Quaternion.identity, 0);
                if (mob == "Zombie")
                    monster.GetComponent<Zombie>().Level = level;
                else if (mob == "Skeleton")
                    monster.GetComponent<Skeleton>().Level = level;
                else if (mob == "Skeleton")
                    monster.GetComponent<Witch>().Level = level;
                else if (mob == "Skeleton")
                    monster.GetComponent<Scorpion>().Level = level;
                else if (mob == "Skeleton")
                    monster.GetComponent<Snake>().Level = level;
                else if (mob == "Skeleton")
                    monster.GetComponent<Zombie>().Level = level;
                else if (mob == "Skeleton")
                    monster.GetComponent<Zombie>().Level = level;
                time = spawningFrequency;
            }
            else
                time -= Time.deltaTime;
        }
    }
}