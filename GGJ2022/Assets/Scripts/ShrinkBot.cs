using System.Collections;
using UnityEngine;

public class ShrinkBot : Robot
{
    // Fields
    [SerializeField] private int shrinkDuration = 1;

    private bool hasShrunk = false;
    private float minSize = 0.25f;
    private float maxSize = 1.0f;
    private float scalingProgress = 0f;

    private void Awake()
    {
	StartCoroutine(Grow());
    }
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
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            Debug.Log("Lose");
        }
    }

    /// <summary>
    /// Checks to see if the player wants to shrink.
    /// </summary>
    protected override void ProcessInput()
    {
        base.ProcessInput();

        if (!hasShrunk)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Shrink());
            }
        }
    }

    /// <summary>
    /// Shrinks the robot for 'shrinkDuration' seconds,then unshrinks it.
    /// </summary>
    private IEnumerator Shrink()
	{
	hasShrunk = true;

        while(transform.localScale.y > minSize)
		{
            scalingProgress += 0.1f;
            transform.localScale = new Vector2(Mathf.Lerp(maxSize, minSize, scalingProgress), Mathf.Lerp(maxSize, minSize, scalingProgress));
            yield return new WaitForFixedUpdate();
		}

        scalingProgress = 0f;

        yield return new WaitForSeconds(shrinkDuration);

	StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
	while (transform.localScale.y < maxSize)
        {
            scalingProgress += 0.1f;
            transform.localScale = new Vector2(Mathf.Lerp(minSize, maxSize, scalingProgress), Mathf.Lerp(minSize, maxSize, scalingProgress));
            yield return new WaitForFixedUpdate();
        }

        scalingProgress = 0f;

        hasShrunk = false;
    }
}
