using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KnightSuperBoss : MonoBehaviour, IPunObservable
{
    float fire;
    float water;
    float air;
    float physical;


    //float movetime;
    //Vector2 direction;

    private int level = 1;

    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            health = 40 + 10 * level;
        }
    }

    int lvl = 1;
    [SerializeField]
    GameObject loot;

    Animator animator;
    PhotonView PV;

    int health;
    public int maxHealth;

    public int howManyExplosives = 2;
    
    //Projectiles
    [SerializeField]
    GameObject explosiveProj;

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
        //empty
    }



    #region Pattern
    IEnumerator Pattern1(float time)
    {
        while (true)
        {
            if (health < maxHealth / 2)
                time = 1;
            //yield return new WaitForSeconds(time);
            //yield return BurstAttack(30);
            if (health < maxHealth / 2)
                time = 1;
            yield return new WaitForSeconds(time);
            yield return ExplosiveAttack(howManyExplosives);
            if (health < maxHealth / 2)
                time = 1;
            //yield return new WaitForSeconds(time);
            //yield return BurstAttack(30);
            if (health < maxHealth / 2)
                time = 1;
            //yield return new WaitForSeconds(time);
            //yield return SummonAttack(10);
            if (health < maxHealth / 2)
                time /= 2;
            //yield return new WaitForSeconds(time);
            //yield return CircleAttack(30);
        }
    }
    #endregion

    #region Explosive
    //Summons 3 Explosivve projectiles (every 2s, each one throws 6 projectiles)
    [PunRPC]
    void RPC_ExplosiveAttack(Vector2 target)
    {
        var Object = Instantiate(explosiveProj, transform.position, RotateTowards(target, 0));
    }

    IEnumerator ExplosiveAttack(int howManyExplosives)
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
        for (int i = 0; i < howManyExplosives; i++)
        {
            yield return new WaitForSeconds(4f);
            PV.RPC("RPC_ExplosiveAttack", RpcTarget.All, target);
        }
        animator.SetTrigger("idle");
    }

    #endregion

    /*#region Burst
    //Burst Attack shots rapidly a lot of projectiles in a random direction near the target
    [PunRPC]
    void RPC_BurstAttack(Vector2 target, int random)
    {
        var Object = Instantiate(CircleProj, transform.position, RotateTowards(target, random));
        Object.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        var projectil = Object.GetComponent<MobProjectile>();
        projectil.target = "player";
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
    #endregion*/

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
