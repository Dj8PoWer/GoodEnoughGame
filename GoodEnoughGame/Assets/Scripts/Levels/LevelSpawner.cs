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
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", mob), transform.position, Quaternion.identity, 0);
                time = spawningFrequency;
            }
            else
                time -= Time.deltaTime;
        }
    }
}