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
            health = 500 + 10 * level;
            strength = 10 + 2 * level;
        }
    }

    int lvl = 1;
    [SerializeField]
    GameObject loot;

    public int strength;

    Animator animator;
    PhotonView PV;

    int health;
    public int maxHealth;

    public int howManyExplosives = 2;
    public int howManyFives = 4;
    
    //Projectiles
    [SerializeField]
    GameObject explosiveProj;
    
    [SerializeField]
    GameObject proj;

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

    bool halfhp = true;



    #region Pattern
    IEnumerator Pattern1(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            yield return Fives(howManyFives);

            if (halfhp && health < maxHealth / 2)
            {
                halfhp = false;
                StartCoroutine(CageAttack());
            }

            yield return new WaitForSeconds(time);
            yield return ExplosiveAttack(howManyExplosives);
            if (halfhp && health < maxHealth / 2)
            {
                halfhp = false;
                StartCoroutine(CageAttack());
            }

            yield return new WaitForSeconds(time);
            yield return SummonAttack(3);

            if (halfhp && health < maxHealth / 2)
            {
                halfhp = false;
                StartCoroutine(CageAttack());
            }

            yield return new WaitForSeconds(time);
            yield return StarAttack(3);

            if (halfhp && health < maxHealth / 2)
            {
                halfhp = false;
                StartCoroutine(CageAttack());
            }

            yield return new WaitForSeconds(time);
            yield return StarAttack(3);

            if (halfhp && health < maxHealth / 2)
            {
                halfhp = false;
                StartCoroutine(CageAttack());
            }

            yield return new WaitForSeconds(time);
            yield return StarAttack(3);

            if (halfhp && health < maxHealth / 2)
            {
                halfhp = false;
                StartCoroutine(CageAttack());
            }

            yield return new WaitForSeconds(time);
            yield return SummonAttack(3);

            if (halfhp && health < maxHealth / 2)
            {
                halfhp = false;
                StartCoroutine(CageAttack());
            }
        }
    }
    #endregion
    
    #region Explosive
    //Summons 3 Explosivve projectiles (every 2s, each one throws 6 projectiles)
    [PunRPC]
    void RPC_ExplosiveAttack(Vector2 target)
    {
        GameObject Object = Instantiate(explosiveProj, transform.position, RotateTowards(target, 0));
        Object.GetComponent<Explosive>().strength = strength;
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

        for (int i = 0; i < howManyExplosives; i++)
        {
            yield return new WaitForSeconds(4f);
            PV.RPC("RPC_ExplosiveAttack", RpcTarget.All, (Vector2)target.transform.position);
        }
    }
    #endregion
    
    #region Fives
    //Circle Attack Shoots 6 projectile all around the boss, multiple times with an offset each time
    [PunRPC]
    void RPC_FivesAttack(Vector2 target)
    {
        for (int angle = 0; angle <= 15; angle += 3)
        {
            GameObject Object = Instantiate(proj, transform.position, RotateTowards(target, -7.5f + angle));
            var projectil = Object.GetComponent<KnightSnow>();
            projectil.speed = 3f;
            projectil.stun = 1;
            projectil.slow = 1;
            projectil.strength = strength;
            Object.transform.localScale = new Vector3(.5f, .5f, .5f);
            projectil.target = "player";
        }
    }

    IEnumerator Fives(int howManyFives)
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

        for (int i = 0; i < howManyFives; i++)
        {
            yield return new WaitForSeconds(2f);
            PV.RPC("RPC_FivesAttack", RpcTarget.All, (Vector2)target.transform.position);
        }
    }
    #endregion
    
    #region SummonAttack
    //Circle Attack Shoots 6 projectile all around the boss, multiple times with an offset each time
    [PunRPC]
    void RPC_SummonAttack()
    {
        for (int angle = 0; angle < 360; angle += 10)
        {
            var Object = Instantiate(proj, transform.position, Quaternion.Euler(Vector3.forward * (angle)));
            var projectil = Object.GetComponent<KnightSnow>();
            projectil.speed = 3f;
            projectil.stun = 0;
            projectil.slow = 1;
            projectil.strength = strength;
            Object.transform.localScale = new Vector3(.5f, .5f, .5f);
            projectil.target = "player";
        }
    }

    IEnumerator SummonAttack(int attack)
    {
        for (int i = 0; i < attack; i++)
        {
            Debug.Log("Attack");
            PV.RPC("RPC_SummonAttack", RpcTarget.All);
            for (int j = 0; j < 2; j++)
            {
                yield return new WaitForSeconds(0.1f);
                PhotonNetwork.Instantiate(System.IO.Path.Combine("PhotonPrefabs", "Phantom"), transform.position, Quaternion.identity, 0);
            }
            yield return new WaitForSeconds(10f);
        }
    }
    #endregion

    #region StarAttack
    //Circle Attack Shoots 6 projectile all around the boss, multiple times with an offset each time
    [PunRPC]
    void RPC_StarAttack()
    {
        for (int angle = 0; angle < 360; angle += 60)
        {
            var Object = Instantiate(proj, transform.position, Quaternion.Euler(Vector3.forward * (angle)));
            var projectil = Object.GetComponent<KnightSnow>();
            projectil.speed = 6f;
            projectil.slow = 1;
            projectil.stun = 1;
            projectil.strength = strength;
            Object.transform.localScale = new Vector3(.5f, .5f, .5f);
            projectil.target = "player";
        }
    }

    IEnumerator StarAttack(int attack)
    {
        for (int i = 0; i < attack; i++)
        {
            yield return new WaitForSeconds(0.1f);
            PV.RPC("RPC_StarAttack", RpcTarget.All);
        }
    }
    #endregion

    #region CageAttack

    [PunRPC]
    void RPC_CageAttack(Vector2 position)
    {
        for (int angle = 0; angle < 360; angle += 90)
        {
            var Object = Instantiate(proj, position, Quaternion.Euler(Vector3.forward * (angle)));
            var projectil = Object.GetComponent<KnightSnow>();
            projectil.speed = 6f;
            projectil.stun = 1;
            projectil.strength = strength;
            projectil.slow = .8f;
            projectil.target = "player";
        }
    }

    IEnumerator CageAttack()
    {
        while (true)
        {
            PV.RPC("RPC_CageAttack", RpcTarget.All, new Vector2(transform.position.x - 10, transform.position.y - 10));
            PV.RPC("RPC_CageAttack", RpcTarget.All, new Vector2(transform.position.x - 10, transform.position.y + 10));
            PV.RPC("RPC_CageAttack", RpcTarget.All, new Vector2(transform.position.x + 10, transform.position.y - 10));
            PV.RPC("RPC_CageAttack", RpcTarget.All, new Vector2(transform.position.x + 10, transform.position.y + 10));
            yield return new WaitForSeconds(.4f);
        }
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
            manager.leavers[2].SetActive(true);
            if (Random.Range(0, 3) == 0)
            {
                var item = Instantiate(loot, transform.position, Quaternion.identity);
                item.GetComponent<ItemLoot>().GenerateRandomItem(level);
            }
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
