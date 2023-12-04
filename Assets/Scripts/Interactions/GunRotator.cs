using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotator : MonoBehaviour
{
    public GameObject target;
    public bool clockWise = true;
    public AudioSource platePress;
    public AudioSource plateUnpress;
    Animator animator;

    private bool isPressed;
    private GameObject lastCollided;
    private float rotationCoefficient = 0.20f;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPressed)
        {
            RotateGun(target);
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (!isPressed)
        {
            if (c.gameObject.CompareTag("Player") || c.gameObject.CompareTag("Block"))
            {
                lastCollided = c.gameObject;
                
                animator.Play("PressedPlate");
                platePress.Play();

                isPressed = true;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (isPressed)
        {
            if (lastCollided.name == c.gameObject.name)
            {
                animator.Play("UnpressedPlate");
                plateUnpress.Play();

                isPressed = false;
            }
        }


    }

    // Rotates the laser gun
    private void RotateGun(GameObject laserGun)
    {
        // Get the current rotation
        Quaternion currentRotation = laserGun.transform.rotation;

        // Modify the "Z" value
        float newZRotation = rotationCoefficient; // Set your desired Z rotation value here

        // Rotate negative direction if clockwise is true
        if(clockWise)
        {
            newZRotation = newZRotation * -1.0f;
        }

        // Rotate clockwise
        Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z + newZRotation);

        // Assign the new rotation to the Transform
        laserGun.transform.rotation = newRotation;
    }
}
