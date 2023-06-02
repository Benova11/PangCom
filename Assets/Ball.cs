using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float maxVerticalForce = 15f;
    public float maxHorizontalFactor = 0.5f;
    // public BallType ballType;
    public int constantDirection = 1;
    private Rigidbody2D rb;
    [SerializeField]private Vector2 initialVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var direction = GetCollisionDirection(collision);
        if (direction.x != 0)
        {
            initialVelocity = new Vector2(direction.x, initialVelocity.y);
        }
        
        float currentHeight = transform.position.y;
        float maxHeight = 3; // ballType.maxHeight;
        
        if (currentHeight <= maxHeight)
        {
            float verticalForce = Mathf.Lerp(0f, maxVerticalForce, 1) * direction.y;
        
            float horizontalForce = Mathf.Lerp(-maxHorizontalFactor, maxHorizontalFactor, 1) * direction.x;
        
            Vector2 jumpForce = new Vector2(horizontalForce, verticalForce);
            rb.AddForce(jumpForce, ForceMode2D.Impulse);

        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(initialVelocity.x, rb.velocity.y);
    }
    
    private Vector2 GetCollisionDirection(Collision2D collision)
    {
        var direction = Vector2.zero;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 contactNormal = contact.normal;

            direction.x = contactNormal.x switch
            {
                > 0 => 1,
                < 0 => -1,
                _ => direction.x
            };

            direction.y = contactNormal.y switch
            {
                > 0 => 1,
                < 0 => -1,
                _ => direction.y
            };
        }

        return direction;
    }
}