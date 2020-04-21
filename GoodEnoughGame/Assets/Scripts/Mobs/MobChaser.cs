using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MobChaser : MonoBehaviour, IPunObservable
{
    public float speed = 1.5f;
    public float stopping = 0f;
    public int health = 50;
    public int strength = 5;
    
    private GameObject target;
    
    public float originalTime = 1f;
    private float time;

    private Animator animMobChaser;

    private PhotonView PV;
    
    void Start()
    {
        animMobChaser = GetComponent<Animator>();
        PV = GetComponent<PhotonView>();
        time = originalTime;
        
        target = null;
        var players = GameObject.FindGameObjectsWithTag("Player");
        float minimum = float.MaxValue;
        foreach (var play in players)
        {
            if (Vector2.Distance(transform.position, play.transform.position) < minimum)
            {
                minimum = Vector2.Distance(transform.position, play.transform.position);
                target = play;
            }
        }
    }

    void Update()
    {
        // old version : target = GameObject.FindWithTag("Player");

        if (target == null)
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            float minimum = float.MaxValue;
            foreach (var play in players)
            {
                if (Vector2.Distance(transform.position, play.transform.position) < minimum)
                {
                    minimum = Vector2.Distance(transform.position, play.transform.position);
                    target = play;
                }
            }
        }
        
        if (Vector2.Distance(transform.position, target.transform.position) > stopping)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        //animPlayer.SetBool("Hurt", true);
        if (health <= 0)
        {
            //animPlayer.SetBool("Dying", true);
            PhotonNetwork.Destroy(gameObject);
            //Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && time <= 0)
        {
            PlayerManager p = other.GetComponent<PlayerManager>();
            p.TakeDamage(strength);

            time = originalTime;
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int)stream.ReceiveNext();
        }
    }
}
