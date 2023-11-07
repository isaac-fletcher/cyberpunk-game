using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPush : MonoBehaviour
{
    public float distance=1f;

    public bool isPushing = false;

    // Store player direction in which they are facing. Obtain this from player movement?
    Vector2 direction;
    public LayerMask box;

    GameObject block;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(direction != Vector2.zero)
        {
            Physics2D.queriesStartInColliders = false;

            // Cast a ray in the direction the player is moving towards(up, down, left, or right)
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, box);
            
            if(hit.collider != null && hit.collider.gameObject.tag=="Block")
            {
                isPushing = true;
                block = hit.collider.gameObject;

                block.GetComponent<FixedJoint2D> ().enabled = true; 
                block.GetComponent<FixedJoint2D> ().connectedBody=this.GetComponent<Rigidbody2D> (); 
            }
            else if(block != null)
            {
                isPushing = false;
                block.GetComponent<FixedJoint2D> ().enabled = false;
                block = null;
            }
        }
    }

    // This is used strictly for debugging. This gizmos shows the push range for the player
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right*transform.localScale.x * distance);
    }

    void OnMove(InputValue movement)
    {
        direction = movement.Get<Vector2>();
    }
}
