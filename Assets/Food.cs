using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] float spawnCircleRadius;

    [SerializeField] float moveSpeed = 1f;
    
    private Vector2 GetRandomPosition()
    {
        Vector2 position = Random.insideUnitCircle * spawnCircleRadius;

        return position;
    }
}
