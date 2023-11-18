using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Player : MonoBehaviour
{
    [SerializeField] private float scaleIndex = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D childCollider;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private bool facingRight = true;
    private Vector2 inputVector;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        virtualCamera.LookAt = this.transform;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.UseSkill.performed += UseSkill_performed;
        playerInputActions.Player.Movement.performed += Movement_performed;
        playerInputActions.Player.Movement.canceled += Movement_canceled;
    }

    private void Movement_canceled(InputAction.CallbackContext obj)
    {
        if (obj.canceled)
        {
            //animator.SetBool("IsSwimming", false);
        }
    }

    private void Movement_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {

        }
    }

    private void Update()
    {
        inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        animator.SetFloat("Speed", inputVector.sqrMagnitude);

        if(inputVector.x < 0 && facingRight)
        {
            Flip();
        }
        if (inputVector.x > 0 && !facingRight)
        {
            Flip();
        }

    }


    private void FixedUpdate()
    {
        rb.velocity = inputVector * moveSpeed;
    }


    private void UseSkill_performed(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Used Skill");
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Vector3 foodSize = collision.transform.localScale;
            transform.localScale += foodSize * scaleIndex;
            GetComponentInChildren<SoundEmitter>();
        }
    }

    

    /*public void UseSkill(InputAction.CallbackContext context)
    {
        
    }*/
}
