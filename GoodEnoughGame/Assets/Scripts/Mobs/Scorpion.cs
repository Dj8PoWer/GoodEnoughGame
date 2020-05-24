using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Scorpion : MonoBehaviour, IPunObservable
{
    public float speed = 1.5f;
    public float stopping = 8f;
    public int health = 50;

    public int strength = 5;

    private bool alive = true;
    public float originalTime;
    private float time;

    private GameObject target;
    public GameObject proj;

    private Animator animMobShooter;

    private int level = 1;

    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            health = 40 + 10 * level;
            strength = 5 + 2 * level;
        }
    }

    [SerializeField]
    GameObject loot;

    private PhotonView PV;

    void Start()
    {

        animMobShooter = GetComponentInChildren<Animator>();
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

        //Flips the object
        if (transform.position.x < target.transform.position.x)
            transform.rotation = new Quaternion(0, 180, 0, 0);
        else
            transform.rotation = new Quaternion(0, 0, 0, 0);

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

        if (PhotonNetwork.IsMasterClient)
        {
            if (time <= 0 && alive)
            {
                float angle = (Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg) - 90;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.transform.position.y, target.transform.position.x) * Mathf.Rad2Deg);
                //Mathf.Rad2Deg * Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x));


                PV.RPC("RPC_Attack", RpcTarget.All);

                time = originalTime;
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int amount, bool willLoot = true)
    {
        health -= amount;
        //animPlayer.SetBool("Hurt", true);
        if (health <= 0 && alive)
        {
            alive = false;
            if (willLoot && Random.Range(0, 3) == 0)
                Instantiate(loot, transform.position, Quaternion.identity);
            //animPlayer.SetBool("Dying", true);
            PV.RPC("RPC_Death", RpcTarget.All);
        }
    }

    [PunRPC]
    void RPC_Attack()
    {
        StartCoroutine(Attack());
        //var Object = Instantiate(proj, pos, RotateTowards(target));
        //var projectil = Object.GetComponent<MobProjectile>();
        //projectil.target = "player";
    }

    [PunRPC]
    void RPC_Death()
    {
        StartCoroutine(Death());
    }

    [PunRPC]
    void RPC_Shoot(Vector3 pos, Vector2 target)
    {
        var Object = Instantiate(proj, pos, RotateTowards(target));
        var projectil = Object.GetComponent<Dart>();
        projectil.strength = strength;
        projectil.target = "player";
    }

    IEnumerator Attack()
    {
        animMobShooter.SetTrigger("attack");
        float tempSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(0.45f);
        if (PhotonNetwork.IsMasterClient)
            PV.RPC("RPC_Shoot", RpcTarget.All, transform.position, (Vector2)target.transform.position);
        speed = tempSpeed;
    }

    IEnumerator Death()
    {
        animMobShooter.SetTrigger("death");
        speed = 0;
        yield return new WaitForSeconds(1);
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(gameObject);
    }

    private Quaternion RotateTowards(Vector2 target)
    {
        var offset = 0f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
            if (PV.IsMine)
                stream.SendNext(level);
        }
        else
        {
            health = (int)stream.ReceiveNext();
            Level = (int)stream.ReceiveNext();
        }
    }
}
