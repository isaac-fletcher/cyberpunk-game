using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ElectricityCybernetic : MonoBehaviour
{
    const float MOVE_SPEED = 1.8f;

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
        rb.MovePosition(pos + dir * MOVE_SPEED * Time.fixedDeltaTime);
        pos += dir * MOVE_SPEED * Time.fixedDeltaTime;
    }

    public void Activate(GameObject origin, Vector2 p, Vector2 d)
    {
        pos = p;
        dir = d;
        dir.Normalize();
        thrower = origin;

        GetComponent<Light2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.tag == "Receiver")
        {
            c.gameObject.GetComponent<Receiver>().OnPowered();
        }
        else if (c.gameObject.tag == "Player" && c.gameObject.name != thrower.name)
        {
            var p = GameObject.Find("InputManager").GetComponent<PlayerInputManager>();
            p.MakeCyberneticActive(c.gameObject);
            Reset();
        }
        else if (c.gameObject.tag == "Insulator")
        {
            Reset();
        }
        else if (c.gameObject.name == "Wall" || c.gameObject.tag == "Door" || c.gameObject.tag == "Block")
        {
            Reset();
        }
    }

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