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
        if(other.CompareTag("Player") && target == "player")
        {
            Debug.Log("touch");
            PlayerManager p = other.GetComponent<PlayerManager>();
            p.TakeDamage(strength);
        }
        if(other.CompareTag("MobShooter") && target == "mob")
        {
            Debug.Log(" mobtouch");
            MobShooter p = other.GetComponent<MobShooter>();
            p.TakeDamage(strength);
        }
        if(other.CompareTag("MobChaser") && target == "mob")
        {
            Debug.Log("mobtouch");
            MobChaser p = other.GetComponent<MobChaser>();
            p.TakeDamage(strength);
        }
    }
}
