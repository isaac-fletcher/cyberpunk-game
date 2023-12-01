using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public bool upDownMovement;

    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!upDownMovement)
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
        else
            transform.Translate(Vector3.up * Time.deltaTime * movementSpeed);
    }
}
