using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Vector3 offset = new Vector3(-5f, 5f, -5f);
    private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

    }
}
