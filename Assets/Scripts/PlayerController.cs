using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player speed")] 
    public float playerAccelerationSpeed;
    public float playerSlowDownSpeed;
    public float maxPlayerDistanceLeftDirection;
    public float maxPlayerDistanceRightDirection;
    public float normalizeSpeedFactor;
    private float _playerInitialPosition;
    [Space(10)]
    
    [Header("Jump")]
    public float jumpHeight;
    private bool _jumpReady = true;
    [Space(10)]

    [Header("Shooting")]
    public GameObject horizontalProjectile;
    public GameObject verticalProjectile;
    public int horizontalProjectileInterval;
    public int verticalProjectileInterval;
    private bool _horizontalShootReady = true;
    private bool _verticalShootReady = true;
    [Space(10)]



    private Rigidbody playerRigidBody;
    
    
    void Start()
    {
        LoadComponents();
        SetInitialVariables();
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

    private void SetInitialVariables()
    {
        _playerInitialPosition = transform.position.x;
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
        Debug.Log(transform.position);
        if (ShouldPlayerAccelerate()) 
        { 
            Debug.Log("Accelerate");
            transform.Translate(Vector3.right * playerAccelerationSpeed * Time.deltaTime);
        }  else if (ShouldPlayerSlowDown()) 
        {
            Debug.Log("SlowDown");
            transform.Translate(Vector3.left * playerSlowDownSpeed * Time.deltaTime);
        }
        else
        {
            NormalizeSpeed();
        }

    }



    private void HanldeShooting()
    {
        if (ShouldPlayerShootHorizontally())
        {
            CreateHorizontalProjectile();
            ReloadHorizontalShoot();
        }
        
        if (ShouldPlayerShootVertically())
        {
            CreateVerticalProjectile();
            ReloadVerticalShoot();
        }
    }

    private void ReloadHorizontalShoot()
    {
        DisableHorizontalShoot();
        Task.Delay(horizontalProjectileInterval * 1000).ContinueWith(t=> EnableHorizontalShoot());
    }
    
    private void ReloadVerticalShoot()
    {
        DisableVerticalShoot();
        Task.Delay(verticalProjectileInterval * 1000).ContinueWith(t=> EnableVerticalShoot());
    }

    private void DisableHorizontalShoot()
    {
        _horizontalShootReady = false;
    }
    private void EnableHorizontalShoot()
    {
        _horizontalShootReady = true;
    }
    
    private void DisableVerticalShoot()
    {
        _verticalShootReady = false;
    }
    private void EnableVerticalShoot()
    {
        _verticalShootReady = true;
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
        return (Input.GetKey(KeyCode.RightArrow) && transform.position.x < _playerInitialPosition + maxPlayerDistanceRightDirection);
    }

    private void NormalizeSpeed()
    {
        if (transform.position.x < _playerInitialPosition)
        {
            transform.Translate(Vector3.right * (playerAccelerationSpeed * normalizeSpeedFactor) * Time.deltaTime);

        } else if (transform.position.x > _playerInitialPosition)
        {
            transform.Translate(Vector3.left * (playerSlowDownSpeed * normalizeSpeedFactor) * Time.deltaTime);

        }
    }


    private bool ShouldPlayerSlowDown()
    {
        return (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > _playerInitialPosition - maxPlayerDistanceLeftDirection);
    }

    private bool ShouldPlayerJump()
    {
        return (Input.GetKeyDown(KeyCode.UpArrow) && _jumpReady);
    }

    private bool ShouldPlayerShootHorizontally(){
        return (Input.GetKeyDown(KeyCode.Space) && _horizontalShootReady);
    }
    private bool ShouldPlayerShootVertically(){
        return (Input.GetKeyDown(KeyCode.Space) && _verticalShootReady);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform")) 
        {
            _jumpReady = true;
        }

    }

}
