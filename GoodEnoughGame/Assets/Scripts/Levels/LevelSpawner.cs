using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using System;

public class LevelSpawner : MonoBehaviour
{
	public float spawningFrequency = 10;
	public float time;

    public string[] mobs;
    public int level;

    public bool spawning = false;

	public LevelManager manager;

    void Update()
    {
        if (PhotonNetwork.IsMasterClient && spawning)
        {
            if (time <= 0)
            {
                System.Random rnd = new System.Random();
                int mobsDiversity = mobs.Length;
                string mob = mobs[rnd.Next(mobsDiversity)];
                var monster = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", mob), transform.position, Quaternion.identity, 0);
                
                if (mob == "Zombie" || mob == "Mummy")
                    monster.GetComponent<Zombie>().Level = level;
                else if (mob == "Skeleton")
                    monster.GetComponent<Skeleton>().Level = level;
                else if (mob == "Witch")
                    monster.GetComponent<Witch>().Level = level;
                else if (mob == "Scorpion")
                    monster.GetComponent<Scorpion>().Level = level;
                else if (mob == "Snake")
                    monster.GetComponent<Snake>().Level = level;
                else if (mob.Substring(0, 7) == "Haunted")
                    monster.GetComponent<Haunted>().Level = level;
                else if (mob == "Chest")
                    monster.GetComponent<Chest>().Level = level;
                else if (mob == "Phantom")
                    monster.GetComponent<Phantom>().Level = level;
                time = spawningFrequency;
            }
            else
                time -= Time.deltaTime;
        }
    }
}