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
    public ContactFilter2D movementFilter;
    
    PlayerInput p1Input;
    PlayerInput p2Input;
    Rigidbody2D p1rb;
    Rigidbody2D p2rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    Animator p1Anim;
    Animator p2Anim;

    string p1LastState;
    string p2LastState;

    // idle animations
    const string IDLE_DOWN = "IdleDown";
    const string IDLE_LEFT = "IdleLeft";
    const string IDLE_RIGHT = "IdleRight";
    const string IDLE_UP = "IdleUp";

    // movement animations
    const string MOVE_DOWN = "MoveDown";
    const string MOVE_LEFT = "MoveLeft";
    const string MOVE_RIGHT = "MoveRight";
    const string MOVE_UP = "MoveUp";

    // Start is called before the first frame update
    void Start()
    {
        p1Input = PlayerInput.Instantiate(p1, 0, "p1Input", pairWithDevice: Keyboard.current);
        p2Input = PlayerInput.Instantiate(p2, 1, "p2Input", pairWithDevice: Gamepad.current);
        p1rb = p1Input.GetComponent<Rigidbody2D>();
        p2rb = p2Input.GetComponent<Rigidbody2D>();
        p1Anim = p1Input.GetComponent<Animator>();
        p2Anim = p2Input.GetComponent<Animator>();
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
        else 
        {
            string idleAnimation = selectIdle(p1LastState);
            p1Anim.Play(idleAnimation);
        }

        if (p2Update != Vector2.zero) 
        {
            // Try to move using given input, if not, try moving with just vertical or just horizontal component
            // to make movement smoother
            if (!TryMove(p2Update, 1))
                if (!TryMove(new Vector2(p2Update.x, 0), 1))
                    TryMove(new Vector2(0, p2Update.y), 1);
        }
        else 
        {
            string idleAnimation = selectIdle(p2LastState);
            p2Anim.Play(idleAnimation);
        }


    }

    private string selectIdle(string lastMovement)
    {
        if (lastMovement == MOVE_UP) return IDLE_UP;
        else if (lastMovement == MOVE_DOWN) return IDLE_DOWN;
        else if (lastMovement == MOVE_LEFT) return IDLE_LEFT;
        else if (lastMovement == MOVE_RIGHT) return IDLE_RIGHT;
        else return lastMovement;
    }

    private string selectMovement(Vector2 posChange)
    {
        float changeX = posChange.x;
        float changeY = posChange.y;

        if (Mathf.Abs(changeX) >= Mathf.Abs(changeY))
        {
            return (changeX >= 0 ? MOVE_RIGHT : MOVE_LEFT);
        }
        else 
        {
            return (changeY >= 0 ? MOVE_UP : MOVE_DOWN);
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
            if (index == 0)
            {
                Vector2 posChange = inp * moveSpeed * Time.fixedDeltaTime;
                string animation = selectMovement(posChange);
                p1Anim.Play(animation);
                p1LastState = animation;

                p1rb.MovePosition(p1rb.position + posChange);
            }
            else
            {
                Vector2 posChange = inp * moveSpeed * Time.fixedDeltaTime;
                string animation = selectMovement(posChange);
                p2Anim.Play(animation);
                p2LastState = animation;
                
                p2rb.MovePosition(p2rb.position + posChange);
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}