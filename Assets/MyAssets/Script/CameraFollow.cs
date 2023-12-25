using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed;
    public Vector3 offset;

    private void Awake()
    {
        smoothSpeed = 0.05f;

    }

    private void FixedUpdate()
    {
        target = move.player_[move.round].transform.GetChild(1).transform;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;


        transform.LookAt(target);
    }

    
}
