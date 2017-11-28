using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    //private Rigidbody rb;
    public Rigidbody rb;
    public float speed;
    public GameController gc;

    public bool serveTowardPlayer1;

    public Vector3 pauseVelocity;

    // Use this for initialization
    void Start () {
        //rb = GetComponent<Rigidbody>();
        serveTowardPlayer1 = true;
        pauseVelocity = Vector3.zero;
        //Debug.Log("test");
    }
	
    public void Serve()  {

        float yAngle = 0.0f;
        float xAngle = 1.0f;

        if(serveTowardPlayer1 == true)
        {
            xAngle = -1.0f;
        }

        while (yAngle > -0.1f && yAngle < 0.1f)
        {
            yAngle = Random.Range(3.5f, -3.5f);
        }

        Vector3 movement = new Vector3(xAngle, yAngle, 0.0f);
        //rb.velocity = movement * speed;

        //rb.AddForce(Vector3.zero);

        rb.velocity = movement * speed;
        rb.velocity = rb.velocity.normalized * speed;
    }

    private void Reset()
    {
        rb.position = Vector3.zero;
        Serve();
    }

    public void GameOver()
    {
        rb.velocity = Vector3.zero;
        rb.position = Vector3.zero;
        serveTowardPlayer1 = true;
    }

    public void PauseBall()
    {
        pauseVelocity = rb.velocity;
        rb.velocity = Vector3.zero;
    }

    public void ResumeBall()
    {
        rb.velocity = pauseVelocity;
    }

    // Update is called once per frame
    void Update () {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = new Vector3
            (
                rb.velocity.x * -1.0f,
                rb.velocity.y,
                rb.velocity.z
            );

            rb.velocity = rb.velocity.normalized * speed;
        } else if (collision.gameObject.CompareTag("Wall")) {
            rb.velocity = new Vector3
            (
                rb.velocity.x,
                rb.velocity.y * -1.0f,
                rb.velocity.z
            );

            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "LeftWall")
        {
            serveTowardPlayer1 = true;
            Reset();
            gc.IncrementPlayer2Score();
        }
        if (other.gameObject.name == "RightWall")
        {
            serveTowardPlayer1 = false;
            Reset();
            gc.IncrementPlayer1Score();
        }
    }

    public void recalculateVelocity()
    {
        pauseVelocity = pauseVelocity.normalized * speed;
    }
}
