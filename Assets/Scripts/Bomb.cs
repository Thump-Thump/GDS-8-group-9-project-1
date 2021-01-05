using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombSpeed;
    public float bombDelaySpeed;

    void Start()
    {
        Invoke("SpeedUpBomb", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Drop();
    }
    
    private void Drop() {
        gameObject.transform.Translate(Vector3.down * bombSpeed * Time.deltaTime);
    }

    private void SpeedUpBomb()
    {
        bombSpeed = bombDelaySpeed;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

    }
}
