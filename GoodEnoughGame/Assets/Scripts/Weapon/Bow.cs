using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public float attackSpeed;
    private float cooldown;

    private Animator animator;
    public Transform arrowPos;
    public Transform arrowRot;
    public GameObject arrow;

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
                PV.RPC("RPC_Attack", RpcTarget.All, arrowPos.position, arrowRot.rotation);
                cooldown = attackSpeed;
            }
        }
    }

    [PunRPC]
    void RPC_Attack(Vector3 pos, Quaternion rot)
    {
        animator.SetTrigger("Attack");
        Instantiate(arrow, pos, rot);
    }
}
