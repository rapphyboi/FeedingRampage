using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Player : MonoBehaviour
{
    [SerializeField] private float scaleIndex = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Animator animator;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
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
            animator.SetBool("IsSwimming", false);
        }
    }

    private void Movement_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            animator.SetBool("IsSwimming", true);
        }
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Debug.Log(inputVector);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Food>())
        {
            Vector3 foodSize = collision.transform.localScale;
            transform.localScale += foodSize * scaleIndex;
        }
    }

    /*public void UseSkill(InputAction.CallbackContext context)
    {
        
    }*/
}
