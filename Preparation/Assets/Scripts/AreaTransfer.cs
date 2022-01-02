using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransfer : MonoBehaviour
{

    public Vector2 cameraChange;
    public Vector3 playerChange;
    private MainCameraMovement cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<MainCameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
        }
    }
}
