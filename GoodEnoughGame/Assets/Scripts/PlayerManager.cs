using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    WeaponManager weaponManager;
    [SerializeField]
    GameObject loot;

    [SerializeField]
    int baseSpeed = 3;
    float speed = 3;

    public int baseHealth = 100;
    private float health = 100;

    private float hpRegen = 1;

    private float armor;
    private float fireRes;
    private float waterRes;
    private float airRes;

    private PhotonView PV;

    private Animator animPlayer;
    

    public GameObject cam;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        // Assings components to variables
        animPlayer = player.GetComponent<Animator>();
        PV = GetComponent<PhotonView>();

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
            Instantiate(loot, transform.position, Quaternion.identity);
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
        health -= amount;
        //animPlayer.SetBool("Hurt", true);
        if (health <= 0)
        {
            //animPlayer.SetBool("Dying", true);
            Destroy(gameObject);
        }
    }

    public void Teleport(Vector3 position)
    {
        if (PV.IsMine)
            transform.position = position;
    }

    public void LinkStats(CharacterStat[] stats)
    {
        health = (baseHealth + stats[0].Value) * stats[1].Value;
        hpRegen = (1 + stats[2].Value) * stats[3].Value;

        speed = baseSpeed * stats[4].Value;

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
}
