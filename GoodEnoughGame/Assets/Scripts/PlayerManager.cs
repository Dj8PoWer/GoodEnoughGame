using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    int speed = 0;

    public int health = 100;

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
}
