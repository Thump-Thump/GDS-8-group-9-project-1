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
    private bool _isJumping = false;
    [Space(10)]

    [Header("Shooting")]
    public GameObject horizontalProjectile;
    public GameObject verticalProjectile;
    public int horizontalProjectileInterval;
    public int verticalProjectileInterval;
    private bool _horizontalShootReady = true;
    private bool _verticalShootReady = true;
    [Space(10)]



    private Rigidbody2D playerRigidBody;
    
    
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
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void SetInitialVariables()
    {
        _playerInitialPosition = transform.position.x;
    }

    private void HandleJump()
    {
        if (ShouldPlayerJump())
        {
            playerRigidBody.freezeRotation = true;
            playerRigidBody.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
            _jumpReady = false;
            _isJumping = true;
        }
    }

    private void HandleMovement()
    {
        if (ShouldPlayerAccelerate())
        {
            Accelerate();
        }  else if (ShouldPlayerSlowDown())
        {
            SlowDown();
        } else if (ShouldPlayerMaintainSpeed())
        {
            
        }
        else
        {
            NormalizeSpeed();
        }

    }

    private void Accelerate()
    {
        //transform.Translate(Vector3.right * playerAccelerationSpeed * Time.deltaTime);
        Debug.Log("Velocity: " + playerRigidBody.velocity.magnitude);
        
        playerRigidBody.AddForce(Vector3.right * playerAccelerationSpeed);

        GameManager.Instance.SetFastPlayerSpeed(); 
    }

    private void SlowDown()
    {
        //transform.Translate(Vector3.left * playerSlowDownSpeed * Time.deltaTime);
        playerRigidBody.AddForce(Vector3.left * playerAccelerationSpeed);
        GameManager.Instance.SetSlowPlayerSpeed();  
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
        return (Input.GetKey(KeyCode.RightArrow) && transform.position.x < _playerInitialPosition + maxPlayerDistanceRightDirection && !_isJumping);
    }

    private void NormalizeSpeed()
    {
        if (transform.position.x < _playerInitialPosition)
        {
            Accelerate();

        } 
        else if (transform.position.x > _playerInitialPosition)
        {
            SlowDown();
        }
        else
        {
            GameManager.Instance.SetNormalPlayerSpeed();
        }
    }


    private bool ShouldPlayerSlowDown()
    {
        return (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > _playerInitialPosition - maxPlayerDistanceLeftDirection && !_isJumping);
    }

    private bool ShouldPlayerMaintainSpeed()
    {
        return (_isJumping || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow));
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Platform")) 
        {
            _jumpReady = true;
            _isJumping = false;
            playerRigidBody.freezeRotation = false;
            Debug.Log("jump ready");

        }

    }

}
