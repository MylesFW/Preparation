using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerMovement : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;

    private Rigidbody2D Rigidbody2D;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        position.x = transform.position.x;
        position.y = transform.position.y;
    }

    void Update()
    {
        position.x = transform.position.x;
        position.y = transform.position.y;
    }
    private void FixedUpdate()
    {
        // Where the Magic Happen pt.2
        Rigidbody2D.MovePosition(velocity);
    }
}
