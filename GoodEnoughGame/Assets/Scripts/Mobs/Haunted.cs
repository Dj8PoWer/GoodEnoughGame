using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Haunted : MonoBehaviour, IPunObservable
{
    [SerializeField]
    GameObject loot;

    public float speed = 1.5f;
    public float stopping = 0f;
    public int health = 50;
    public int strength = 5;

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

    private bool Alive = true;
    
    private GameObject target;
    
    public float originalTime = 1f;
    private float time;

    private Animator animMobChaser;

    public AudioSource contactPlayer;

    private PhotonView PV;

    float tempSpeed;
    
    void Start()
    {
        tempSpeed = speed;
        animMobChaser = GetComponentInChildren<Animator>();
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

        //Flips the object
        if (transform.position.x < target.transform.position.x)
            transform.rotation = new Quaternion(0, 0, 0, 0);
        else
            transform.rotation = new Quaternion(0, 180, 0, 0);

        if (Vector2.Distance(transform.position, target.transform.position) > stopping)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        LevelSystem levelSystem = FindObjectOfType<LevelSystem>();
        if (levelSystem != null)
            levelSystem.AddExperience(50);
    }

    public void TakeDamage(int amount, bool willLoot = true)
    {
        health -= amount;
        //animPlayer.SetBool("Hurt", true);
        if (health <= 0 && Alive)
        {
            //animPlayer.SetBool("Dying", true);
            if (willLoot && Random.Range(0, 3) == 0)
                Instantiate(loot, transform.position, Quaternion.identity);
            GetComponent<Collider2D>().enabled = false;
            Alive = false;
            PV.RPC("RPC_Death", RpcTarget.All);
            //Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && time <= 0 && Alive)
        {
            PlayerManager p = other.gameObject.GetComponent<PlayerManager>();
            p.TakeDamage(strength);
            p.Blind(4);
            PV.RPC("RPC_Attack", RpcTarget.All);
            time = originalTime;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag("Player"))
            //contactPlayer.Play();
    }

    [PunRPC]
    void RPC_Attack()
    {
        StartCoroutine(Attack());
    }

    [PunRPC]
    void RPC_Death()
    {
        StartCoroutine(Death());
    }

    IEnumerator Attack()
    {
        float baseSpeed = speed;
        speed = 0;
        animMobChaser.SetTrigger("attack");
        yield return new WaitForSeconds(0.4f);
        speed = baseSpeed;
    }

    IEnumerator Death()
    {
        speed = 0;
        animMobChaser.SetTrigger("death");
        yield return new WaitForSeconds(1.1f);
        PhotonNetwork.Destroy(gameObject);
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
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

    public void SpeedBuff(float duration, float value)
    {
        StartCoroutine(SpeedB(duration, value));
    }

    IEnumerator SpeedB(float duration, float value)
    {
        if (value < 0)
            speed = -Mathf.Abs(speed);
        else
            speed = speed * value;
        yield return new WaitForSeconds(duration);
        speed = tempSpeed;
    }
}
