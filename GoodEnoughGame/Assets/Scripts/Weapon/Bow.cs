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
                PV.RPC("RPC_Attack", RpcTarget.All, arrowPos.position, arrowRot.rotation);
                cooldown = attackSpeed;
            }
            else
                cooldown -= Time.deltaTime;
        }
    }

    [PunRPC]
    void RPC_Attack(Vector3 pos, Quaternion rot)
    {
        var Object = Instantiate(arrow, pos, rot);
        var projectil = Object.GetComponent<Arrow>();
        projectil.target = "mob";
    }
}
