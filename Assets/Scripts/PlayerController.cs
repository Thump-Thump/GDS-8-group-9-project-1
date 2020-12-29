using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float baseSpeed;
    public float speedInterval;
    public float accelerationQuantity;
    public float playerSpeed;

    public float jumpHeight;
    private bool jumpReady = true;

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
        HandleJump();
    }

    private void LoadComponents() 
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void HandleJump()
    {
        if (ShouldPlayerJump())
        {
            playerRigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jumpReady = false;
            Debug.Log("JUMNP ");

        }
    }

    private void MovePlayer()
    {
        //float currentPlayerSpeed = playerRigidBody.velocity.sqrMagnitude;
        if (ShouldPlayerAccelerate()) 
        {
            playerSpeed += speedInterval;
        }  else if (ShouldPlayerSlowDown()) 
        {
            playerSpeed -= speedInterval;
        }
        //Debug.Log("Speed " + playerSpeed );

        playerRigidBody.velocity = new Vector3(playerSpeed, 0, 0);

    }

    private bool ShouldPlayerAccelerate()
    {
        return ( Input.GetKeyDown(KeyCode.RightArrow) && playerSpeed < (baseSpeed + (speedInterval * accelerationQuantity)));
    }


    private bool ShouldPlayerSlowDown()
    {
        return (Input.GetKeyDown(KeyCode.LeftArrow) && playerSpeed > baseSpeed);
    }

    private bool ShouldPlayerJump()
    {
        return (Input.GetKeyDown(KeyCode.UpArrow) && jumpReady);
    }


    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Platform")
        {
            Debug.Log("JUMP READY");
            jumpReady = true;
        }

    }

}
