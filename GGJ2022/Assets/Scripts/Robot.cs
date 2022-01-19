using UnityEngine;

public abstract class Robot : MonoBehaviour
{
    // Fields
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float maxSpeed;

    protected Vector2 velocity;

    // Force all robots to implement a collision method
    protected abstract void OnCollisionEnter2D(Collision2D collision);

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ProcessInput();
    }

    protected virtual void FixedUpdate()
    {
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    /// <summary>
    /// Poll the input system for movement input or robot switch input
    /// </summary>
	protected virtual void ProcessInput()
	{
        if (Input.GetKey(KeyCode.D))
        {
            velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }
        else
        {
            velocity = new Vector2(0, rb.velocity.y);
        }

        GameManager.TrySwitchRobots();
    }
}
