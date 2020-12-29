using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float force;
    public float maxSpeed;
    public float minSpeed;

    public float baseSpeed;
    public float speedInterval;
    public float accelerationQuantity;
    public float playerSpeed;

    private Rigidbody playerRigidBody;
    
    
    void Start()
    {
        LoadComponents();
        playerSpeed = baseSpeed;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void LoadComponents() 
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void MovePlayer()
    {
        float currentPlayerSpeed = playerRigidBody.velocity.sqrMagnitude;
        float forwardIpnut = Input.GetAxis("Horizontal");


        if (ShouldPlayerAccelerate(forwardIpnut, currentPlayerSpeed)) 
        {
            playerSpeed += speedInterval;
        }  else if (ShouldPlayerSlowDown(forwardIpnut, currentPlayerSpeed)) 
        {
            playerSpeed -= speedInterval;
        }
        Debug.Log("Speed " + playerSpeed );

        playerRigidBody.velocity = new Vector3(playerSpeed, 0, 0);

    }

    private bool ShouldPlayerAccelerate(float forwardIpnut, float currentPlayerSpeed)
    {
        return (forwardIpnut == 1 && playerSpeed < (baseSpeed + (speedInterval * accelerationQuantity)));
    }


    private bool ShouldPlayerSlowDown(float forwardIpnut, float currentPlayerSpeed)
    {
        return (forwardIpnut == -1 && playerSpeed > baseSpeed);
    }


}
