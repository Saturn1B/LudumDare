using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed;
    public Vector3 offSet;
    Vector3 velocity = Vector3.zero;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offSet;
        desiredPosition = new Vector3(0, desiredPosition.y, desiredPosition.z);
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
        //Mathf.Clamp(transform.position.x, -205, 1403);
    }
}
