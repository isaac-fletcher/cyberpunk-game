using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.Universal;

public class Torch : MonoBehaviour
{
    public GameObject [] sources;
    Light2D selfLight;

    bool isLit = false;

    // Start is called before the first frame update
    void Start()
    {
        selfLight = GetComponent<Light2D>();
        isLit = selfLight.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        bool allSourcesActive = true;

        foreach (var source in sources)
        {
            var on = source.GetComponent<TilemapRenderer>().enabled;
            if (!on)
                allSourcesActive = false;
        }

        if (isLit != allSourcesActive)
            FlipState();
    }

    void FlipState()
    {
        selfLight.enabled = !selfLight.enabled;
        isLit = selfLight.enabled;
    }
}
