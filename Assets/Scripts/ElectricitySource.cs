using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricitySource : MonoBehaviour
{


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
            var p = GameObject.Find("InputManager").GetComponent<PlayerInputManager>();
            p.MakeCyberneticActive(c.gameObject);
        }
    }
}