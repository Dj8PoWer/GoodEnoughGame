using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float attackSpeed;
    private float cooldown;

    public Animator animator;
    public Transform slashPos;
    public Transform slashRot;
    public GameObject slash;

    private PhotonView PV;

    public int strength = 10;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        animator.keepAnimatorControllerStateOnDisable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
//            if (animator.GetBool("Attack"))
//                animator.SetBool("Attack", false);
            if (cooldown <= 0 && Input.GetKey(KeyCode.Mouse0))
            {
                //                animator.SetBool("Attack", true);
                animator.SetTrigger("attacc");
                PV.RPC("RPC_Attack", RpcTarget.All, slashPos.position, slashRot.rotation, strength);
                cooldown = attackSpeed;
            }
            else
                cooldown -= Time.deltaTime;
        }
    }

    void Flip()
    {
        if (transform.localScale.x == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    [PunRPC]
    void RPC_Attack(Vector3 pos, Quaternion rot, int strength)
    {
        var Object = Instantiate(slash, pos, rot);
        var projectil = Object.GetComponent<SwordSlash>();
        projectil.target = "mob";
        projectil.strength = strength;
    }
    
}
