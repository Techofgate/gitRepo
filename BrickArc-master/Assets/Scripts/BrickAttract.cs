using UnityEngine;
using System.Collections;

public class BrickAttract : BrickBase
{
    public GameObject attractPrefab;
    public float attractR;

    private GameObject attract;

    protected override void Start()
    {
        base.Start();
        attract = Instantiate(attractPrefab, transform);
        attract.transform.SetParent(transform.root);
        attract.transform.localScale = new Vector3(attractR, attractR, attractR);
        attract.transform.SetParent(transform);
    }

    protected override void BrickEffect(GameObject ball)
    {
        SpeedAdjust(ball);
        Destroy(attract);
        DestroyBrick();
    }

    protected override void EmitBreakParticle() {
        GameObject.FindGameObjectWithTag("ParticleManager").GetComponent<ParticleManager>().Emit(1, transform);
    }

    private void SpeedAdjust(GameObject ball)
    {
        float newMagnitude = Mathf.Log(ball.GetComponent<Rigidbody2D>().velocity.magnitude - 2) + 3;
        ball.GetComponent<Rigidbody2D>().velocity = ball.GetComponent<Rigidbody2D>().velocity.normalized * newMagnitude;
    }
}
