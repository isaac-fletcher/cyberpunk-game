using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public bool upMovement;

    public bool shouldStop;

    public float stopAtPosition;

    public float movementSpeed;

    private Vector3 startPos;

    private float stopPoint;

    private float newStopPoint;
    
    private int slowDownIterator;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        slowDownIterator = 2;

        if (!upMovement){
            stopPoint = (stopAtPosition - startPos.x) / 2;
        }
        else {
            stopPoint = (stopAtPosition - startPos.y) / 2;
        }

        newStopPoint = stopPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldStop){
            if (!upMovement)
                transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
            else
                transform.Translate(Vector3.up * Time.deltaTime * movementSpeed);
        }
        else {
            if (!upMovement)
                transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
            else
                transform.Translate(Vector3.up * Time.deltaTime * movementSpeed);
            slowDown();
        }
    }

    void slowDown(){
        Vector3 currPos = transform.position;

        if (!upMovement){
            if ((currPos.x - startPos.x) >= newStopPoint) {
                transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
                movementSpeed = movementSpeed * 0.8f;
                newStopPoint += stopPoint / slowDownIterator;
                slowDownIterator *= 2;
            }
        }
        else{
            if ((currPos.y - startPos.y) >= newStopPoint) {
                transform.Translate(Vector3.up * Time.deltaTime * movementSpeed);
                movementSpeed = movementSpeed * 0.8f;
                newStopPoint += stopPoint / slowDownIterator;
                slowDownIterator *= 2;
            }
        }

        if (currPos.y >= stopAtPosition){
            movementSpeed = 0;
        }
    }
}
