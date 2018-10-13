using UnityEngine;
using System.Collections;

public class BrickSpeedChange : BrickBase
{
    public float bounciness;

    private PhysicsMaterial2D physicsMaterial2D;

    protected override void Start()
    {
        base.Start();
        physicsMaterial2D = new PhysicsMaterial2D();
        physicsMaterial2D.friction = 0;
        physicsMaterial2D.bounciness = bounciness;
        GetComponent<Collider2D>().sharedMaterial = physicsMaterial2D;
    }
    
}
