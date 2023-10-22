using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float rotationSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Camera rotation based on rotationSpeed and horizontalInput
        float horixontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horixontalInput * rotationSpeed * Time.deltaTime);
    }
}
