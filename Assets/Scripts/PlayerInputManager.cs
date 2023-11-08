using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;

    public float moveSpeed = 1.0f;
    public float collisionOffset = 0.01f;
    public float pushSpeedRatio = 0.5f;
    public ContactFilter2D movementFilter;

    PlayerInput p1Input;
    PlayerInput p2Input;
    Rigidbody2D p1rb;
    Rigidbody2D p2rb;
    PlayerPush p1Push;
    PlayerPush p2Push;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        p1Input = PlayerInput.Instantiate(p1, 0, "p1Input", pairWithDevice: Keyboard.current);
        p2Input = PlayerInput.Instantiate(p2, 1, "p2Input", pairWithDevice: Gamepad.current);
        p1rb = p1Input.GetComponent<Rigidbody2D>();
        p2rb = p2Input.GetComponent<Rigidbody2D>();
        p1Push = p1Input.GetComponent<PlayerPush>();
        p2Push = p2Input.GetComponent<PlayerPush>();
    }

    private void FixedUpdate()
    {
        var p1Update = p1Input.actions["Move"].ReadValue<Vector2>();
        var p2Update = p2Input.actions["Move"].ReadValue<Vector2>();
        if (p1Update != Vector2.zero) 
        {
            // Try to move using given input, if not, try moving with just vertical or just horizontal component
            // to make movement smoother
            if (!TryMove(p1Update, 0))
                if (!TryMove(new Vector2(p1Update.x, 0), 0))
                    TryMove(new Vector2(0, p1Update.y), 0);
        }
        if (p2Update != Vector2.zero) 
        {
            // Try to move using given input, if not, try moving with just vertical or just horizontal component
            // to make movement smoother
            if (!TryMove(p2Update, 1))
                if (!TryMove(new Vector2(p2Update.x, 0), 1))
                    TryMove(new Vector2(0, p2Update.y), 1);
        }
    }

    private bool TryMove(Vector2 inp, int index)
    {
        // Check for collisions by casting ray from player to intended position
        int count = (index == 0) ? 
        p1rb.Cast(inp, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset) :
        p2rb.Cast(inp, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            // Move player
            if (index == 0)
                if(p1Push.isPushing)
                {
                    p1rb.MovePosition(p1rb.position + inp * moveSpeed * pushSpeedRatio * Time.fixedDeltaTime);
                }
                else
                {
                    p1rb.MovePosition(p1rb.position + inp * moveSpeed * Time.fixedDeltaTime);
                }
            else
                if(p2Push.isPushing)
                {
                    p2rb.MovePosition(p2rb.position + inp * moveSpeed * pushSpeedRatio * Time.fixedDeltaTime);
                }
                else
                {
                    p2rb.MovePosition(p2rb.position + inp * moveSpeed * Time.fixedDeltaTime);
                }
            return true;
        }
        else
        {
            return false;
        }
    }
}