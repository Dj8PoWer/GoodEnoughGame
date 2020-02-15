using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    Vector3 target, mousePos, refVel, shakeOffset;
    float cameraDist = 1.5f;
    float smoothTime = 0.15f, zStart;

    float ShakeMag, ShakeTimeEnd;
    Vector3 shakeVector;
    bool shaking;
    
    // Start is called before the first frame update
    void Start()
    {
        target = player.position;
        zStart = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = GetMousePos();
        target = UpdateTargetPos();
        MoveCamera();
    }

    private Vector3 GetMousePos()
    {
        // Gets the position as a Vector2 of the mouse on the screen (value of coordinate between 0 and 1)
        // (0,0) is the bottom left of the screen, (1,1) is the top right
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        // Set the (0,0) of the vector in the middle of the screen
        // Makes so that (-1,-1) is bottom left and (1,1) is top right
        ret *= 2;
        ret -= Vector2.one;

        // If the coordinates are outside the specified value, resizes the vector to be at a maximum length
        float max = 0.9f;
        if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
            ret = ret.normalized;
        return ret;
    }

    Vector3 UpdateTargetPos()
    {
        // Calculates the new targeted position of the camera
        Vector3 mouseOffset = mousePos * cameraDist;
        Vector3 ret = player.position + mouseOffset;
        // Prevents to camera from being on top of the player
        ret.z = zStart;
        return ret;
    }

    void MoveCamera()
    {
        Vector3 tempPos;
        // Makes the movement smoother
        tempPos = Vector3.SmoothDamp(transform.position, target, ref refVel, smoothTime);
        transform.position = tempPos;
    }
}
