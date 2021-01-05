using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -35)
        {
            Destroy(gameObject);
        } else if (transform.position.y > 10)
        {
            Destroy(gameObject);
        } else if (transform.position.x > 15)
        {
            Destroy(gameObject);
        } else if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
