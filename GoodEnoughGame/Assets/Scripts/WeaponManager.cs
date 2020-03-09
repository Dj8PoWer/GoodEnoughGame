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

    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        SwapWeapon();
    }

    public void SwapWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            staff.SetActive(false);
            bow.SetActive(false);
            sword.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            staff.SetActive(false);
            bow.SetActive(true);
            sword.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            staff.SetActive(true);
            bow.SetActive(false);
            sword.SetActive(false);
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
}
