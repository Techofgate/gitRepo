using UnityEngine;
using System.Collections;

public class BrickHard : BrickBase
{
    public Material[] materials;

    private int life;
    private MeshRenderer meshRenderer;

    protected override void Start()
    {
        base.Start();
        life = materials.Length;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = materials[life - 1];
    }

    protected override void BrickEffect(GameObject ball)
    {
        life--;
        if (life > 0)
            meshRenderer.material = materials[life - 1];
        else
            DestroyBrick();
    }
}
