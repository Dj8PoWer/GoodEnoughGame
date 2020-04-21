using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level = 0;

    public Level1Spawner1 spawn1_1;
    public Level1Spawner2 spawn1_2;
    public Level1Spawner3 spawn1_3;

    public Level1SpawnPoint spawn1;

    void Start()
    {
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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        switch (level)
        {
            case 1:
                SendTo(players, spawn1.gameObject);
                
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
}
