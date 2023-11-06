using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public void UseSkill(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Use Skill");

        }
    }
}
