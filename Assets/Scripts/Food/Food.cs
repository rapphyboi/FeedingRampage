using PanettoneGames.Poseidon.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IGameObjectPooled
{
    public GameObjectPool Pool { get; set; }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float panicSpeed = 5f;
    [SerializeField] private float panicDuration = 3f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private float _foodSize;

    public float FoodSize { get { return _foodSize; } }

    private FoodZone _foodZone;
    public FoodZone FoodZone { set { _foodZone = value; } }

    private float currentMoveSpeed;

    private Vector2 _swimDirection;
    public Vector2 SwimDirection { set { _swimDirection = value; } }

    private bool facingRight = true;

    private void Start()
    {
        currentMoveSpeed = moveSpeed;
        StartSwim();
    }

    private void OnEnable()
    {
        currentMoveSpeed = moveSpeed;
        StartSwim();
    }

    private void StartSwim()
    {
        animator.SetFloat("Speed", _swimDirection.sqrMagnitude);
        if (_swimDirection.x < 0 && facingRight)
        {
            Flip();
        }
        if (_swimDirection.x > 0 && !facingRight)
        {
            Flip();
        }
        rb.velocity = _swimDirection * currentMoveSpeed;
    }

    private void SetRandomSwimDirection()
    {
        _swimDirection = new Vector2(Random.Range(0, 2) * 2 - 1, 1);
    }

    public void StartSwimFaster()
    {
        StartCoroutine(FastSwim());
    }

    IEnumerator FastSwim()
    {
        SetRandomSwimDirection();
        currentMoveSpeed = panicSpeed;
        StartSwim();
        yield return new WaitForSeconds(panicDuration);
        SetRandomSwimDirection();
        currentMoveSpeed = moveSpeed;
        StartSwim();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Mouth>())
        {
            Debug.Log("LOL");
            collision.gameObject.GetComponent<Mouth>().CreatePanic();
            _foodZone.DecrementCurrentFood();
            Debug.Log(_foodZone.CurrentFoodCount);
            Pool?.ReturnToPool(this.gameObject);
        }
    }

}
