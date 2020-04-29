using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCarCameraController : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offset;
    public float followSpeed = 10;
    public float lookSpeed = 10;

    public void LookAtTarget()
    {
        var lookDirection = objectToFollow.position - transform.position;
        var rot = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.fixedDeltaTime);
    }

    public void MoveToTarget()
    {
        var targetPos = objectToFollow.position + 
            objectToFollow.forward * offset.z + 
            objectToFollow.right * offset.x + 
            objectToFollow.up * offset.y;

        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }

    private void Start()
    {
        offset = transform.position - objectToFollow.position + offset;
    }
}
