using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CustomInput input = null;
    private Vector3 moveVector = Vector3.zero;
    private Rigidbody rigidbody = null;
    private float speed = 4.0f;

    private void Awake()
    {
        input = new CustomInput();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += MovePerformed;
        input.Player.Move.canceled += MoveCanceled;
    }   

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= MovePerformed;
        input.Player.Move.canceled -= MoveCanceled;
    }

    private void FixedUpdate()
    {
        Debug.Log(moveVector);
        Vector3 smoothedDelta = Vector3.MoveTowards(rigidbody.position, rigidbody.position+moveVector, Time.fixedDeltaTime * speed);
        rigidbody.MovePosition(smoothedDelta);
    }

    private void MovePerformed(InputAction.CallbackContext value)
    {
        Vector2 direction = value.ReadValue<Vector2>();
        moveVector = new Vector3(direction.x, 0, direction.y);
    }

    private void MoveCanceled(InputAction.CallbackContext value)
    {
        moveVector = Vector3.zero;
    }
    /*// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
