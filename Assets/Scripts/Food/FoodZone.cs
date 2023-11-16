using PanettoneGames.Poseidon.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodZone : MonoBehaviour
{
    [SerializeField] private GameObjectPool foodPool;
    [SerializeField] private int foodCount = 5;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] List<Food> foodList = new List<Food>();

    private Food thisFood;

    private int currentFoodCount;
    public int CurrentFoodCount { get { return currentFoodCount; } }

    private Vector2 swimDirection;

    private void Awake()
    {
        foodPool.Prewarm();
        currentFoodCount = foodCount;
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

    private void SpawnFood()
    {
        var food = foodPool.Get();
        thisFood = food.GetComponent<Food>();
        //foodList.Add(thisFood);
        thisFood.transform.position = RandomPointInBounds(circleCollider.bounds);
        thisFood.SwimDirection = swimDirection;
        
    }

    public void DecrementCurrentFood()
    {
        currentFoodCount--;
        if(currentFoodCount == 0)
        {
            swimDirection = SetRandomSwimDirection();
            Invoke("StartRespawn", 3f);
        }
    }
    
    private void StartRespawn()
    {
        for (int i = 0; i < foodCount; i++)
        {
            SpawnFood();
        }
        currentFoodCount = foodCount;
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
