using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector2 velocity;
    public Transform pTransform;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 1);
    }
}
