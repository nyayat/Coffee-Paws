using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Player : MonoBehaviour
{
    /* *** PUBLIC *** */
    public GameObject[] prefabsDataBase;
    public GameObject food;
    /* *** PRIVATE *** */
    private CustomInput input = null;
    private Vector3 moveVector = Vector3.zero;
    private Vector3 lastMoveVector = Vector3.zero;
    private Rigidbody rigidbody = null;
    private float speed = 4.0f;
    private GameObject interactiveGameObject = null;
    private float heightPlayer = 0f;
    private bool isMoving = false;
    private GameObject prefab = null;

    private void Awake()
    {
        input = new CustomInput();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        heightPlayer = transform.localScale.y;
    }

    private void FixedUpdate()
    {
        Vector3 smoothedDelta = Vector3.MoveTowards(rigidbody.position, rigidbody.position+moveVector, Time.fixedDeltaTime * speed);
        rigidbody.MovePosition(smoothedDelta);

        if(isMoving)
        {
            moveObject();
        }
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

    private void MovePerformed(InputAction.CallbackContext value)
    {
        Vector2 direction = value.ReadValue<Vector2>();
        moveVector = new Vector3(direction.x, 0, direction.y);
        lastMoveVector = moveVector;
    }

    private void MoveCanceled(InputAction.CallbackContext value)
    {
        moveVector = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player enter in " + other.name);
        switch (other.tag)
        {
            case "Food":
                if(interactiveGameObject == null)
                    interactiveGameObject = other.gameObject;
                break;
            case "Box":
                prefab = prefabsDataBase.FirstOrDefault(p => p.name == other.name);
                Debug.Log("Prefab found " + prefab.name);
                if (prefab == null)
                    Debug.LogWarning("Prefab with name " + other.tag + " not found in prefabs array.");
                break;
            case "Machine":
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player exit in " + other.name);
        if(!isMoving && interactiveGameObject != null)
        {
            Debug.Log("Object null " + interactiveGameObject.name);
            interactiveGameObject = null;
            prefab = null;
        }
    }

    public void OnPickUp(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            // To create a prefab
            if(prefab != null && interactiveGameObject == null)
            {
                interactiveGameObject = Instantiate(prefab);
                interactiveGameObject.transform.SetParent(food.transform);
            }

            if(interactiveGameObject != null)
            {
                if(!isMoving)
                {
                    isMoving = true;
                }
                else
                {
                    isMoving = false;
                    float distance = transform.localScale.x/2 + interactiveGameObject.transform.localScale.x/2 + 0.5f;
                    interactiveGameObject.transform.position += lastMoveVector * distance;
                    Vector3 pos = interactiveGameObject.transform.position;
                    pos.y = 1 + interactiveGameObject.transform.localScale.y;
                    interactiveGameObject.transform.position = pos;
                    prefab = null;
                    interactiveGameObject = null;
                }
            }
        }
    }

    /* *** ACTION ON OTHER *** */
    private void moveObject()
    {
        interactiveGameObject.transform.position = new Vector3(transform.position.x, transform.position.y + heightPlayer, transform.position.z);
    }
}
