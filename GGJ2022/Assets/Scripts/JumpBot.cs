using UnityEngine;

public class JumpBot : Robot
{
    // Fields
    [SerializeField] private int jumpSpeed = 12;

    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /// <summary>
    /// If the robot touches an obstacle, the player loses.
    /// If it touches the platform, reset the jump.
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Platform")
		{
            isJumping = false;
	    animator.SetBool("IsJumping", false);
		}
        else if (collision.collider.tag == "Obstacle")
        {
            Debug.Log("Lose");
        }
    }

    /// <summary>
    /// Checks to see if the player wants to jump.
    /// </summary>
    protected override void ProcessInput()
    {
        base.ProcessInput();

        if (!isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }

    /// <summary>
    /// Makes the robot jump.
    /// </summary>
    private void Jump()
	{
        isJumping = true;
	animator.SetBool("IsJumping", true);
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
	}
}
