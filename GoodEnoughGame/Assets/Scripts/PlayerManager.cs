using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    WeaponManager weaponManager;
    [SerializeField]
    GameObject loot;

    public GameObject[] spawnpoints;

    [SerializeField]
    int baseSpeed = 3;
    float tempSpeed = 3;
    float speed = 3;

    public int baseHealth = 100;
    private float health = 100;
    private float Health
    {
        get { return health; }
         set
         {
            health = value;
            if (health > baseHealth)
                health = baseHealth;
            if (health > 0)
                hpBar.fillAmount = health / baseHealth;
            else
            {
                health = 0;
                hpBar.fillAmount = health / baseHealth;
            }
         }
    }

    private float hpRegen = 1;

    private float armor;
    private float fireRes;
    private float waterRes;
    private float airRes;

    private PhotonView PV;

    private Animator animPlayer;

    private Image hpBar;
    public Text respawnText;

    bool alive = true;

    public GameObject cam;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        respawnText = GameObject.Find("RespawnText").GetComponent<Text>();
        respawnText.gameObject.SetActive(false);
        spawnpoints = GameObject.FindGameObjectsWithTag("spawnpoint");

        StartCoroutine(Regen());
        // Assings components to variables
        animPlayer = player.GetComponent<Animator>();
        PV = GetComponent<PhotonView>();
        hpBar = GameObject.Find("HealthBar").GetComponent<Image>();
        // Enables the player's camera to avoid conflict if playing with other players
        if (PV.IsMine)
        {
            cam.SetActive(true);
        }

        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            Deplacement();
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }

    }

    void Deplacement()
    {
        Vector2 deplacement = new Vector2(0,0);
        if (Input.GetKey(KeyCode.W))
        {
            deplacement.y++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            deplacement.x--;
        }
        if (Input.GetKey(KeyCode.S))
        {
            deplacement.y--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            deplacement.x++;
        }
        bool diag = deplacement.x != 0 && deplacement.y != 0;
        transform.Translate(deplacement * speed * Time.deltaTime * (diag? Mathf.Sin(Mathf.Sqrt(2)/2) : 1));

        // Animates the player according to his movements
        if (deplacement != Vector2.zero)
            animPlayer.SetBool("Moving", true);
        else
            animPlayer.SetBool("Moving", false);
    }

    void Flip()
    {
        //if camera is on the right side of the screen, flips the character
        if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x < 0.5f)
            player.transform.rotation = new Quaternion(0, 180, 0, 0);
        else
            player.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    
    public void TakeDamage(int amount)
    {
        Health -= amount;
        //animPlayer.SetBool("Hurt", true);
        if (health <= 0 && alive)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        alive = false;
        respawnText.gameObject.SetActive(true);
        animPlayer.SetTrigger("death");
        weaponManager.gameObject.SetActive(false);
        tempSpeed = speed;
        speed = 0;
        for (int i = 20; i >= 0; i--)
        {
            respawnText.text = "Réapparition dans " + i.ToString();
            yield return new WaitForSeconds(1f);
        }
        respawnText.gameObject.SetActive(false);
        speed = tempSpeed;
        animPlayer.SetTrigger("alive");
        weaponManager.gameObject.SetActive(true);
        float min = float.MaxValue;
        GameObject tp = null;
        foreach (var obj in spawnpoints)
        {
            if (Vector2.Distance(transform.position, obj.transform.position) < min)
            {
                min = Vector2.Distance(transform.position, obj.transform.position);
                tp = obj;
            }
        }
        Teleport(tp.transform.position);
        alive = true;
        Health = baseHealth;
    }

    public void Teleport(Vector3 position)
    {
        position.z = 0;
        if (PV.IsMine)
            transform.position = position;
    }

    public void LinkStats(CharacterStat[] stats)
    {
        baseHealth = (int)((100 + stats[0].Value) * stats[1].Value);
        Health = health;
        hpRegen = (1 + stats[2].Value) * stats[3].Value;

        if (speed == tempSpeed)
        {
            tempSpeed = baseSpeed * stats[4].Value;
            speed = tempSpeed;
        }
        else
            tempSpeed = baseSpeed * stats[4].Value;

        armor = stats[5].Value;
        fireRes = stats[6].Value;
        waterRes = stats[7].Value;
        airRes = stats[8].Value;

        weaponManager.LinkStats(stats[9].Value);

        //DAMAGES
    }

    public void SwapWeapons(string weapon)
    {
        weaponManager.Swap(weapon);
    }

    IEnumerator Regen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (alive && health < baseHealth)
            {
                Health += hpRegen;
            }
        }
    }

    public void TakeDOTDamage(int repetition, float dmg)
    {
        StartCoroutine(DOTDamage(repetition, dmg));
    }

    IEnumerator DOTDamage(int repetition, float dmg)
    {
        for (int i = 0; i< repetition; i++)
        {
            Health -= dmg;
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void SpeedBuff(int duration, float value)
    {
        StartCoroutine(SpeedB(duration, value));
    }

    IEnumerator SpeedB(int duration, float value)
    {
        speed = speed * value;
        yield return new WaitForSeconds(duration);
        speed = tempSpeed;
    }
}
