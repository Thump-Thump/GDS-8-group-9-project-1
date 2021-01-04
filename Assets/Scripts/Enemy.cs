using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject bomb;
    
    public float minWait;
    public float maxWait;

    public float enemySpeed;
 
    private bool _isBombSpawning;

    private GameObject _enemyArea;
    private BoxCollider2D _enemyAreaBoxCollider;

    private float _leftBound;
    private float _rightBound;
    private float _topBound;
    private float _bottomBound;
    private Vector3 _targetPosition;
    private bool _isTargetPositionReached = true;
    private bool _customMovingEnabled = false;

 
    void Awake()
    {
        _isBombSpawning = false;
    }

    void Start()
    {
        _enemyArea = GameObject.FindGameObjectsWithTag("EnemyArea")[0];
        _enemyAreaBoxCollider = _enemyArea.GetComponent<BoxCollider2D>();
        CalculateEnemyAreaBounds();
        Invoke("EnableCustomMoving", 3);
    }
    
    
 
    void Update()
    {
        Debug.Log("YOLO");
        HandleSpawningBomb();
        if (_customMovingEnabled)
        {
            HandleMovingEnemy();
        }
        else
        {
           StandbyMoving();
        }
    }

    private void HandleSpawningBomb()
    {
        if (!_isBombSpawning)
        {
            float timer = Random.Range(minWait, maxWait);
            Invoke("SpawnBomb", timer);
            _isBombSpawning = true;
        }  
    }
 
    void SpawnBomb()
    {
        Instantiate(bomb, transform.position, transform.rotation);
        _isBombSpawning = false;
    }

    private void CalculateEnemyAreaBounds()
    {
        _leftBound = _enemyAreaBoxCollider.bounds.min.x;
        _rightBound =  _enemyAreaBoxCollider.bounds.max.x;
        _topBound = _enemyAreaBoxCollider.bounds.max.y;
        _bottomBound = _enemyAreaBoxCollider.bounds.min.y;

    }

    private float GenerateNewXPosition()
    {
        return Random.Range(_leftBound, _rightBound);
    }

    private float GenerateNewYPosition()
    {
        return Random.Range(_topBound, _bottomBound);
    }

    private Vector3 GenerateNewPosition()
    {
        return new Vector3(GenerateNewXPosition(), GenerateNewYPosition(), 0);
    }

    private void HandleMovingEnemy()
    {
        if (transform.position == _targetPosition)
        {
            _isTargetPositionReached = true;
        }
        
        if (!_isTargetPositionReached)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, enemySpeed * Time.deltaTime);
        }
        else
        { 
            _targetPosition = GenerateNewPosition();
            _isTargetPositionReached = false;
        }
    }

    private void StandbyMoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_rightBound, transform.position.y, transform.position.z), enemySpeed * Time.deltaTime);
    }

    private void EnableCustomMoving()
    {
        _customMovingEnabled = true;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("enemy");


    }
}
