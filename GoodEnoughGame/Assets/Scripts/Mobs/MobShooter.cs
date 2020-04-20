using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MobShooter : MonoBehaviour
{
    public float speed = 1.5f;
    public float stopping = 8f;
    public float back = 8f;
    public int health = 50;

    public float originalTime;
    private float time;
    
    private GameObject target;
    public GameObject proj;

    private Animator animMobShooter;
    
    private PhotonView PV;
    
    void Start()
    {
        animMobShooter = GetComponent<Animator>();
        time = originalTime;
        
        PV = GetComponent<PhotonView>();
        
        target = null;
        var players = GameObject.FindGameObjectsWithTag("Player");
        float minimum = float.MaxValue;
        foreach (var play in players)
        {
            if (Vector2.Distance(transform.position, play.transform.position) < minimum)
                target = play;
        }
    }

    void Update()
    {
        //old version : target = GameObject.FindWithTag("Player");
        
        if (target == null)
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            float minimum = float.MaxValue;
            foreach (var play in players)
            {
                if (Vector2.Distance(transform.position, play.transform.position) < minimum)
                    target = play;
            }
        }
        
        if (Vector2.Distance(transform.position, target.transform.position) > stopping)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        
        else if (Vector2.Distance(transform.position, target.transform.position) < stopping &&
                 Vector2.Distance(transform.position, target.transform.position) > back)
            transform.position = this.transform.position;
        
        else if (Vector2.Distance(transform.position, target.transform.position) < back)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -speed * Time.deltaTime);

        if (time <= 0)
        {
            PV.RPC("RPC_Attack", RpcTarget.MasterClient, target.transform.position, Quaternion.identity);

            time = originalTime;
        }
        else
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
            Destroy(gameObject);
        }
    }
    
    [PunRPC]
    void RPC_Attack(Vector3 pos, Quaternion rot)
    {
        var Object = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "arrow"), arrowPos.position, arrowRot.rotation, 0);
        var projectil = Object.GetComponent<Arrow>();
        projectil.target = "player";
    }
}
