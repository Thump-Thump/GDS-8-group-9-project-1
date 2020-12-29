using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float force;
    public float maxSpeed;
    public float minSpeed;

    private Rigidbody playerRigidBody;
    
    
    void Start()
    {
        LoadComponents();
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
            playerRigidBody.AddForce(Vector3.right * force * forwardIpnut, ForceMode.Acceleration);
        }  
        else if (ShouldPlayerMaintainMaxSpeed(forwardIpnut, currentPlayerSpeed)) 
        {
            playerRigidBody.velocity = new Vector3(maxSpeed, 0, 0);

        } 
        else if (ShouldPlayerSlowDown(forwardIpnut, currentPlayerSpeed)) 
        {
            playerRigidBody.AddForce(Vector3.right * force * forwardIpnut, ForceMode.Acceleration);
        } 
        else if (ShouldPlayerMaintainMinSpeed(forwardIpnut, currentPlayerSpeed)) 
        {
            playerRigidBody.velocity = new Vector3(minSpeed, 0, 0);
        } 
        else 
        {
            playerRigidBody.velocity = new Vector3(minSpeed, 0, 0);
        }
    }

    private bool ShouldPlayerAccelerate(float forwardIpnut, float currentPlayerSpeed)
    {
        return (forwardIpnut == 1 && currentPlayerSpeed < maxSpeed);
    }

    private bool ShouldPlayerMaintainMaxSpeed(float forwardIpnut, float currentPlayerSpeed)
    {
        return (forwardIpnut == 1 && currentPlayerSpeed >= maxSpeed);
    }

    private bool ShouldPlayerSlowDown(float forwardIpnut, float currentPlayerSpeed)
    {
        return (forwardIpnut == -1 && currentPlayerSpeed > minSpeed);
    }

        private bool ShouldPlayerMaintainMinSpeed(float forwardIpnut, float currentPlayerSpeed)
    {
        return (forwardIpnut == -1 && currentPlayerSpeed <= minSpeed);
    }
}
