using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPlayerController : MonoBehaviour {
    public GameObject ball;
    public float speed;

    private float yMin;
    private float yMax;

    private Rigidbody rb;
    private Rigidbody rbball;

    public bool isPlaying;
    public bool isHumanPlayer;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rbball = ball.GetComponent<Rigidbody>();

        yMin = -7.25f;
        yMax = 7.25f;

        isHumanPlayer = false;
    }

    public void Reset()
    {
        rb.transform.position = new Vector3
        (
            14.0f,
            0.0f,
            -0.5f
        );

        isPlaying = false;
    }

    // Update is called once per frame
    void Update () {
        if(isPlaying)
        {
            if(isHumanPlayer)
            {
                speed = 15;
                if (Input.GetKey("up"))
                {
                    rb.transform.Translate(0.0f, 1.0f * speed * Time.deltaTime, 0.0f);
                }
                else if (Input.GetKey("down"))
                {
                    rb.transform.Translate(0.0f, -1.0f * speed * Time.deltaTime, 0.0f);
                }

                rb.transform.Translate(0.0f, Input.GetAxis("Vertical2") * speed * Time.deltaTime, 0.0f);

                rb.transform.position = new Vector3
                (
                    14.0f,
                    Mathf.Clamp(rb.position.y, yMin, yMax),
                    -0.5f
                );
            }
            else
            {
                if ((rb.position.y + 2.5f) < rbball.position.y)
                {
                    rb.transform.Translate(0.0f, 1.0f * speed * Time.deltaTime, 0.0f);
                }
                else if ((rb.position.y - 2.5f) > rbball.position.y)
                {
                    rb.transform.Translate(0.0f, -1.0f * speed * Time.deltaTime, 0.0f);
                }
            }
        }
    }
}
