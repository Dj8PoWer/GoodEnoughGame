using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    public float attackSpeed;
    private float cooldown;

    private Animator animator;
    public Transform projectilePos;
    public Transform projectileRot;
    public GameObject projectile;

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
                PV.RPC("RPC_Attack", RpcTarget.All, projectilePos.position, projectileRot.rotation, (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
                cooldown = attackSpeed;
            }
        }
    }
    
    [PunRPC]
    void RPC_Attack(Vector3 pos, Quaternion rot, Vector2 mousePos)
    {
        var Object = Instantiate(projectile, pos, Quaternion.identity);
        var projectil = Object.GetComponent<Projectile>();
        projectil.mousePos = mousePos;
        projectil.target = "mob";
    }
}
