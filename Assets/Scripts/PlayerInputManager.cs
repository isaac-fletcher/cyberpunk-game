using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public const int PLAYER_ONE = 0;
    public const int PLAYER_TWO = 1;

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
        p1Input = PlayerInput.Instantiate(p1, PLAYER_ONE, "p1Input", pairWithDevice: Keyboard.current);
        p2Input = PlayerInput.Instantiate(p2, PLAYER_TWO, "p2Input", pairWithDevice: Gamepad.current);
        p1rb = p1Input.GetComponent<Rigidbody2D>();
        p2rb = p2Input.GetComponent<Rigidbody2D>();
        p1Anim = p1Input.GetComponent<Animator>();
        p2Anim = p2Input.GetComponent<Animator>();
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
            if (!TryMove(p1Update, PLAYER_ONE))
                if (!TryMove(new Vector2(p1Update.x, 0), PLAYER_ONE))
                    TryMove(new Vector2(0, p1Update.y), PLAYER_ONE);
        }
        else 
        {
            string idleAnimation = selectIdle(PLAYER_ONE);
            playMovement(idleAnimation, PLAYER_ONE);
        }

        if (p2Update != Vector2.zero) 
        {
            // Try to move using given input, if not, try moving with just vertical or just horizontal component
            // to make movement smoother
            if (!TryMove(p2Update, 1))
                if (!TryMove(new Vector2(p2Update.x, 0), PLAYER_TWO))
                    TryMove(new Vector2(0, p2Update.y), PLAYER_TWO);
        }
        else 
        {
            string idleAnimation = selectIdle(PLAYER_TWO);
            playMovement(idleAnimation, PLAYER_TWO);
        }

    }

    private string selectIdle(int player)
    {
        string lastMovement = (player == PLAYER_ONE) ? p1LastState : p2LastState;

        if (lastMovement == MOVE_UP) return IDLE_UP;
        else if (lastMovement == MOVE_DOWN) return IDLE_DOWN;
        else if (lastMovement == MOVE_LEFT) return IDLE_LEFT;
        else if (lastMovement == MOVE_RIGHT) return IDLE_RIGHT;
        else return lastMovement;
    }

    private string selectAnimation(Vector2 inp, int player)
    {
        float inpX = inp.x;
        float inpY = inp.y;

        Debug.Log("X Input: " + inpX + "Y Input:" + inpY);

        if (inp == Vector2.zero) {
            return (player == PLAYER_ONE ? p1LastState : p2LastState);
        } 
        else if (Mathf.Abs(inpX) > Mathf.Abs(inpY))
        {
            return (inpX >= 0 ? MOVE_RIGHT : MOVE_LEFT);
        }
        else 
        {
            return (inpY >= 0 ? MOVE_UP : MOVE_DOWN);
        }
    }

    private void playMovement(string animation, int player) {
        if (player == PLAYER_ONE)
        {
            p1Anim.Play(animation);
            p1LastState = animation;  
        }
        else 
        {
            p2Anim.Play(animation);
            p2LastState = animation;  
        }

        return;
    }

    private bool TryMove(Vector2 inp, int player)
    {
        
        Rigidbody2D rb = (player == PLAYER_ONE) ? p1rb : p2rb;
        PlayerPush push = (player == PLAYER_ONE) ? p1Push : p2Push;

        // Check for collisions by casting ray from player to intended position
        bool collided = rb.Cast(inp, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset) != 0;

        if (!collided)
        {
            // Move player
            Vector2 posChange;

            if(push.isPushing)
            {
                posChange = inp * moveSpeed * pushSpeedRatio * Time.fixedDeltaTime;
            }
            else
            {
                posChange = inp * moveSpeed * Time.fixedDeltaTime;
            }

            string animation = selectAnimation(inp, player);
            playMovement(animation, player);
            rb.MovePosition(rb.position + posChange);

            return true;
        }
        else
        {
            return false;
        }
    }
}