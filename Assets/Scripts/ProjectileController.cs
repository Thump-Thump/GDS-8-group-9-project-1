using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public enum Direction
    {
        Horizontal,
        Vertical
    }

    public Direction projectileDirection;
    public float projectileSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontally();
    }

    private void HandleProjectileTrajectory()
    {
        if (projectileDirection == Direction.Horizontal)
        {
            MoveHorizontally();
        } else if (projectileDirection == Direction.Vertical)
        {
            MoveVertically();
        }
    }

    private void MoveHorizontally() {
        gameObject.transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
    }

    private void MoveVertically()
    {
        gameObject.transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);

    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Projectile");


    }
}
