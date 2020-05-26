using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public float attackSpeed;
    private float cooldown;

    public Animator animator;
    public Transform arrowPos;
    public Transform arrowRot;
    public GameObject arrow;

    private PhotonView PV;

    public int strength = 10;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        animator.keepAnimatorControllerStateOnDisable = true;
    }

    private void OnValidate()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (animator.GetBool("Shot"))
                animator.SetBool("Shot", false);
            if (cooldown <= 0 && Input.GetKey(KeyCode.Mouse0))
            {
                animator.SetBool("Shot", true);
                PV.RPC("RPC_Attack", RpcTarget.All, arrowPos.position, arrowRot.rotation, strength);
                cooldown = attackSpeed;
            }
            else
                cooldown -= Time.deltaTime;
        }
    }

    [PunRPC]
    void RPC_Attack(Vector3 pos, Quaternion rot, int strength)
    {
        var Object = Instantiate(arrow, pos, rot);
        var projectil = Object.GetComponent<Arrow>();
        projectil.target = "mob";
        projectil.strength = strength;
    }
}
