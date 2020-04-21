using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelAccess1 : MonoBehaviour
{
    public LevelManager lvlmng;
    public int lvl = 1;
    
    void Start()
    {
        //empty
    }

    void Update()
    {
        //empty
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PhotonNetwork.IsMasterClient && other.CompareTag("Player"))
        {
            lvlmng.level = lvl;
            lvlmng.LoadLevel(lvlmng.level);
        }
    }
}
