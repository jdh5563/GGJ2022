using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Fields
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        rb.velocity = new Vector2(-speed, 0);
    }
}
