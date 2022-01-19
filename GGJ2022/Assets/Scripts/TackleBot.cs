using System.Collections;
using UnityEngine;

public class TackleBot : Robot
{
	// Fields
	[SerializeField] private float tackleDuration = 0.5f;
	[SerializeField] private int tackleBoost = 6;

	private bool isTackling = false;

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
	/// If the robot tackles a breakable wall, destroy it.
	/// If it touches any other obstacle or does not tackle a breakable wall, the player loses.
	/// </summary>
	/// <param name="collision"></param>
	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Obstacle")
		{
			if (collision.collider.name == "Breakable Wall(Clone)")
			{
				if (isTackling)
				{
					Destroy(collision.gameObject);
					GameManager.obstacles.Remove(collision.gameObject);
				}
				else
				{
					Debug.Log("Lose");
				}
			}
			else
			{
				Debug.Log("Lose");
			}
		}
	}

	/// <summary>
	/// Checks to see if the player wants to tackle
	/// </summary>
	protected override void ProcessInput()
	{
		if (!isTackling){
			base.ProcessInput();
			if (Input.GetKeyDown(KeyCode.Space))
			{
				StartCoroutine(Tackle());
			}
		}
	}

	/// <summary>
	/// Makes the robot run at a faster speed to the right for 'tackleDuration' seconds.
	/// The player loses controls while tackling.
	/// </summary>
	private IEnumerator Tackle()
	{
		velocity.x = maxSpeed + tackleBoost;
		isTackling = true;
		animator.SetBool("IsTackling", true);
		yield return new WaitForSeconds(tackleDuration);
		velocity.x = 0;
		isTackling = false;
		animator.SetBool("IsTackling", false);
	}
}
