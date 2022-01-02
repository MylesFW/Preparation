using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float movementSpeed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    public VectorValue startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
            // Input.GetAxis => creates a "floating" feeling
            // Input.GetAxisRaw => feels more responsive
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();



        //Debug.Log(change);
            // Debug.Log(var/"var") => acts as print statement
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        myRigidBody.MovePosition(transform.position + change * movementSpeed * Time.fixedDeltaTime);
    }
}
