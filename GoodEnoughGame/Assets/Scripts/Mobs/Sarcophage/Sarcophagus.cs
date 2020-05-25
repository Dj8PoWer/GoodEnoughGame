using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Sarcophagus : MonoBehaviour, IPunObservable
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
            health = 40 + 10 * level;
        }
    }

    int lvl = 1;
    [SerializeField]
    GameObject loot;

    PhotonView PV;

    int health;
    public int maxHealth;


    //Projectiles
    [SerializeField]
    GameObject CircleProj;
    [SerializeField]
    GameObject Headseek;
    [SerializeField]
    GameObject WallProjectil;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        health = maxHealth;
        PV = gameObject.GetPhotonView();
        if (PhotonNetwork.IsMasterClient)
            StartCoroutine("Pattern1", 3f);
    }

    private void Update()
    {
    }



    #region Pattern
    IEnumerator Pattern1(float time)
    {
        while (true)
        {
            Debug.Log("pattern");
            //yield return new WaitForSeconds(time);
            //yield return TriangleAttack(15);

            yield return new WaitForSeconds(time);
            yield return WallAttack(3);

            yield return new WaitForSeconds(time);
            //yield return BurstAttack(30);

            yield return new WaitForSeconds(time);
            //yield return SummonAttack(10);

            yield return new WaitForSeconds(time);
            //yield return CircleAttack(30);
        }
    }
    #endregion

    #region Summon
    //Summons 3 Skeleton Archers twice and shoots 4 projectiles every 1.5s
    [PunRPC]
    void RPC_TriangeAttack(Vector2 target)
    {
        for (int angle = 0; angle < 360; angle += 120)
        {
            var Object = Instantiate(CircleProj, transform.position, RotateTowards(target, angle));
            var projectil = Object.GetComponent<MobProjectile>();
            projectil.target = "player";
        }
    }

    IEnumerator TriangleAttack(int attack)
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

        for (int i = 0; i < attack; i++)
        {
            yield return new WaitForSeconds(1.5f);
            PV.RPC("RPC_TriangeAttack", RpcTarget.All, (Vector2)target.transform.position);
        }
    }

    #endregion

    #region Head Seeker
    //Burst Attack shots rapidly a lot of projectiles in a random direction near the target
    [PunRPC]
    void RPC_HeadSeekerAttack()
    {
        var Object = Instantiate(Headseek, transform.position, Quaternion.Euler(Vector3.forward * -90));
        var projectil = Object.GetComponent<Headseeker>();
        
        projectil.target = "player";
    }

    IEnumerator HeadSeekerAttack(int attack)
    {
        for (int i = 0; i < attack; i++)
        {
            yield return new WaitForSeconds(2f);
            //RPC_BurstAttack(target, Random.Range(-10, 10));
            PV.RPC("RPC_HeadSeekerAttack", RpcTarget.All);
        }
    }
    #endregion

    #region CircleAttack
    //Circle Attack Shoots 6 projectile all around the boss, multiple times with an offset each time
    [PunRPC]
    void RPC_WallAttack(Vector2 position)
    {
        var Object = Instantiate(WallProjectil, position, Quaternion.identity);
        var projectil = Object.GetComponent<WallProjectile>();
        Object.transform.localScale = new Vector3(.5f, .5f, .5f);
        projectil.target = "player";
    }

    IEnumerator WallAttack(int attack)
    {
        for (int i = 0; i < attack; i++)
        {
            //RPC_CircleAttack(i * 20);
            for (int j = -10; j <= 10; j++)
            {
                if (Random.Range(0, 3) == 0)
                {
                    PV.RPC("RPC_WallAttack", RpcTarget.All, new Vector2(transform.position.x - 10, transform.position.y + j));
                }
            }
            yield return new WaitForSeconds(10f);
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
