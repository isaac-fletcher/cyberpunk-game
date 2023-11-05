using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject door;

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
        Debug.Log(c);
        if (c.gameObject.CompareTag("Player") || c.gameObject.CompareTag("Block"))
            door.SetActive(false);
        else
            door.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player") || c.gameObject.CompareTag("Block"))
            door.SetActive(true);
    }

}
