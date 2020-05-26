using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GhostBoss : MonoBehaviour, IPunObservable
{
    float fire;
    float water;
    float air;
    float physical;


    float movetime;
    Vector2 direction;

    private int level = 1;

    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            health = 300 + 10 * level;
            strength = 10 + 2 * level;
        }
    }

    int lvl = 1;
    [SerializeField]
    GameObject loot;

    Animator animator;
    PhotonView PV;

    int health;
    public int maxHealth;
    public int strength;
    
    //Projectiles
    [SerializeField]
    GameObject CircleProj;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        health = maxHealth;
        animator = GetComponent<Animator>();
        PV = gameObject.GetPhotonView();
        if (PhotonNetwork.IsMasterClient)
            StartCoroutine("Pattern1", 3f);
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (movetime <= 0)
            {
                direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                movetime = 1;
            }
            else
                movetime -= Time.deltaTime;

            transform.Translate(direction * Time.deltaTime);
        }
    }



    #region Pattern
    IEnumerator Pattern1(float time)
    {
        while (true)
        {
            if (health < maxHealth / 2)
                time = 1;
            yield return new WaitForSeconds(time);
            yield return BurstAttack(30);
            if (health < maxHealth / 2)
                time = 1;
            yield return new WaitForSeconds(time);
            yield return CircleAttack(30);
            if (health < maxHealth / 2)
                time = 1;
            yield return new WaitForSeconds(time);
            yield return BurstAttack(30);
            if (health < maxHealth / 2)
                time = 1;
            yield return new WaitForSeconds(time);
            yield return SummonAttack(10);
            if (health < maxHealth / 2)
                time /= 2;
            yield return new WaitForSeconds(time);
            yield return CircleAttack(30);
        }
    }
    #endregion

    #region Summon
    //Summons 3 Skeleton Archers twice and shoots 4 projectiles every 1.5s
    [PunRPC]
    void RPC_SummonAttack(int offset)
    {
        for (int angle = 0; angle < 360; angle += 90)
        {
            var Object = Instantiate(CircleProj, transform.position, Quaternion.Euler(Vector3.forward * (angle + offset)));
            var projectil = Object.GetComponent<MobProjectile>();
            projectil.target = "player";
            projectil.strength = strength;
        }
    }

    IEnumerator SummonAttack(int attack)
    {
        animator.SetTrigger("attack");

        for (int j = 0; j < 3; j++)
        {
            yield return new WaitForSeconds(0.1f);
            PhotonNetwork.Instantiate(System.IO.Path.Combine("PhotonPrefabs", "Zombie"), transform.position, Quaternion.identity, 0);
        }

        for (int i = 0; i < attack / 2; i++)
        {
            yield return new WaitForSeconds(1.5f);
            //RPC_SummonAttack(i * 37);
            PV.RPC("RPC_SummonAttack", RpcTarget.All, i * 37);
        }

        yield return new WaitForSeconds(3f);
        PhotonNetwork.Instantiate(System.IO.Path.Combine("PhotonPrefabs", "Skeleton"), transform.position, Quaternion.identity, 0);
        yield return new WaitForSeconds(0.1f);
        PhotonNetwork.Instantiate(System.IO.Path.Combine("PhotonPrefabs", "Witch"), transform.position, Quaternion.identity, 0);
        yield return new WaitForSeconds(0.1f);
        PhotonNetwork.Instantiate(System.IO.Path.Combine("PhotonPrefabs", "Zombie"), transform.position, Quaternion.identity, 0);
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < attack / 2; i++)
        {
            yield return new WaitForSeconds(1.5f);
            //RPC_SummonAttack(i * 37);
            PV.RPC("RPC_SummonAttack", RpcTarget.All, i* 37);
        }
        animator.SetTrigger("idle");
    }

    #endregion

    #region Burst
    //Burst Attack shots rapidly a lot of projectiles in a random direction near the target
    [PunRPC]
    void RPC_BurstAttack(Vector2 target, int random)
    {
        var Object = Instantiate(CircleProj, transform.position, RotateTowards(target, random));
        Object.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        var projectil = Object.GetComponent<MobProjectile>();
        projectil.target = "player";
        projectil.strength = strength;
    }

    IEnumerator BurstAttack(int attack)
    {
        GameObject target = null;

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

        animator.SetTrigger("attack");
        for (int i = 0; i < attack; i++)
        {
            yield return new WaitForSeconds(0.1f);
            //RPC_BurstAttack(target, Random.Range(-10, 10));
            PV.RPC("RPC_BurstAttack", RpcTarget.All, (Vector2)target.transform.position, Random.Range(-10, 10));
        }
        animator.SetTrigger("idle");
    }
    #endregion

    #region CircleAttack
    //Circle Attack Shoots 6 projectile all around the boss, multiple times with an offset each time
    [PunRPC]
    void RPC_CircleAttack(int offset)
    {
        for (int angle = 0; angle < 360; angle += 60)
        {
            var Object = Instantiate(CircleProj, transform.position, Quaternion.Euler(Vector3.forward * (angle+offset)));
            var projectil = Object.GetComponent<MobProjectile>();
            projectil.speed = 3f;
            projectil.strength = strength;
            Object.transform.localScale = new Vector3(.5f, .5f, .5f);
            projectil.target = "player";
        }
    }

    IEnumerator CircleAttack(int attack)
    {
        animator.SetTrigger("attack");
        for (int i = 0; i < attack; i++)
        {
            yield return new WaitForSeconds(0.35f);
            Debug.Log("Attack");
            //RPC_CircleAttack(i * 20);
            PV.RPC("RPC_CircleAttack", RpcTarget.All, i * 20);
        }
        animator.SetTrigger("idle");
    }
    #endregion

    private Quaternion RotateTowards(Vector2 target, float offset = 0f)
    {
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        //animPlayer.SetBool("Hurt", true);
        if (health <= 0)
        {
            LevelManager manager = FindObjectOfType<LevelManager>();
            manager.leavers[0].SetActive(true);
            Instantiate(loot, transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if (PV.IsMine)
                stream.SendNext(level);
        }
        else
        {
            Level = (int)stream.ReceiveNext();
        }
    }
}
