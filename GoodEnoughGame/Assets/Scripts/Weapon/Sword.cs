using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float attackSpeed;
    private float cooldown;

    private Animator animator;
    public Transform slashPos;
    public Transform slashRot;
    public GameObject slash;

    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0 && Input.GetKey(KeyCode.Mouse0))
            {
                PV.RPC("RPC_Attack", RpcTarget.All, slashPos.position, slashRot.rotation);
                cooldown = attackSpeed;
            }
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
    void RPC_Attack(Vector3 pos, Quaternion rot)
    {
        animator.SetTrigger("Attack");
        Instantiate(slash, pos, rot);
    }
}
