using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    [SerializeField] CircleCollider2D circleCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            Food food = collision.gameObject.GetComponent<Food>();
            food.StartSwimFaster();
        }
    }

   
}
