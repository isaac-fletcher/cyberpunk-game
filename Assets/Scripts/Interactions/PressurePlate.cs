using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PressurePlate : MonoBehaviour
{
    public GameObject [] targets;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            foreach (var target in targets)
            {
                if (target.CompareTag("Door"))
                    target.SetActive(false);
                else if (target.CompareTag("Bridge"))
                    FlipBridge(target);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            foreach (var target in targets)
            {
                if (target.CompareTag("Door"))
                    target.SetActive(true);
                else if (target.CompareTag("Bridge"))
                    FlipBridge(target);
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