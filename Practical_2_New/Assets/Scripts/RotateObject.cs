using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public float xrotationAmount = 5.0f;
    public float yrotationAmount = 5.0f;
    public float zrotationAmount = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xrotationAmount, yrotationAmount, zrotationAmount);
    }
}
