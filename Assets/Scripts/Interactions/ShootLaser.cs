using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    LaserBeam beam;

    // Update is called once per frame
    void Update()
    {
        // Remove old laser
        Destroy(GameObject.Find("Laser Beam"));
        // Create new laser
        beam = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material);
    }
}
