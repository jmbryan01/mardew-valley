using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] public Camera cam;
    private Vector2 mousePos;

    private Vector2 movement;
    private Vector2 lastMove;    
    private float inputX;
    private float inputY;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(inputX, inputY).normalized;
        movement *= moveSpeed;
        
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = LayerMask.GetMask("Interactables"); //Only allows objects from this layer to be selected
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10f, layerMask);
            if(hit.collider != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null)
                {
                    CanInteract(interactable);
                }
            }     
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x, movement.y);

        if (inputX != 0 || inputY != 0)
        {
            lastMove = new Vector2(inputX, inputY);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void CanInteract(Interactable interactableObject)
    {
        interactableObject.TargetInteractable(transform);
    }
}
