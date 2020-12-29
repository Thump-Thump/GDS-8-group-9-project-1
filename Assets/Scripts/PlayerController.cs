using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float baseSpeed;
    public float speedInterval;
    public float accelerationQuantity;
    public float playerSpeed;

    public GameObject horizontalProjectile;
    public GameObject verticalProjectile;

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
        HanldeShooting();
        HandleMovement();
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

    private void HandleMovement()
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

    private void HanldeShooting()
    {
        if (ShouldPlayerShoot())
        {
            Instantiate(horizontalProjectile, transform.position, horizontalProjectile.transform.rotation);
            Instantiate(verticalProjectile, transform.position, verticalProjectile.transform.rotation);
        }
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

    private bool ShouldPlayerShoot(){
        return (Input.GetKeyDown(KeyCode.Space));
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (CompareTag("Platform")) 
        {
            
            Debug.Log("JUMP READY");
            jumpReady = true;
        }

    }

}
