using Application.UseCase;
using System.Threading.Tasks;
using UnityEngine;

public class Players : MonoBehaviour
{
    private MovementDirections directions;
    private DoT DOT;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float multiplier;
    private int count;

    [SerializeField] private GameObject GameObject;
    private void Awake()
    {
        directions = new MovementDirections();
        DOT = new();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Returner();
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

        if(DOT.GetHp() <= 0) gameObject.SetActive(false);

    }

    public async Task DoT()
    {
        await Task.Delay(3000);
        DOT.DamageOverTime(10);
        Debug.Log(DOT.GetHp());
        await Returner();
    }

    public async Task Returner()
    {
        await DoT();
    }
}
