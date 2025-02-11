﻿using System.Collections;
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
        HandleProjectileTrajectory();
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
        gameObject.transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);
    }

    private void MoveVertically()
    {
        gameObject.transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);

    }
    
    
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
    }
}
