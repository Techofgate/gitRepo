using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBase : MonoBehaviour {

    private float ballVelocityNormal;
    private float ballVelocityReturnStep;

    protected virtual void Start()
    {
        ballVelocityNormal = 3.0f;
        ballVelocityReturnStep = 0.1f;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            SpeedAdjust(collision.gameObject);
            BrickEffect(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }

    
    protected virtual void BrickEffect(GameObject ball)
    {
        DestroyBrick();
    }

    protected void DestroyBrick()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().BrickNumberReduce();
        EmitBreakParticle();
        Destroy(gameObject);
    }

    protected virtual void EmitBreakParticle() {
        GameObject.FindGameObjectWithTag("ParticleManager").GetComponent<ParticleManager>().Emit(0, transform);
    }

    private void SpeedAdjust(GameObject ball)
    {
        Rigidbody2D ballRigidbody2D = ball.GetComponent<Rigidbody2D>();
        Vector2 ballVelocity = ballRigidbody2D.velocity;
        //Debug.Log("ballVelocity1: "+ ballRigidbody2D.velocity.ToString()+" "+ ballRigidbody2D.velocity.magnitude.ToString());
        if (Mathf.Abs(ballVelocity.magnitude - ballVelocityNormal) <= ballVelocityReturnStep)
            ballRigidbody2D.velocity = ballVelocity.normalized * ballVelocityNormal;
        else if (ballVelocity.magnitude > ballVelocityNormal)
            ballRigidbody2D.velocity = ballVelocity.normalized * (ballVelocity.magnitude - ballVelocityReturnStep);
        else if (ballVelocity.magnitude < ballVelocityNormal)
            ballRigidbody2D.velocity = ballVelocity.normalized * (ballVelocity.magnitude + ballVelocityReturnStep);
        //Debug.Log("ballVelocity2: " + ballRigidbody2D.velocity.ToString() + " " + ballRigidbody2D.velocity.magnitude.ToString());
    }
}
