using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float baseSpeed;
    public float speedInterval;
    public float accelerationQuantity;
    public float playerSpeed;

    public GameObject horizontalProjectile;
    public GameObject verticalProjectile;
    public int shotIntervalsInSeconds;
    private bool _shootReady = true;
    
    public float jumpHeight;
    private bool _jumpReady = true;

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
            _jumpReady = false;
        }
    }

    private void HandleMovement()
    {
        if (ShouldPlayerAccelerate()) 
        {
            playerSpeed += speedInterval;
        }  else if (ShouldPlayerSlowDown()) 
        {
            playerSpeed -= speedInterval;
        }

        SetPlayerSpeed();
    }

    private void SetPlayerSpeed()
    {
        playerRigidBody.velocity = new Vector3(playerSpeed, 0, 0);

    }

    private void HanldeShooting()
    {
        if (ShouldPlayerShoot())
        {
            CreateVerticalProjectile();
            CreateHorizontalProjectile();
            ReloadShoot();
        }
    }

    private void ReloadShoot()
    {
        DisableShoot();
        Task.Delay(shotIntervalsInSeconds * 1000).ContinueWith(t=> EnableShoot());
    }

    private void DisableShoot()
    {
        _shootReady = false;
    }
    private void EnableShoot()
    {
        _shootReady = true;
    }

    private void CreateVerticalProjectile()
    {
        Instantiate(verticalProjectile, transform.position, verticalProjectile.transform.rotation);
    }

    private void CreateHorizontalProjectile()
    {
        Instantiate(horizontalProjectile, transform.position, horizontalProjectile.transform.rotation);
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
        return (Input.GetKeyDown(KeyCode.UpArrow) && _jumpReady);
    }

    private bool ShouldPlayerShoot(){
        return (Input.GetKeyDown(KeyCode.Space) && _shootReady);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform")) 
        {
            _jumpReady = true;
        }

    }

}
