using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaTransfer : MonoBehaviour
{

    public Vector2 cameraChange;
    public Vector3 playerChange;
    private MainCameraMovement cam;
    public bool needText;
    public string placeName;
    public GameObject text;
    public GameObject locationCard;
    public Text placeCardAndText;

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
            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        locationCard.SetActive(true);
        placeCardAndText.text = placeName;
        yield return new WaitForSeconds(10f);
        text.SetActive(false);
    }
}
