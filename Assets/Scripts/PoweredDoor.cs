using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.Universal;

public class PoweredDoor : MonoBehaviour
{
    public GameObject [] sources;
    public AudioSource open;

    bool opened = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool powered = true;

        foreach (var source in sources)
        {
            if (source.GetComponent<Light2D>().enabled is false)
                powered = false;
        }

        if (powered && !opened)
            OpenDoor();
    }

    void OpenDoor()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<TilemapRenderer>().enabled = false;
        GetComponent<Light2D>().enabled = true;

        open.Play();
        opened = true;
    }
}
