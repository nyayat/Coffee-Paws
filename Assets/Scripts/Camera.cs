using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    protected Vector3 offset = new Vector3(-5f, 5f, -5f);
    protected float smoothTime = 0.1f;
    protected Vector3 velocity = Vector3.zero;

/*     public Transform SetTarget
	{
		set { target = value; }
		
	} */
    [SerializeField] protected Transform target;

    protected void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

    }
}
