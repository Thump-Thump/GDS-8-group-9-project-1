using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    public float backgroundSpeed;
    
    private BoxCollider _boxCollider;
    void Start()
    {
        LoadComponents();
    }
    void Update()
    {
        MoveBackground();
        HandleRepeatingBackground();
    }

    private void LoadComponents()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void MoveBackground()
    {
        transform.Translate(Vector3.left * backgroundSpeed * Time.deltaTime);
    }

    private void HandleRepeatingBackground()
    {
        Debug.Log("Position: " + transform.position.x);
        Debug.Log("Background half: " + (_boxCollider.size.x / 2) * -1);

        if (transform.position.x < (_boxCollider.size.x / 2) * -1)
        {
            transform.position = new Vector3(-4.38f, 0, 0);
        }
    }
}
