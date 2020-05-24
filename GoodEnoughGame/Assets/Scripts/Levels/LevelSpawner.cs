using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class LevelSpawner : MonoBehaviour
{
    public float dif1_spawningFrequency = 10f;
    public float dif1_time = 15f;
	public float dif2_spawningFrequency;
    public float dif2_time;
	public float dif3_spawningFrequency;
    public float dif3_time;

	private float spawningFrequency;
	private float time;

    public string mob;

    public bool spawning = false;

	public LevelManager manager;

    void Start()
    {
		spawningFrequency = dif1_spawningFrequency;

		switch (manager.difficulty)
        {
            case 1:
                spawningFrequency = dif1_spawningFrequency;
                break;
            case 2:
                spawningFrequency = dif2_spawningFrequency;
                break;
			case 3:
                spawningFrequency = dif3_spawningFrequency;
                break;
            default:
                break;
        }

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