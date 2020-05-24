using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class LevelSpawner : MonoBehaviour
{
    public float spawningFrequency = 2f;
    public float time = 7f;
    public string mob;
    public int level;

    public bool spawning = false;

    void Start()
    {
        time = spawningFrequency;
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient && spawning)
        {
            if (time <= 0)
            {
                var monster = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", mob), transform.position, Quaternion.identity, 0);
                if (mob == "Zombie")
                    monster.GetComponent<Zombie>().Level = level;
                time = spawningFrequency;
            }
            else
                time -= Time.deltaTime;
        }
    }
}