using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject bomb;
    
    public float minWait;
    public float maxWait;
 
    private bool _isBombSpawning;
    private GameObject _enemyArea;
    private BoxCollider2D _enemyAreaBoxCollider;

    private float _leftBound;
    private float _rightBound;
    private float _topBound;
    private float _bottomBound;
 
    void Awake()
    {
        _isBombSpawning = false;
    }

    void Start()
    {
        _enemyArea = GameObject.FindGameObjectsWithTag("EnemyArea")[0];
        _enemyAreaBoxCollider = _enemyArea.GetComponent<BoxCollider2D>();
        CalculateEnemyAreaBounds();
    }
    
    
 
    void Update()
    {

        HandleSpawningBomb();
        Debug.Log("Collider WIDTH: " + _enemyAreaBoxCollider.bounds.min.x);
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
        Instantiate(bomb);
        _isBombSpawning = false;
    }

    private void CalculateEnemyAreaBounds()
    {
        _leftBound = _enemyAreaBoxCollider.bounds.min.x;
        _rightBound =  _enemyAreaBoxCollider.bounds.max.x;
        _topBound = _enemyAreaBoxCollider.bounds.max.y;
        _bottomBound = _enemyAreaBoxCollider.bounds.min.y;

    }
}
