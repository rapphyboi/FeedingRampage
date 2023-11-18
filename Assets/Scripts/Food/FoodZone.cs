using PanettoneGames.Poseidon.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodZone : MonoBehaviour
{
    [SerializeField] private GameObjectPool foodPool;
    [SerializeField] private int foodCount = 5;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private Renderer spriteRenderer;

    private Food thisFood;

    private int _currentFoodCount;
    public int CurrentFoodCount { get { return _currentFoodCount; } }

    private Vector2 swimDirection;

    private void Awake()
    {
        foodPool.Prewarm();
        _currentFoodCount = foodCount;
    }

    private void Start()
    {
        swimDirection = SetRandomSwimDirection();

        for(int i = 0; i < foodCount; i++)
        {
            SpawnFood();
            thisFood.FoodZone = this;
        }
    }

    private void Update()
    {
        if (_currentFoodCount == 0)
        {
            if (!spriteRenderer.isVisible)
            {
                StartRespawn();
            }
        }
    }

    private void SpawnFood()
    {
        var food = foodPool.Get();
        thisFood = food.GetComponent<Food>();
        thisFood.transform.position = RandomPointInBounds(circleCollider.bounds);
        thisFood.SwimDirection = swimDirection;
    }

    public void DecrementCurrentFood()
    {
        _currentFoodCount--;
    }

    private void StartRespawn()
    {
        _currentFoodCount = foodCount;
        swimDirection = SetRandomSwimDirection();
        for (int i = 0; i < foodCount; i++)
        {
            SpawnFood();
        }
    }

    private Vector2 SetRandomSwimDirection()
    {
        return new Vector2(Random.Range(0, 2) * 2 - 1, 0);
    }

    private Vector2 RandomPointInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y));
    }

    
}
