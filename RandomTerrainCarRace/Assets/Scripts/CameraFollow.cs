using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;

    private void FixedUpdate()
    {
        HandleTranslation();
    }

    private void HandleTranslation()
    {
        Vector3 jointTarget = (target1.transform.position + target2.transform.position) / 2;
        Vector3 targetPosition = new Vector3(0f, offset.y, jointTarget.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
        if(transform.position.z < 3.1f)
        {
            Vector3 newPosition = transform.position;
            newPosition.z = 3.1f;
            transform.position = newPosition;
        }
    }
}
