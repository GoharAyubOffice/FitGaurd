using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public float speed = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatformOnZ();
    }

    public void MovePlatformOnZ()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.z > 50)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -50);
        }
    }
}
