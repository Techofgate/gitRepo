using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody2D rb;

    public float attractForce = 10.0f;
    public float MaxSpeed = 5.0f;
    public float MinSpeed = 3.0f;
    public float InitialSpeed = 3.0f;
    public bool Active;

    public void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.onUnitSphere * InitialSpeed;

        Active = true;
    }
	
	void FixedUpdate () {
        if (rb.velocity.magnitude < MinSpeed)
            rb.velocity = rb.velocity.normalized * MinSpeed;
        else if (rb.velocity.magnitude > MaxSpeed)
            rb.velocity = rb.velocity.normalized * MaxSpeed;
    }
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attract")
        {
            Vector2 distance = collision.gameObject.transform.position - gameObject.transform.position;
            Vector2 force = attractForce * distance.normalized / (distance.magnitude * distance.magnitude);
            rb.AddForce(force);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MapArea" && Active)
        {
            Debug.Log("Player Died!");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().EndGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // 音效
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().InstantiateAutoAudio();

        foreach (ContactPoint2D contact in collision.contacts) {
            Vector3 normal = contact.normal;
            Quaternion quaternion = Quaternion.Euler(0, 0, Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg);
            GameObject.FindGameObjectWithTag("ParticleManager").GetComponent<ParticleManager>().Emit(2, transform.position, quaternion);
            break;
        }
    }
}
