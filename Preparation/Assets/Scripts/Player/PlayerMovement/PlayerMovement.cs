using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerMovement : MonoBehaviour, ISavable
{
    [HideInInspector] public Vector2 position;
    [HideInInspector] public Vector2 velocity;

    private Inputs inputs;
    private Rigidbody2D Rigidbody2D;
    private ICollidable collidable;
    private float slowSpeed;
    
    public float SlowSpeed
    {
        get;
        set;
    }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        inputs = GetComponent<Inputs>();
    }

    private void Start()
    {
        position.x = transform.position.x;
        position.y = transform.position.y;
        velocity = new Vector2(position.x, position.y);
        EndSlow();
    }

    void Update()
    {
        if (transform.position == Vector3.zero)
        {

        }
        position.x = transform.position.x;
        position.y = transform.position.y;
    }
    private void FixedUpdate()
    {
        
        // Where the Magic Happen pt.2
        ApplySlow();
        Rigidbody2D.MovePosition(velocity);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<ICollidable>(out var inst_Collidable))
        {
            collidable = inst_Collidable;
            collidable.OnEnter();
        }
        else
        {
            return;
        }
    }
    private void OnTriggerStay2D()
    {
        if (collidable == null) {  return; }
        collidable.OnStay();
    }
    private void OnTriggerExit2D()
    {
        if (collidable == null) { return; }
        collidable.OnExit();
        collidable = null;
    }    
    private void ApplySlow()
    {
        velocity.x *= slowSpeed;
        velocity.y *= slowSpeed;
    }
    public void SetSlowSpeed(float value)
    {
        if (slowSpeed > value) { return; }
        slowSpeed = value;
    }
    public void EndSlow()
    {
        if (slowSpeed != 1) { slowSpeed = 1; }
    }

    GameData ISavable.SaveInstance(GameData data)
    {
        var playerData = data.playerData;
        var serVec3 = new SerializableVector3(transform.position.x, transform.position.y, transform.position.z);
        playerData.pos.x = serVec3.x;
        playerData.pos.y = serVec3.y;

        return data;
    }

    void ISavable.LoadInstance(GameData data)
    {
        var playerData = data.playerData;
        var vecPos = playerData.pos;
        transform.position = vecPos.ToVec();
        velocity = vecPos.ToVec();
    }
    void ISavable.NewGame()
    {
        velocity = new Vector2(transform.position.x, transform.position.y);
    }
}

