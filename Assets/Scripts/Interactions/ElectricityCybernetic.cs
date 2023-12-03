using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ElectricityCybernetic : MonoBehaviour
{
    const float MOVE_SPEED = 30;

    Vector2 startpos;
    Vector2 pos;
    Vector2 dir;

    GameObject electricity;
    GameObject thrower;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<BoxCollider2D>().enabled = false;
        startpos = rb.position;
        pos = startpos;
        dir = Vector2.zero;
    }

    void FixedUpdate()
    {
        //CastRay(pos, dir);

        rb.MovePosition(pos + dir);
        pos += dir;
    }

    public void Activate(GameObject origin, Vector2 p, Vector2 d)
    {
        pos = p;
        dir = d;
        thrower = origin;

        GetComponent<Light2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.tag == "Receiver")
        {
            Debug.Log("Received");
        }
        else if (c.gameObject.tag == "Player" && c.gameObject.name != thrower.name)
        {
            Debug.Log("Thrower " + thrower);
            Debug.Log("Collided with " + c.gameObject);
            Debug.Log("Retrieved by other player");
            var p = GameObject.Find("InputManager").GetComponent<PlayerInputManager>();
            p.MakeCyberneticActive(c.gameObject);
            Reset();
        }
        else if (c.gameObject.tag == "Insulator")
        {
            Debug.Log("Absorbed");
            Reset();
        }
        else if (c.gameObject.name == "Wall")
        {
            Debug.Log("Died to wall");
            Reset();
        }
        else
        {
            Debug.Log("What?");
        }
    }

    /*
    private void CastRay(Vector2 pos, Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, MOVE_SPEED);

        if(hit.collider != null)
        {
            CheckHit(hit, dir);
        }
    }

    private void CheckHit(RaycastHit2D hit, Vector2 direction)
    {
        if(hit.collider.gameObject.tag == "Receiver")
        {
            Debug.Log("Received");
        }
        else if (hit.collider.gameObject.tag == "Player" && hit.collider.gameObject != thrower)
        {
            Debug.Log(thrower);
            Debug.Log(hit.collider.gameObject);
            Debug.Log("Retrieved by other player");
            var p = GameObject.Find("InputManager").GetComponent<PlayerInputManager>();
            p.MakeCyberneticActive(hit.collider.gameObject);
            Reset();
        }
        else if (hit.collider.gameObject.tag == "Insulator")
        {
            Debug.Log("Absorbed");
            Reset();
        }
        else if (hit.collider.gameObject.name == "Wall")
        {
            Debug.Log("Died to wall");
            Reset();
        }
        else
        {
            Debug.Log("What?");
        }
    }*/

    private void Reset()
    {
        rb.MovePosition(startpos);
        pos = startpos;
        dir = Vector2.zero;
        thrower = null;

        GetComponent<Light2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}