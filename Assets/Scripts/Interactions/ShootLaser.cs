using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    public GameObject receptacle;
    public string laserBeamName;
    public Color laserColor;

    LaserBeam beam;

    // Update is called once per frame
    void Update()
    {
        // Remove old laser
        Destroy(GameObject.Find(laserBeamName));
        // Create new laser, -gameObject.transform.up fires a laser downwards(this aligns with the gun sprite)
        beam = new LaserBeam(gameObject.transform.position, -gameObject.transform.up, material, receptacle, laserBeamName, laserColor);
    }
}
