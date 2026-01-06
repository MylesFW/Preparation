using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class Player : MonoBehaviour
{

    public PlayerState currentState;
    public float movementSpeed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    public VectorValue startingPosition;
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        //transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        // Is the player in an interaction
        if (currentState == PlayerState.interact)
        {
            return;
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
            // Input.GetAxis => creates a "floating" feeling
            // Input.GetAxisRaw => feels more responsive
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
        



        //Debug.Log(change);
            // Debug.Log(var/"var") => acts as print statement
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.2f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    public void RaiseItem()
    {
        
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
        //change.Normalize();
        //myRigidBody.MovePosition(transform.position + change * movementSpeed * Time.fixedDeltaTime);
    }
    
    //player knockback will occurr if the player has more than zero health

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        //sends signal to the SignalListener which then displays the number of health
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {

            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            Debug.Log("YOU HAVE DIED.");
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;

        }
    }
}
