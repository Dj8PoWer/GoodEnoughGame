using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    PhotonView PV;

    public GameObject staff;
    public GameObject sword;
    public GameObject bow;

    int staffID;
    int swordID;
    int bowID;

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
        if (PV.IsMine)
            Swap();
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

    public void LinkStats(float attackSpeed)
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

        staff.GetComponent<Staff>().attackSpeed = 1 / attackSpeed;
    }
}
