using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PressurePlate : MonoBehaviour
{
    public GameObject [] targets;
    public AudioSource platePress;
    public AudioSource plateUnpress;
    Animator animator;

    private bool isPressed;
    private GameObject lastCollided;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

                foreach (var target in targets)
                {
                    if (target.CompareTag("Door"))
                        target.SetActive(false);
                    else if (target.CompareTag("Bridge"))
                        FlipBridge(target);
                }
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
                foreach (var target in targets)
                {
                    if (target.CompareTag("Door"))
                        target.SetActive(true);
                    else if (target.CompareTag("Bridge"))
                        FlipBridge(target);
                }

                isPressed = false;
            }
        }


    }

    private void FlipBridge(GameObject bridge)
    {
        var collider = bridge.GetComponent<Collider2D>();
        collider.enabled = !collider.enabled;

        var renderer = bridge.GetComponent<TilemapRenderer>();
        renderer.enabled = !renderer.enabled;
    }
}