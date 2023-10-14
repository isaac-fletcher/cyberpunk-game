using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float collisionOffset = 0.01f;
    public ContactFilter2D movementFilter;

    Vector2 inp;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (inp != Vector2.zero) 
        {
            // Try to move using given input, if not, try moving with just vertical or just horizontal component
            // to make movement smoother
            if (!TryMove(inp))
                if (!TryMove(new Vector2(inp.x, 0)))
                    TryMove(new Vector2(0, inp.y));
        }
    }

    private bool TryMove(Vector2 direction)
    {
        // Check for collisions by casting ray from player to intended position
        int count = rb.Cast(inp, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            rb.MovePosition(rb.position + inp * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue movement)
    {
        inp = movement.Get<Vector2>();
    }
}
