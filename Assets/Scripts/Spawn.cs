using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    
    private GameObject _enemyArea;
    private BoxCollider2D _enemyAreaBoxCollider;
    
    private float _leftBound;
    private float _rightBound;
    private float _topBound;
    private float _bottomBound;
    private Vector3 _enemiesSpawnPosition;

    public float minWaitBeforeSpawn;
    public float maxWaitBeforeSpwan;
    private bool _isEnemySpawning = false;
    void Start()
    {
        _enemyArea = GameObject.FindGameObjectsWithTag("EnemyArea")[0];
        _enemyAreaBoxCollider = _enemyArea.GetComponent<BoxCollider2D>();
        CalculateEnemyAreaBounds();
        SpawnEnemy();
    }

    void Update()
    {
        HandleSpawningEnemy();
    }
    
    private void HandleSpawningEnemy()
    {
        if (!_isEnemySpawning)
        {
            float timer = UnityEngine.Random.Range(minWaitBeforeSpawn, maxWaitBeforeSpwan);
            Invoke("SpawnEnemy", timer);
            _isEnemySpawning = true;
        }  
    }
 
    void SpawnEnemy()
    {
        int enemyQuantity = Random.Range(2, 5);
        _enemiesSpawnPosition = GenerateNewPosition();

        for (int i = 0; i < enemyQuantity; i++)
        {
            Instantiate(enemyPrefab, new Vector3((_leftBound - 2) - (i * 1), _enemiesSpawnPosition.y, _enemiesSpawnPosition.z), enemyPrefab.transform.rotation);
        }       
        
        _isEnemySpawning = false;
    }
    
    private void CalculateEnemyAreaBounds()
    {
        _leftBound = _enemyAreaBoxCollider.bounds.min.x;
        _rightBound =  _enemyAreaBoxCollider.bounds.max.x;
        _topBound = _enemyAreaBoxCollider.bounds.max.y;
        _bottomBound = _enemyAreaBoxCollider.bounds.center.y;

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
}
