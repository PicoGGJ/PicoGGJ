using Application.UseCase;
using UnityEngine;

public class Players : MonoBehaviour
{
    private MovementDirections directions;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float multiplier;
    private void Awake()
    {
        directions = new MovementDirections();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Movement(string direction)
    {
        var move = directions.Direction(direction);
        rb.linearVelocity = new Vector2(move[0], move[1]).normalized * speed;
        Debug.Log(rb.linearVelocity.ToString());
    }

    private void Update()
    {
        if(rb.linearVelocityY < 0)
        {
            rb.gravityScale = 10;
        }
        if(rb.linearVelocityY > 0)
        {
            rb.gravityScale = 2;
        }
    }
}
