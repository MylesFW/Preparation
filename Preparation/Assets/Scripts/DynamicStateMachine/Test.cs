using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private FiniteStateMachine fsm;
    private ObjectContext playerContext;

    // Start is called before the first frame update
    void Start()
    {
        playerContext = new PlayerContext
        {
            transform = transform,
            rigidbody = GetComponent<Rigidbody2D>(),
            collider = GetComponent<BoxCollider2D>(),
            animator2D = GetComponent<Animator2D>(),
            audioListener = GetComponent<AudioListener>(),
            audioSource = GetComponent<AudioSource>(),
        };
        fsm = GetComponent<FiniteStateMachine>();
        fsm.context = playerContext;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnGUI()
    {

    }
}