using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponManager : MonoBehaviour
{
    PhotonView PV;

    public GameObject staff;
    public GameObject sword;
    public GameObject bow;

    float cd1 = 3f;
    float cd2 = 3f;
    float cd3 = 3f;

    int staffID;
    int swordID;
    int bowID;

    float castspeed;

    int physdmg;
    public int Phys
    {
        get { return physdmg; }
        set { physdmg = value;
            bow.GetComponent<Bow>().strength = value;
            sword.GetComponent<Sword>().strength = value;
            staff.GetComponent<Staff>().strength = value;
        }
    }
    public int waterdmg;
    public int firedmg;
    public int airdmg;
    

    public Transform projPos;
    public GameObject Fireball;
    public GameObject Bladevortex;
    public GameObject WaterBomb;

    Action[] Spell = new Action[3];

    void Start()
    {
        PV = GetComponent<PhotonView>();
        PhotonView[] childs = GetComponentsInChildren<PhotonView>();
        staffID = childs[1].ViewID;
        bowID = childs[2].ViewID;
        swordID = childs[3].ViewID;
        staff.SetActive(true);
        bow.SetActive(false);
        sword.SetActive(false);
    }

    private void Update()
    {
        //if (PV.IsMine)
        //Swap();
        if (PV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                PV.RPC("RPC_Fireball", RpcTarget.All, projPos.position, projPos.rotation, firedmg);
            if (Input.GetKeyDown(KeyCode.X))
                PV.RPC("RPC_BladeVortex", RpcTarget.All, projPos.position, projPos.rotation, waterdmg);
            if (Input.GetKeyDown(KeyCode.C))
                ThrowWaterBomb();


            if (Input.GetKeyDown(KeyCode.Alpha1) && Spell[0] != null && cd1 <= 0)
            {
                Spell[0]();
                cd1 = 3 / castspeed;
            }
            else
                cd1 -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Alpha2) && Spell[1] != null && cd2 <= 0)
            {
                Spell[1]();
                cd2 = 3 / castspeed;
            }
            else
                cd2 -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Alpha3) && Spell[2] != null && cd3 <= 0)
            {
                Spell[2]();
                cd3 = 3 / castspeed;
            }
            else
                cd3 -= Time.deltaTime;
        }

    }

    public void Swap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PV.RPC("SwapWeapon", RpcTarget.All, swordID, staffID, bowID, 1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            PV.RPC("SwapWeapon", RpcTarget.All, swordID, staffID, bowID, 2);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            PV.RPC("SwapWeapon", RpcTarget.All, swordID, staffID, bowID, 3);
    }

    public void Swap(string weapon)
    {
        if (weapon == "Sword")
            PV.RPC("SwapWeapon", RpcTarget.All, swordID, staffID, bowID, 1);
        else if (weapon == "Bow")
            PV.RPC("SwapWeapon", RpcTarget.All, swordID, staffID, bowID, 2);
        else if (weapon == "Staff")
            PV.RPC("SwapWeapon", RpcTarget.All, swordID, staffID, bowID, 3);
    }


    [PunRPC]
    public void SwapWeapon(int sword, int staff, int bow, int i)
    {

        if (i == 1)
        {
            PhotonView.Find(staff).gameObject.SetActive(false);
            PhotonView.Find(bow).gameObject.SetActive(false);
            PhotonView.Find(sword).gameObject.SetActive(true);
        }
        if (i == 2)
        {
            PhotonView.Find(staff).gameObject.SetActive(false);
            PhotonView.Find(bow).gameObject.SetActive(true);
            PhotonView.Find(sword).gameObject.SetActive(false);
        }
        if (i == 3)
        {
            PhotonView.Find(staff).gameObject.SetActive(true);
            PhotonView.Find(bow).gameObject.SetActive(false);
            PhotonView.Find(sword).gameObject.SetActive(false);
        }
    }

    void LateUpdate()
    {
        if (PV.IsMine)
        {
            RotateHand();
        }
    }

    void RotateHand()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 position = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        transform.right = position;

        Vector3 tempRot = new Vector3(1,1,1);
        //if camera is on the right side of the screen, flips the character
        if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x > 0.5f)
            tempRot.y = 1;
        else
            tempRot.y = -1;

        transform.localScale = tempRot;
    }

    public void LinkStats(float attackSpeed, float castSpeed)
    {
        this.bow.SetActive(!this.bow.activeInHierarchy);
        Bow bow = this.bow.GetComponent<Bow>();
        bow.animator.keepAnimatorControllerStateOnDisable = true;
        bow.attackSpeed = .7f / attackSpeed;
        bow.animator.SetFloat("Speed", attackSpeed);
        this.bow.SetActive(!this.bow.activeInHierarchy);

        this.sword.SetActive(!this.sword.activeInHierarchy);
        Sword sword = this.sword.GetComponent<Sword>();
        sword.animator.keepAnimatorControllerStateOnDisable = true;
        sword.attackSpeed = .7f / attackSpeed;
        sword.animator.SetFloat("Speed", attackSpeed * 0.310f);
        this.sword.SetActive(!this.sword.activeInHierarchy);

        castspeed = castSpeed;

        staff.GetComponent<Staff>().attackSpeed = 1 / attackSpeed;
    }

    public void SpellSelect(string[] names)
    {
        for (int i = 0; i <3; i++)
        {
            switch(names[i])
            {
                case "Fireball":
                    Spell[i] = () => PV.RPC("RPC_Fireball", RpcTarget.All, projPos.position, projPos.rotation, firedmg);
                    break;
                case "Blade Vortex":
                    Spell[i] = () => PV.RPC("RPC_BladeVortex", RpcTarget.All, projPos.position, projPos.rotation, physdmg);
                    break;
                case "Water Bomb":
                    Spell[i] = () => ThrowWaterBomb();
                    break;
                case "Wind Burst":
                    Spell[i] = null;
                    break;
                default:
                    Spell[i] = null;
                    break;
            }
        }
    }

    void ThrowWaterBomb()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        PV.RPC("RPC_WaterBomb", RpcTarget.All, position, waterdmg);
    }

    [PunRPC]
    void RPC_WaterBomb(Vector3 pos, int strength)
    {
        var Object = Instantiate(WaterBomb, pos, Quaternion.identity);
        var projectil = Object.GetComponent<WaterBomb>();
        projectil.target = "mob";
        projectil.strength = strength;
    }

    [PunRPC]
    void RPC_Fireball(Vector3 pos, Quaternion rot, int strength)
    {
        var Object = Instantiate(Fireball, pos, rot);
        var projectil = Object.GetComponent<Fireball>();
        projectil.target = "mob";
        projectil.strength = strength;
    }

    [PunRPC]
    void RPC_BladeVortex(Vector3 pos, Quaternion rot, int strength)
    {
        var Object = Instantiate(Bladevortex, pos, rot);
        var projectil = Object.GetComponent<BladeVortex>();
        projectil.target = "mob";
        projectil.strength = strength;
    }
}
