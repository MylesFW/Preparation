using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerController controller;
    public Vector2 velocity;
    public Vector3 playerPos;
    public Transform pTransform;

    public void Awake()
    {
        controller = PlayerController.instance;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = controller.playerContext.playerMovement.position;
    }
}
