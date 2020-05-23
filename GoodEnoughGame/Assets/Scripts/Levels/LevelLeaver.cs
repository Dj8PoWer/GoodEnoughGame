using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLeaver : MonoBehaviour
{
    public LevelManager lvlmng;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PhotonNetwork.IsMasterClient && other.CompareTag("Player"))
        {
            lvlmng.BackSpawn();
        }
    }
}
