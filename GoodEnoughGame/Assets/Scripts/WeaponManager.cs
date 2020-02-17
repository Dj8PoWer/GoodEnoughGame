using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    PhotonView PV;

    void Start()
    {
        PV = GetComponent<PhotonView>();
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
