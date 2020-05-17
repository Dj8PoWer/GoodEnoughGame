using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

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
            {
                minimum = Vector2.Distance(transform.position, play.transform.position);
                target = play;
            }
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
                {
                    minimum = Vector2.Distance(transform.position, play.transform.position);
                    target = play;
                }
            }
        }
        
        if (Vector2.Distance(transform.position, target.transform.position) > stopping)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        
        else if (Vector2.Distance(transform.position, target.transform.position) < stopping &&
                 Vector2.Distance(transform.position, target.transform.position) > back)
            transform.position = this.transform.position;
        
        else if (Vector2.Distance(transform.position, target.transform.position) < back)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -speed * Time.deltaTime);
        if (PhotonNetwork.IsMasterClient)
        {
            if (time <= 0)
            {
                float angle = (Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg) - 90;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.transform.position.y, target.transform.position.x) * Mathf.Rad2Deg);
                //Mathf.Rad2Deg * Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x));


                PV.RPC("RPC_Attack", RpcTarget.All, transform.position, (Vector2)target.transform.position);

                time = originalTime;
            }
            else
            {
                time -= Time.deltaTime;
            }
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
    void RPC_Attack(Vector3 pos, Vector2 target)
    {
        var Object = Instantiate(proj, pos, RotateTowards(target));
        var projectil = Object.GetComponent<MobProjectile>();
        projectil.target = "player";
    }

    private Quaternion RotateTowards(Vector2 target)
    {
        var offset = 0f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
