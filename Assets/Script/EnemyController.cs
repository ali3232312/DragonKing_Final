using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform Player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;

    private Rigidbody2D rb;
    private Vector2 movement;
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);
        Vector2 direction = Vector2.zero;
        if (distanceToPlayer < detectionRadius)
        {
             direction = (Player.position - transform.position).normalized;
           
            movement = new Vector2(direction.x, 0);

        }
        else
        {
            movement = Vector2.zero;
        }
        rb.linearVelocity = direction * speed  ;
    }

}
