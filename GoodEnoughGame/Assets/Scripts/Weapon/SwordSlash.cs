using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    float time = 0.4f;
    
    public AudioClip spawn;
    AudioSource audio;
    
    public int strength = 10;
    public string target = "";

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(spawn, 0.8F);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && target == "player")
        {
            PlayerManager p = other.GetComponent<PlayerManager>();
            p.TakeDamage(strength);
        }
        if (other.CompareTag("Witch") && target == "mob")
        {
            Witch p = other.GetComponent<Witch>();
            p.TakeDamage(strength);
        }
        if (other.CompareTag("Zombie") && target == "mob")
        {
            Zombie p = other.GetComponent<Zombie>();
            p.TakeDamage(strength);
        }
        if (other.CompareTag("Skeleton") && target == "mob")
        {
            Skeleton p = other.GetComponent<Skeleton>();
            p.TakeDamage(strength);
        }
        if (other.CompareTag("GhostBoss") && target == "mob")
        {
            GhostBoss p = other.GetComponent<GhostBoss>();
            p.TakeDamage(strength);
        }
    }

}
