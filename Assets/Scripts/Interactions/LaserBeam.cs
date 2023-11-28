using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    float laserThickness = 0.03f;
    Vector2 pos, dir;
    
    GameObject laserObj;

    // Visual reprentation of laser
    LineRenderer laser;
    List<Vector2> laserIndices = new List<Vector2>();
    public LaserBeam(Vector2 pos, Vector2 dir, Material material)
    {
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam";
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = laserThickness;
        this.laser.endWidth = laserThickness;
        this.laser.material = material;
        this.laser.startColor = Color.green;
        this.laser.endColor = Color.green;

        CastRay(pos, dir, laser);
    }

    void CastRay(Vector2 pos, Vector2 dir, LineRenderer laser)
    {
        laserIndices.Add(pos);

        RaycastHit2D hit = Physics2D.Raycast(pos, dir, 30);;

        if(hit.collider != null)
        {
            CheckHit(hit, dir, laser);
        }
        else
        {
            laserIndices.Add(pos + dir * 30);
            UpdateLaser();
        }
    }

    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach (Vector2 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
        }
    }

    void CheckHit(RaycastHit2D hit, Vector2 direction, LineRenderer laser)
    {
        if(hit.collider.gameObject.tag == "Mirror")
        {
            Debug.Log("HIT A MIRROR!");
            Vector2 pos = hit.point;
            Vector2 dir = Vector2.Reflect(direction, hit.normal);

            CastRay(pos, dir, laser);
        }

        else
        {
            laserIndices.Add(hit.point);
            UpdateLaser();
        }
    }
}
