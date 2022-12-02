using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    // Start is called before the first frame update
    void Start()
    {

        startPos = transform.position;

        // Get the half width of background using box collider
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Bring the background at the starting position if exactly on its half
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;    
        }
    }
}
