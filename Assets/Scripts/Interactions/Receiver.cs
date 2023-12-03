using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Receiver : MonoBehaviour
{
    public GameObject target;

    bool powered;

    // Start is called before the first frame update
    void Start()
    {
        powered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPowered()
    {
        if (!powered)
        {
            target.SetActive(false);
            powered = false;
        }
    }
}